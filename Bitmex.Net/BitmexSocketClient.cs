using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Bitmex.Net.Client.Interfaces;
using Bitmex.Net.Client.Objects.Socket;
using Bitmex.Net.Client.Objects.Socket.Repsonses;
using Bitmex.Net.Client.Objects.Socket.Requests;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bitmex.Net.Client
{
    public class BitmexSocketClient : BaseSocketClient, IBitmexSocketClient
    {
        private readonly ConcurrentDictionary<string, BitmexSubscribeRequest> _sendedSubscriptions = new ConcurrentDictionary<string, BitmexSubscribeRequest>();
        private readonly List<UpdateSubscription> _subscriptions = new List<UpdateSubscription>();
        private object _locker = new object();

        public BitmexSocketClient() : this(BitmexSocketClientOptions.Default.Copy())
        {
        }
        public BitmexSocketClient(BitmexSocketClientOptions options) : base("Bitmex", options)
        {
            SocketStreams = AddApiClient(new BitmexSocketStream(log, this, options));
        }
        public IBitmexSocketStream SocketStreams { get; set; }

        //should be removed after refactoring!
        internal ConcurrentDictionary<string, BitmexSubscribeRequest> SendedSubscriptions => _sendedSubscriptions;

        public event Action OnPongReceived;
        // public event Action<Exception> OnSocketException;
        // public event Action OnSocketClose;
        // public event Action OnSocketOpened;

        #region BaseSocketClient abstract methods implementation
        protected override async Task<CallResult<bool>> AuthenticateSocketAsync(SocketConnection socketConnection)
        {
            if (socketConnection.ApiClient.AuthenticationProvider is null)
                return new CallResult<bool>(false);

            bool isSuccess = false;
            ServerError serverError = null;
            var authRequest = new BitmexSubscribeRequest() { Op = BitmexWebSocketOperation.AuthKeyExpires };
            var authParams = ((BitmexAuthenticationProvider)socketConnection.ApiClient.AuthenticationProvider);
            // request = {"op": "authKeyExpires", "args": [API_KEY, expires, signature]}
            var expires = authParams.ApiExpires;
            authRequest.Args.Add(authParams.Credentials.Key.GetString());
            authRequest.Args.Add(expires);
            authRequest.Args.Add(authParams.Sign(authParams.CreateAuthPayload(HttpMethod.Get, "/realtime", expires)));
            await socketConnection.SendAndWaitAsync(authRequest, TimeSpan.FromSeconds(1), f =>
            {
                if (String.IsNullOrEmpty(f.ToString()))
                {
                    isSuccess = false;
                    serverError = new ServerError("Auth request was not succesful");
                }
                else
                {
                    isSuccess = true;
                }
                return true;

            });
            return isSuccess ? new CallResult<bool>(true) : new CallResult<bool>(serverError);
        }

        protected override bool HandleQueryResponse<T>(SocketConnection socketConnection, object request, JToken data, [NotNullWhen(true)] out CallResult<T> callResult)
        {
            if (data.Type == JTokenType.String && data.ToString() == "pong")
            {
                callResult = null;
                return true;
            }
            callResult = Deserialize<T>(data);
            return callResult;
        }

        protected override bool HandleSubscriptionResponse(SocketConnection socketConnection, SocketSubscription subscription, object request, JToken message, out CallResult<object> callResult)
        {
            callResult = null;
            if (message.Type == JTokenType.String && message.ToString() == "pong")
            {
                return false;
            }
            if (message["info"] != null && ((string)message["info"]).StartsWith("Welcome"))
            {
                log.Write(LogLevel.Debug, "skipping welcome message by request");
                return false;
            }
            if (message.Type == JTokenType.String && (string)message == "pong")
            {
                return true;
            }
            var response = Deserialize<BitmexSubscriptionResponse>(message);
            var bRequest = (BitmexSubscribeRequest)request;
            callResult = response.As<object>(response);
            if (response)
            {
                if (bRequest.Args.Contains(response.Data.Subscribe))
                {
                    return true;
                }
            }
            return false;
        }

        protected override bool MessageMatchesHandler(SocketConnection socketConnection, JToken message, object request)
        {
            if (message.Type == JTokenType.String && message.ToString() == "pong")
            {
                return false;
            }
            if (message["info"] != null && ((string)message["info"]).StartsWith("Welcome"))
            {
                log.Write(LogLevel.Debug, "skipping welcome message by request");
                return false;
            }
            return true;
        }

        protected override bool MessageMatchesHandler(SocketConnection socketConnection, JToken message, string identifier)
        {
            if (message.Type == JTokenType.String && message.ToString() == "pong")
            {
                OnPongReceived?.Invoke();
                return false;
            }
            if (message["info"] != null && ((string)message["info"]).StartsWith("Welcome"))
            {
                log.Write(LogLevel.Debug, "skipping welcome message by id");
                return false;
            }
            return true;
        }

        protected async override Task<bool> UnsubscribeAsync(SocketConnection connection, SocketSubscription subscriptionToUnsub)
        {
            if (subscriptionToUnsub.Request is BitmexSubscribeRequest subReq)
            {
                var r = subscriptionToUnsub.Request as BitmexSubscribeRequest;
                r.Op = BitmexWebSocketOperation.Unsubscribe;
                var result = await QueryAndWaitAsync<BitmexSubscriptionResponse>(connection, r);
                if (result && result.Data.Success && result.Data.Request.Args.Contains(result.Data.Unsubscribe))
                {
                    _sendedSubscriptions.TryRemove(result.Data.Unsubscribe, out _);
                    log.Write(LogLevel.Warning, $"{JsonConvert.SerializeObject(subscriptionToUnsub.Request)} subscription was unsubscribed");
                }
                else
                {
                    log.Write(LogLevel.Warning, $"{JsonConvert.SerializeObject(subscriptionToUnsub.Request)} subscription was not unsubscribed: {result.Error?.Message}");
                }
            }
            return false;
        }

        public override async Task UnsubscribeAllAsync()
        {
            foreach (var c in this._subscriptions)
            {
                await c.CloseAsync().ConfigureAwait(false);
            }
            await base.UnsubscribeAllAsync();
        }

        public override void Dispose()
        {
            //foreach (var s in _subscriptions)
            //{
            //    s.Close().GetAwaiter().GetResult();
            //}
            //foreach (var s in this.sockets.Values)
            //{
            //    s.Close().Wait();
            //}
            _subscriptions.Clear();
            _sendedSubscriptions.Clear();
            base.Dispose();
        }
        #endregion

        private void CheckDoubleSendingRequest(BitmexSubscribeRequest request)
        {
            lock (_locker)
            {
                List<string> toRemove = new List<string>();
                foreach (var arg in request.Args)
                {
                    if (_sendedSubscriptions.ContainsKey(arg.ToString()))
                    {
                        toRemove.Add(arg.ToString());
                    }
                    else
                    {
                        _sendedSubscriptions.TryAdd(arg.ToString(), request);
                    }
                }
                if (toRemove.Any())
                {
                    log.Write(LogLevel.Warning, $"Not sending another subscribe request for topics: {String.Join(',', toRemove)}, cause it was already sended");
                    request.Args.RemoveAll(c => toRemove.Contains(c));
                }
            }
        }
        internal async Task<CallResult<UpdateSubscription>> SubscribeInternal<TUpdate>
        (
            SocketApiClient apiClient,
            string url,
            BitmexSubscribeRequest request,
            bool authenticate,
            Action<DataEvent<TUpdate>> onData,
            CancellationToken ct)
        {
            CheckDoubleSendingRequest(request);
            if (!request.Args.Any())
            {
                log.Write(LogLevel.Warning, $"Not sending empty request {JsonConvert.SerializeObject(request)}");
                return new CallResult<UpdateSubscription>(new ServerError("Not sending empty request ", request));
            }
            request.Args.ValidateNotNull(nameof(request));

            var subscription = await SubscribeAsync
            (
                apiClient,
                url,
                request,
                url + NextId(),
                authenticate,
                onData,
                ct).ConfigureAwait(false);
            if (subscription)
            {
                if (request.Op == BitmexWebSocketOperation.Subscribe)
                    _subscriptions.Add(subscription.Data);
            }
            return subscription;
        }

        internal CallResult<T> DeserializeInternal<T>(JToken token) => Deserialize<T>(token);
    }
}