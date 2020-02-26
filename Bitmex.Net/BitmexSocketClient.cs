using Bitmex.Net.Client.Interfaces;
using Bitmex.Net.Client.Objects.Socket.Repsonses;
using Bitmex.Net.Client.Objects.Socket.Requests;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitmex.Net.Client
{
    public class BitmexSocketClient : SocketClient, IBitmexSocketClient
    {
        private static BitmexSocketClientOptions defaultOptions = new BitmexSocketClientOptions();
        private static BitmexSocketClientOptions DefaultOptions => defaultOptions.Copy<BitmexSocketClientOptions>();
        public BitmexSocketClient() : this(DefaultOptions)
        {
            this.log.Level = CryptoExchange.Net.Logging.LogVerbosity.Debug;
            this.log.UpdateWriters(new List<System.IO.TextWriter>() { new DebugTextWriter() });
            this.SocketCombineTarget = 1;

        }
        public BitmexSocketClient(BitmexSocketClientOptions bitmexSocketClientOptions) : base(bitmexSocketClientOptions, bitmexSocketClientOptions.ApiCredentials == null ? null : new BitmexAuthenticationProvider(bitmexSocketClientOptions.ApiCredentials))
        {
            ContinueOnQueryResponse = false;
            AddGenericHandler("Ping", (connection, token) => { });
            AddGenericHandler("Info", (connection, token) => { });
            this.log.Level = CryptoExchange.Net.Logging.LogVerbosity.Debug;
            this.log.UpdateWriters(new List<System.IO.TextWriter>() { new DebugTextWriter() });
        }


        protected override async Task<CallResult<bool>> AuthenticateSocket(SocketConnection s)
        {
            BitmexAuthenticationProvider p = (BitmexAuthenticationProvider)this.authProvider;
            bool isSucces = false;
            await s.SendAndWait(new BitmexSubscribeRequest(Objects.Socket.BitmexWebSocketOperation.AuthKeyExpires, p.CreateWebsocketSignatureParams()), TimeSpan.FromSeconds(5),
                token =>
                {
                    var obj = Deserialize<BitmexSubscriptionResponse>(token, false);
                    isSucces = token.ToString().Contains("success");
                    log.Write(LogVerbosity.Debug, token.ToString());
                    return true;
                });
            return new CallResult<bool>(isSucces,null);
        }

  

        protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
        {
            callResult = new CallResult<T>(Deserialize<T>(data).Data, null);
            return (data.ToString().Contains("pong")) || (request.ToString() == "Hello" && data["info"] != null);
        }

        protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object> callResult)
        {
            callResult = null;
            var greetings = message.ToObject<GreetingsMessage>();
            var response = Deserialize<BitmexSubscriptionResponse>(message, false);

            if (greetings.Info != null)
            {
                callResult = new CallResult<object>(response, response.Success ? null : new ServerError("Subscribtion was not success", response));
                return true;
            }
            var bRequest = (BitmexSubscribeRequest)request;
            if (response.Success && response.Data.Success && response.Data.Request?.Op == bRequest.Op)
            {
                foreach (var r in bRequest.Args)
                {
                    if (r.ToString().Contains(response.Data.Subscribe) || r.ToString().Contains(response.Data.Unsubscribe))
                    {
                        callResult = new CallResult<object>(response, response.Success ? null : new ServerError("Subscribtion was not success", response));

                        return true;
                    }
                }
            }
            if (message.Type != JTokenType.Object)
                return false;

            if (!response || !response.Data.Success || bRequest.Args.All(c => c.ToString() != response.Data.Subscribe) || String.IsNullOrEmpty(response.Data.Subscribe) || String.IsNullOrEmpty(response.Data.Unsubscribe))
            {
                return false;
            }

            callResult = new CallResult<object>(response, response.Success ? null : new ServerError("Subscribtion was not success", response));
            return true;
        }

        protected override bool MessageMatchesHandler(JToken message, object request)
        {
            //var response = Deserialize<BitmexSubscriptionResponse>(message, false);
            var bRequest = (BitmexSubscribeRequest)request;
            //if (response.Success && response.Data.Success && response.Data.Request?.Op == bRequest.Op)
            //{
            //    foreach (var r in bRequest.Args)
            //    {
            //        if (r.ToString().Contains(response.Data.Subscribe) || r.ToString().Contains(response.Data.Unsubscribe))
            //        {
            //            return true;
            //        }
            //    }
            //}
            if (message["error"] != null || (message["success"] != null && !(bool)message["success"]))
            {
                return false;
            }
            foreach (var r in bRequest.Args)
            {
                if (message["table"] == null)
                    continue;
                if (r.ToString().Contains((string)message["table"]))
                {
                    return true;
                }
            }
            return false;
        }

        protected override bool MessageMatchesHandler(JToken message, string identifier)
        {
            if (message.Type != JTokenType.Object)
                return false;

            if (identifier == "Ping" && message["op"] != null && (string)message["op"] == "Ping")
                return true;

            if (identifier == "Info" && message["info"] != null)
                return true;
            var subscribe = Deserialize<BitmexSubscriptionResponse>(message, false);
            if (subscribe)
            {
                return subscribe && subscribe.Data.Success;
            }

            return false;
        }

        protected override Task<bool> Unsubscribe(SocketConnection connection, SocketSubscription s)
        {
            throw new NotImplementedException();
        }

        public CallResult<UpdateSubscription> SubscribeToTrades(Action<BitmexTradeEvent> onData, string symbol = "") => SubscribeToTradesAsync(onData, symbol).Result;

        public async Task<CallResult<UpdateSubscription>> SubscribeToTradesAsync(Action<BitmexTradeEvent> onData, string symbol = "")
        {
            return await Subscribe(new BitmexSubscribeRequest("trade", symbol), null, true, onData).ConfigureAwait(false);
        }
    }
}
