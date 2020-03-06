using Bitmex.Net.Client.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitmex.Net.Client.Objects.Socket.Requests
{
    public class BitmexSubscribeRequest : BitmexBaseMessage
    {
        public BitmexSubscribeRequest()
        {
            Op = BitmexWebSocketOperation.Subscribe;
        }
        public BitmexSubscribeRequest(string topic, string symbol = "")
        {
            Op = BitmexWebSocketOperation.Subscribe;
            string table = topic;
            if (!String.IsNullOrEmpty(symbol))
            {
                table += $":{symbol}";
            }
            Args.Add(table);
        }
        public BitmexSubscribeRequest(BitmexWebSocketOperation op, params object[] args)
        {
            Op = op;
            foreach (var a in args)
            {
                Args.Add(a);
            }
        }
        public BitmexSubscribeRequest(params object[] args)
        {
            Op = BitmexWebSocketOperation.Subscribe;
        }
        [JsonProperty("args")]
        public virtual List<object> Args { get; set; } = new List<object> { };
        public BitmexSubscribeRequest Subscribe(BitmexSubscribtions table, string symbol = null)
        {
            string endpoint = JsonConvert.SerializeObject(table, new BitmexWebsocketTableConverter(false));
            if (!String.IsNullOrEmpty(symbol))
            {
                endpoint += $":{symbol}";
            }
            Args.Add(endpoint);
            return this;
        }
    }
}
