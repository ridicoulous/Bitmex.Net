using Bitmex.Net.Client.Converters;
using Bitmex.Net.Client.Helpers.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bitmex.Net.Client.Objects.Socket.Requests
{
    public class BitmexSubscribeRequest : BitmexBaseMessage
    {
        public BitmexSubscribeRequest()
        {
            Op = BitmexWebSocketOperation.Subscribe;
        }    

        public BitmexSubscribeRequest(params object[] args)
        {
            Op = BitmexWebSocketOperation.Subscribe;
            Args.AddRange(args);
        }
        [JsonProperty("args")]        
        public List<object> Args { get; set; } = new List<object> { };

        public BitmexSubscribeRequest AddSubscription(BitmexSubscribtions table, string symbol = null)
        {
            string endpoint = JsonConvert.SerializeObject(table, new BitmexWebsocketTableConverter(false));
            if (!String.IsNullOrEmpty(symbol))
            {
                endpoint += $":{symbol}";
            }
            if (!Args.Contains(endpoint))
            {
                Args.Add(endpoint);
            }
            return this;
        }
        public BitmexSubscribeRequest UndoSubscription(BitmexSubscribtions table, string symbol = null)
        {
            Op = BitmexWebSocketOperation.Unsubscribe;
            string endpoint = JsonConvert.SerializeObject(table, new BitmexWebsocketTableConverter(false));
            if (!String.IsNullOrEmpty(symbol))
            {
                endpoint += $":{symbol}";
            }
            if (!Args.Contains(endpoint))
            {
                Args.Add(endpoint);
            }
            return this;
        }
        public BitmexSubscribeRequest CreateUnsubscribeRequest()
        {
            return new()
            {
                Args = this.Args,
                Op = BitmexWebSocketOperation.Unsubscribe
            };
        }

        /// <summary>
        /// Moves the subscription topics from the request that should be sent to the different endpoint into one more BitmexSubscribeRequest
        /// </summary>
        /// <returns>new BitmexSubscribeRequest with nontrade topics only</returns>
        internal BitmexSubscribeRequest PopNonTradeSubscriptions()
        {
            var nonTradeArgs = Args.Where(arg => arg.IsItNonTradeSubscriptionString()).ToArray(); //ToArray() because BitmexSubscribeRequest(params ...) ctor requires array
            Args.RemoveAll(x => nonTradeArgs.Contains(x));
            return new BitmexSubscribeRequest(nonTradeArgs);
        }

    }
}
