using Bitmex.Net.Client.Helpers.Extensions;
using Bitmex.Net.Client.Interfaces;
using Bitmex.Net.Client.Objects;
using Bitmex.Net.Client.Objects.Socket;
using Bitmex.Net.Client.Objects.Socket.Repsonses;
using Bitmex.Net.Client.Objects.Socket.Requests;
using CryptoExchange.Net;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Bitmex.Net.Client
{
    public class BitmexSocketClient : SocketClient, IBitmexSocketClient
    {
        private static BitmexSocketClientOptions defaultOptions = new BitmexSocketClientOptions();
        private readonly ConcurrentDictionary<string, BitmexSubscribeRequest> _sendedSubscriptions = new ConcurrentDictionary<string, BitmexSubscribeRequest>();
        private static BitmexSocketClientOptions DefaultOptions => defaultOptions.Copy<BitmexSocketClientOptions>();
        private static readonly Dictionary<string, BitmexInstrumentIndexWithTick> instrumentsIndexesAndTicks = new Dictionary<string, BitmexInstrumentIndexWithTick>();
        private static readonly AutoResetEvent instumentGetWaiter = new(true);
        private static bool areInstrumentsLoaded;
        private readonly bool isTestnet;
        private bool shouldUseIndexesAndTicksFromBitmex = false;

        private readonly List<UpdateSubscription> _subscriptions = new List<UpdateSubscription>();
        private object _locker = new object();
        public BitmexSocketClient() : this(DefaultOptions)
        {
        }
        public BitmexSocketClient(BitmexSocketClientOptions bitmexSocketClientOptions) : base(nameof(BitmexSocketClient), bitmexSocketClientOptions, bitmexSocketClientOptions.ApiCredentials == null ? null : new BitmexAuthenticationProvider(bitmexSocketClientOptions.ApiCredentials))
        {
            isTestnet = bitmexSocketClientOptions.IsTestnet;

            if (bitmexSocketClientOptions.SendPingManually)
            {
                SendPeriodic(TimeSpan.FromSeconds(10), connection => "ping");
            }
            if (bitmexSocketClientOptions.LoadInstruments)
            {
                shouldUseIndexesAndTicksFromBitmex = true;
                Task.Run( async () => await GetInstrumentsTickerAndIndices());
            }
        }

        #region events
        public event Action OnPongReceived;
        public event Action<BitmexSocketEvent<Announcement>> OnAnnouncementUpdate;
        public event Action<BitmexSocketEvent<Chat>> OnChatMessageUpdate;
        public event Action<BitmexSocketEvent<ConnectedUsers>> OnChatConnectionUpdate;
        public event Action<BitmexSocketEvent<Funding>> OnFundingUpdate;
        public event Action<BitmexSocketEvent<Instrument>> OnInstrimentUpdate;
        public event Action<BitmexSocketEvent<Insurance>> OnInsuranceUpdate;
        public event Action<BitmexSocketEvent<Liquidation>> OnLiquidationUpdate;
        public event Action<BitmexSocketEvent<BitmexOrderBookEntry>> OnOrderBookL2_25Update;
        public event Action<BitmexSocketEvent<BitmexOrderBookEntry>> OnorderBookL2Update;
        public event Action<BitmexSocketEvent<BitmexOrderBookL10>> OnOrderBook10Update;
        public event Action<BitmexSocketEvent<GlobalNotification>> OnGlobalNotificationUpdate;
        public event Action<BitmexSocketEvent<Quote>> OnQuotesUpdate;
        public event Action<BitmexSocketEvent<Quote>> OnOneMinuteQuoteBinUpdate;
        public event Action<BitmexSocketEvent<Quote>> OnFiveMinuteQuoteBinUpdate;
        public event Action<BitmexSocketEvent<Quote>> OnOneHourQuoteBinUpdate;
        public event Action<BitmexSocketEvent<Quote>> OnDailyQuoteBinUpdate;
        public event Action<BitmexSocketEvent<Settlement>> OnSettlementUpdate;
        public event Action<BitmexSocketEvent<Trade>> OnTradeUpdate;
        public event Action<BitmexSocketEvent<TradeBin>> OnOneMinuteTradeBinUpdate;
        public event Action<BitmexSocketEvent<TradeBin>> OnFiveMinuteTradeBinUpdate;
        public event Action<BitmexSocketEvent<TradeBin>> OnOneHourTradeBinUpdate;
        public event Action<BitmexSocketEvent<TradeBin>> OnDailyTradeBinUpdate;
        public event Action<BitmexSocketEvent<Affiliate>> OnUserAffiliatesUpdate;
        public event Action<BitmexSocketEvent<Execution>> OnUserExecutionsUpdate;
        public event Action<BitmexSocketEvent<Order>> OnUserOrdersUpdate;
        public event Action<BitmexSocketEvent<Margin>> OnUserMarginUpdate;
        public event Action<BitmexSocketEvent<Position>> OnUserPositionsUpdate;
        public event Action<BitmexSocketEvent<Transaction>> OnUserTransactionsUpdate;
        public event Action<BitmexSocketEvent<Wallet>> OnUserWalletUpdate;
        public event Action<Exception> OnSocketException;
        public event Action OnSocketClose;
        public event Action OnSocketOpened;
        #endregion

        // protected override IWebsocket CreateSocket(string address)
        // {
        //     Dictionary<string, string> emptyCoockies = new Dictionary<string, string>();
        //     Dictionary<string, string> headers = new Dictionary<string, string>();
        //     //if (authProvider != null)
        //     //{
        //     //    headers = this.authProvider.AddAuthenticationToHeaders("bitmex.com/realtime", HttpMethod.Get, null, true, PostParameters.InUri, ArrayParametersSerialization.MultipleValues);
        //     //}
        //     headers.Add("Accept-Encoding", "gzip, deflate, br");
        //     headers.Add("Cache-Control", "no-cache");
        //     headers.Add("Connection", "Upgrade");
        //     headers.Add("Host", $"{(isTestnet ? "testnet" : "www")}.bitmex.com");
        //     headers.Add("Origin", $"https://{(isTestnet ? "testnet" : "www")}.bitmex.com");
        //     headers.Add("Sec-WebSocket-Extensions", "");
        //     headers.Add("Sec-WebSocket-Version", "13");
        //     headers.Add("Upgrade", "websocket");
        //     headers.Add("User-Agent", "https://github.com/ridicoulous/Bitmex.Net/");

        //     var s = SocketFactory.CreateWebsocket(this.log, address, emptyCoockies, headers);
        //     s.Origin = $"https://{(isTestnet ? "testnet" : "www")}.bitmex.com";
        //     s.OnClose += S_OnClose;
        //     s.OnError += S_OnError;
        //     s.OnOpen += S_OnOpen;
        //     return s;
        // }
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

        private void S_OnOpen()
        {
            log.Write(LogLevel.Debug, $"Socket opened");
            OnSocketOpened?.Invoke();
        }

        private void S_OnError(Exception obj)
        {
            log.Write(LogLevel.Debug, $"Socket catched exception {obj.ToString()}");

            OnSocketException?.Invoke(obj);
        }

        private void S_OnClose()
        {
            OnSocketClose?.Invoke();
        }

        protected override async Task<CallResult<bool>> AuthenticateSocketAsync(SocketConnection s)
        {
            //if (authProvider == null)
            //{
            //    return new CallResult<bool>(false, new ServerError("Need to create auth provider"));
            //}
            //return new CallResult<bool>(true, null);
            bool isSuccess = false;
            ServerError serverError = null;
            var authRequest = new BitmexSubscribeRequest() { Op = BitmexWebSocketOperation.AuthKeyExpires };
            var authParams = ((BitmexAuthenticationProvider)authProvider);
            // request = {"op": "authKeyExpires", "args": [API_KEY, expires, signature]}
            var expires = authParams.ApiExpires;
            authRequest.Args.Add(authParams.Credentials.Key.GetString());
            authRequest.Args.Add(expires);
            authRequest.Args.Add(authParams.Sign(authParams.CreateAuthPayload(HttpMethod.Get, "/realtime", expires)));
            await s.SendAndWaitAsync(authRequest,TimeSpan.FromSeconds(1),f=> 
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
            return new CallResult<bool>(isSuccess, null);
        }

      

        protected override async Task<bool> UnsubscribeAsync(SocketConnection connection, SocketSubscription s)
        {
            if (s.Request is BitmexSubscribeRequest)
            {
                await UnsubscribeInternal(connection, s);
            }
            return true;
        }

        public override async Task UnsubscribeAllAsync()
        {
            foreach (var c in this._subscriptions)
            {
                await c.CloseAsync().ConfigureAwait(false);
            }
            await base.UnsubscribeAllAsync();
        }

        protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
        {
            if (data.Type == JTokenType.String && data.ToString() == "pong")
            {
                callResult = null;
                return true;
            }
            callResult = Deserialize<T>(data);
            return callResult;
        }

        protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object> callResult)
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
            var response = Deserialize<BitmexSubscriptionResponse>(message, false);
            var bRequest = (BitmexSubscribeRequest)request;
            if (response)
            {
                if (bRequest.Args.Contains(response.Data.Subscribe))
                {
                    callResult = new CallResult<object>(response, response.Success ? null : new ServerError("Subscribtion was not success", response));

                    return true;
                }
            }
            callResult = new CallResult<object>(response, response.Success ? null : new ServerError("Subscribtion was not success", response));
            return false;
        }
        protected override bool MessageMatchesHandler(JToken message, object request)
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
        protected override bool MessageMatchesHandler(JToken message, string identifier)
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
        public CallResult<UpdateSubscription> SubscribeToOrderBookUpdates(Action<DataEvent<BitmexSocketEvent<BitmexOrderBookEntry>>> onData, string symbol = "", bool full = false) => SubscribeToOrderBookUpdatesAsync(onData, symbol, full).Result;

        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(Action<DataEvent<BitmexSocketEvent<BitmexOrderBookEntry>>> onData, string symbol = "", bool full = false)
        {
            BitmexSubscribtions orderbookType = full ? BitmexSubscribtions.OrderBookL2 : BitmexSubscribtions.OrderBookL2_25;
            return await SubscribeAsync(new BitmexSubscribeRequest().AddSubscription(orderbookType, symbol), onData);
        }

        public CallResult<UpdateSubscription> Subscribe(BitmexSubscribeRequest bitmexSubscribeRequest) => SubscribeAsync(bitmexSubscribeRequest).Result;

        ResponseTableToDataTypeMapping Map = new ResponseTableToDataTypeMapping();

        public async Task<CallResult<UpdateSubscription>> SubscribeAsync(BitmexSubscribeRequest bitmexSubscribeRequest)
        {
            CheckDoubleSendingRequest(bitmexSubscribeRequest);
            if (!bitmexSubscribeRequest.Args.Any())
            {
                log.Write(LogLevel.Warning, $"Not sending empty request {JsonConvert.SerializeObject(bitmexSubscribeRequest)}");
                return new CallResult<UpdateSubscription>(null, new ServerError("Not sending empty request ", bitmexSubscribeRequest));
            }
            var handler = new Action<DataEvent<string>>(dataEvent =>
            {
                var data = dataEvent.Data;
                var token = JToken.Parse(data);
                var table = (string)token["table"];

                if (String.IsNullOrEmpty(table) || !Map.Mappings.ContainsKey(table))
                {
                    var subscriptionResponse = (string)token["subscribe"];
                    if (!String.IsNullOrEmpty(subscriptionResponse))
                    {
                        var response = Deserialize<BitmexSubscriptionResponse>(data);
                        if (response)
                            _sendedSubscriptions.TryAdd(response.Data.Subscribe, response.Data.Request);
                        return;
                    }
                    else if (!String.IsNullOrEmpty((string)token["unsubscribe"]))
                    {
                        var response = Deserialize<BitmexSubscriptionResponse>(data);
                        if (response)
                            OnUnsubscribe(response.Data);
                        return;
                    }
                    else
                    {
                        log.Write(LogLevel.Warning, $"Unknown table [{table}] update catched at data {data}");
                        return;
                    }
                }
                BitmexSubscribtions updatedTable = Map.Mappings[table];

                switch (updatedTable)
                {
                    case BitmexSubscribtions.Announcements:
                        {
                            var result = Deserialize<BitmexSocketEvent<Announcement>>(token, false);
                            if (result.Success)
                                OnAnnouncementUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Chat:
                        {
                            var result = Deserialize<BitmexSocketEvent<Chat>>(token, false);
                            if (result.Success)
                                OnChatMessageUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Connected:
                        {
                            var result = Deserialize<BitmexSocketEvent<ConnectedUsers>>(token, false);
                            if (result.Success)
                                OnChatConnectionUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Funding:
                        {
                            var result = Deserialize<BitmexSocketEvent<Funding>>(token, false);
                            if (result.Success)
                                OnFundingUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Instrument:
                        {
                            var result = Deserialize<BitmexSocketEvent<Instrument>>(token, false);
                            if (result.Success)
                                OnInstrimentUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Insurance:
                        {
                            var result = Deserialize<BitmexSocketEvent<Insurance>>(token, false);
                            if (result.Success)
                                OnInsuranceUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Liquidation:
                        {
                            var result = Deserialize<BitmexSocketEvent<Liquidation>>(token, false);
                            if (result.Success)
                                OnLiquidationUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.OrderBookL2_25:
                        {
                            var result = Deserialize<BitmexSocketEvent<BitmexOrderBookEntry>>(token, false);
                            if (result.Success)
                            {
                                if (shouldUseIndexesAndTicksFromBitmex)
                                {
                                    foreach (var level in result.Data.Data)
                                    {
                                        var symbolTickInfo = GetIndexAndTickForInstrument(level.Symbol);
                                        level.SetPrice(symbolTickInfo.Index, symbolTickInfo.TickSize);
                                    }
                                }
                                OnOrderBookL2_25Update?.Invoke(result.Data);
                            }
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.OrderBookL2:
                        {
                            var result = Deserialize<BitmexSocketEvent<BitmexOrderBookEntry>>(token, false);
                            if (result.Success)
                            {
                                if (shouldUseIndexesAndTicksFromBitmex)
                                {
                                    foreach (var level in result.Data.Data)
                                    {
                                        var symbolTickInfo = GetIndexAndTickForInstrument(level.Symbol);
                                        level.SetPrice(symbolTickInfo.Index, symbolTickInfo.TickSize);
                                    }
                                }
                                OnorderBookL2Update?.Invoke(result.Data);
                            }
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.OrderBook10:
                        {
                            var result = Deserialize<BitmexSocketEvent<BitmexOrderBookL10>>(token, false);
                            if (result.Success)
                                OnOrderBook10Update?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.PublicNotifications:
                        {
                            var result = Deserialize<BitmexSocketEvent<GlobalNotification>>(token, false);
                            if (result.Success)
                                OnGlobalNotificationUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Quote:
                        {
                            var result = Deserialize<BitmexSocketEvent<Quote>>(token, false);
                            if (result.Success)
                                OnQuotesUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.QuoteBin1m:
                        {
                            var result = Deserialize<BitmexSocketEvent<Quote>>(token, false);
                            if (result.Success)
                                OnOneMinuteQuoteBinUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.QuoteBin5m:
                        {
                            var result = Deserialize<BitmexSocketEvent<Quote>>(token, false);
                            if (result.Success)
                                OnFiveMinuteQuoteBinUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.QuoteBin1h:
                        {
                            var result = Deserialize<BitmexSocketEvent<Quote>>(token, false);
                            if (result.Success)
                                OnOneHourQuoteBinUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.QuoteBin1d:
                        {
                            var result = Deserialize<BitmexSocketEvent<Quote>>(token, false);
                            if (result.Success)
                                OnDailyQuoteBinUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Settlement:
                        {
                            var result = Deserialize<BitmexSocketEvent<Settlement>>(token, false);
                            if (result.Success)
                                OnSettlementUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Trade:
                        {
                            var result = Deserialize<BitmexSocketEvent<Trade>>(token, false);
                            if (result.Success)
                                OnTradeUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.TradeBin1m:
                        {
                            var result = Deserialize<BitmexSocketEvent<TradeBin>>(token, false);
                            if (result.Success)
                                OnOneMinuteTradeBinUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.TradeBin5m:
                        {
                            var result = Deserialize<BitmexSocketEvent<TradeBin>>(token, false);
                            if (result.Success)
                                OnFiveMinuteTradeBinUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.TradeBin1h:
                        {
                            var result = Deserialize<BitmexSocketEvent<TradeBin>>(token, false);
                            if (result.Success)
                                OnOneHourTradeBinUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.TradeBin1d:
                        {
                            var result = Deserialize<BitmexSocketEvent<TradeBin>>(token, false);
                            if (result.Success)
                                OnDailyTradeBinUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Affiliate:
                        {
                            var result = Deserialize<BitmexSocketEvent<Affiliate>>(token, false);
                            if (result.Success)
                                OnUserAffiliatesUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Execution:
                        {
                            var result = Deserialize<BitmexSocketEvent<Execution>>(token, false);
                            if (result.Success)
                                OnUserExecutionsUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Order:
                        {
                            var result = Deserialize<BitmexSocketEvent<Order>>(token, false);
                            if (result.Success)
                                OnUserOrdersUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Margin:
                        {
                            var result = Deserialize<BitmexSocketEvent<Margin>>(token, false);
                            if (result.Success)
                                OnUserMarginUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Position:
                        {
                            var result = Deserialize<BitmexSocketEvent<Position>>(token, false);
                            if (result.Success)
                                OnUserPositionsUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Transact:
                        {
                            var result = Deserialize<BitmexSocketEvent<Transaction>>(token, false);
                            if (result.Success)
                                OnUserTransactionsUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Wallet:
                        {
                            var result = Deserialize<BitmexSocketEvent<Wallet>>(token, false);
                            if (result.Success)
                                OnUserWalletUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    default:
                        {
                            log.Write(LogLevel.Warning, $"Catched inknown table update: {data}");
                            break;
                        }
                }
            });
            return await SubscribeAsync(bitmexSubscribeRequest, handler);
        }
        private async Task<CallResult<UpdateSubscription>> SubscribeAsync<T>(BitmexSubscribeRequest request, Action<DataEvent<T>> onData)
        {
            request.Args.ValidateNotNull(nameof(request));
            var url = BaseAddress;

            var subscription = await SubscribeAsync(url, request, url + NextId(), authProvider != null, onData).ConfigureAwait(false);
            if (subscription)
            {
                if (request.Op == BitmexWebSocketOperation.Subscribe)
                    _subscriptions.Add(subscription.Data);
            }
            return subscription;
        }

        private void OnUnsubscribe(BitmexSubscriptionResponse response)
        {
            log.Write(LogLevel.Debug, $"Unsub: {JsonConvert.SerializeObject(response)}");
            if (!String.IsNullOrEmpty(response.Unsubscribe))
            {
                _sendedSubscriptions.TryRemove(response.Unsubscribe, out _);
                log.Write(LogLevel.Warning, $"{response.Unsubscribe} topic from {JsonConvert.SerializeObject(response.Request)} subscription was unsubscribed");
            }
        }

        private async Task UnsubscribeInternal(SocketConnection connection, SocketSubscription s)
        {
            var r = s.Request as BitmexSubscribeRequest;
            r.Op = BitmexWebSocketOperation.Unsubscribe;
            var result = await QueryAndWaitAsync<BitmexSubscriptionResponse>(connection, r);
            if (result && result.Data.Success && result.Data.Request.Args.Contains(result.Data.Unsubscribe))
            {
                _sendedSubscriptions.TryRemove(result.Data.Unsubscribe, out _);
                log.Write(LogLevel.Warning, $"{JsonConvert.SerializeObject(s.Request)} subscription was unsubscribed");
            }
            else
            {
                log.Write(LogLevel.Warning, $"{JsonConvert.SerializeObject(s.Request)} subscription was not unsubscribed: {result.Error?.Message}");
            }
            //await Subscribe<BitmexSubscriptionResponse>(r, OnUnsubscribe);
        }

        public BitmexInstrumentIndexWithTick GetIndexAndTickForInstrument(string instrument)
        {
            if (!shouldUseIndexesAndTicksFromBitmex)
            {
                return null;
            }
            if (!areInstrumentsLoaded)
            {
                GetInstrumentsTickerAndIndices().GetAwaiter().GetResult();
            }
            return instrumentsIndexesAndTicks[instrument];
        }

        private async Task GetInstrumentsTickerAndIndices()
        {
            instumentGetWaiter.WaitOne();
            if (areInstrumentsLoaded)
            {
                instumentGetWaiter.Set();
                return;
            }
            using (var bitmexClient = new BitmexClient(new BitmexClientOptions(isTestnet)))
            {
                var getByOnce = 500;
                var lastResponseItemCount = 0;
                var ind = 0;
                do
                {
                    var instruments = await bitmexClient.GetInstrumentsAsync(new Objects.Requests.BitmexRequestWithFilter().WithStartingFrom(ind).WithResultsCount(500).AddColumnsToGetInRequest(new string[] { "symbol", "tickSize" }));
                    if (instruments)
                    {
                        lastResponseItemCount = instruments.Data.Count;
                        for (int i = 0; i < instruments.Data.Count; i++)
                        {
                            instrumentsIndexesAndTicks.Add(instruments.Data[i].Symbol, new BitmexInstrumentIndexWithTick(ind++, instruments.Data[i].TickSize));
                        }
                    }
                    else
                    {
                        log.Write(LogLevel.Error, "Instrument indicies and price ticks for calculation was not obtained");
                        instumentGetWaiter.Set();
                        return;
                    }
                }
                while (lastResponseItemCount == getByOnce);
            }
            areInstrumentsLoaded = true;
            instumentGetWaiter.Set();
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
    }
}
