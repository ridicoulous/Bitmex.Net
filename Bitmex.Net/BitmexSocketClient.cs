using Bitmex.Net.Client.Interfaces;
using Bitmex.Net.Client.Objects;
using Bitmex.Net.Client.Objects.Socket.Repsonses;
using Bitmex.Net.Client.Objects.Socket.Requests;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        }
        public BitmexSocketClient(BitmexSocketClientOptions bitmexSocketClientOptions) : base(bitmexSocketClientOptions, bitmexSocketClientOptions.ApiCredentials == null ? null : new BitmexAuthenticationProvider(bitmexSocketClientOptions.ApiCredentials))
        {
            this.log.Level = CryptoExchange.Net.Logging.LogVerbosity.Debug;
            this.log.UpdateWriters(new List<System.IO.TextWriter>() { new DebugTextWriter() });
            AddGenericHandler("Info", (c,t) => { GenericInfo(c, t); });
        }
        private int limit = 0;
        private void GenericInfo(SocketConnection socketConnection, JToken token)
        {
            if (token["info"] != null)
            {
                limit = (int)token["limit"]["remaining"];
            }
        }
        protected override IWebsocket CreateSocket(string address)
        {
            Dictionary<string, string> empty = new Dictionary<string, string>();
            return SocketFactory.CreateWebsocket(this.log, address, empty, this.authProvider == null ? empty : this.authProvider.AddAuthenticationToHeaders("bitmex.com/realtime", HttpMethod.Get, null, true));
        }
        protected override async Task<CallResult<bool>> AuthenticateSocket(SocketConnection s)
        {
            if (authProvider == null)
            {
                return new CallResult<bool>(false, new ServerError("Need to create auth provider"));
            }

            return new CallResult<bool>(true, null);
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
            var bRequest = (BitmexSubscribeRequest)request;
            if (bRequest == null)
                return false;

            foreach (var r in bRequest.Args)
            {
                if (message["table"] == null)
                    continue;
                if (r.ToString().Contains((string)message["table"]))
                {
                    return true;
                }
            }
            if (message["error"] != null || (message["success"] != null && !(bool)message["success"]))
            {
                return false;
            }
            return false;
        }

        protected override bool MessageMatchesHandler(JToken message, string identifier)
        {
            //if (message["info"] != null&&identifier=="Info")
            //{
            //    limit = (int)message["limit"]["remaining"];
            //    return true;
            //}
            return true;
        }

        protected override Task<bool> Unsubscribe(SocketConnection connection, SocketSubscription s)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Subscribe to live trades
        /// </summary>
        /// <param name="onData">Handler of trade events</param>
        /// <param name="symbol">if not setted, trades for each active instrument will be pushed</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToAllTrades(Action<BitmexTradeEvent> onData, string symbol = "") => SubscribeToAllTradesAsync(onData, symbol).Result;
        /// <summary>
        /// Subscribe to live trades
        /// </summary>
        /// <param name="onData">Handler of trade events</param>
        /// <param name="symbol">if not setted, trades for each active instrument will be pushed</param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToAllTradesAsync(Action<BitmexTradeEvent> onData, string symbol = "")
        {          
            return await Subscribe(new BitmexSubscribeRequest("trade", symbol), null, true, onData).ConfigureAwait(false);
        }
        /// <summary>
        /// Subscribe to live user orders updates
        /// </summary>
        /// <param name="onData">order updates handler</param>
        /// <param name="symbol">filter orders by symbol or get all updates by all intruments</param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToUserOrderUpdates(Action<BitmexOrderUpdateEvent> onData, string symbol = "") => SubscribeToUserOrderUpdatesAsync(onData, symbol).Result;
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserOrderUpdatesAsync(Action<BitmexOrderUpdateEvent> onData, string symbol = "")
        {           
            return await Subscribe(new BitmexSubscribeRequest("order", symbol), null, true, onData).ConfigureAwait(false);
        }
        /// <summary>
        /// subscribe to orderbook updates
        /// </summary>
        /// <param name="onData">orderbook</param>
        /// <param name="symbol"></param>
        /// <param name="orderBookLevelType"></param>
        /// <returns></returns>
        public CallResult<UpdateSubscription> SubscribeToOrderBookUpdates(Action<BitmexOrderBookUpdateEvent> onData, string symbol = "", string orderBookLevelType = "orderBookL2_25") => SubscribeToOrderBookUpdatesAsync(onData, symbol).Result;

        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(Action<BitmexOrderBookUpdateEvent> onData, string symbol = "", string orderBookLevel = "orderBookL2_25")
        {           
            return await Subscribe(new BitmexSubscribeRequest(orderBookLevel, symbol), null, false, onData).ConfigureAwait(false);
        }

        public CallResult<UpdateSubscription> SubscribeToUserExecutions(Action<BitmexExecutionEvent> onData, string symbol = "") => SubscribeToUserExecutionsAsync(onData, symbol).Result;
        /// <summary>
        /// Subscribe to user executions
        /// </summary>
        /// <param name="onData"></param>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserExecutionsAsync(Action<BitmexExecutionEvent> onData, string symbol = "")
        {
           
            return await Subscribe(new BitmexSubscribeRequest("execution", symbol), null, true, onData);
        }
    }
}
