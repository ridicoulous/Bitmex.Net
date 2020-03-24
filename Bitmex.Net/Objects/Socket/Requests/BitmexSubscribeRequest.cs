using Bitmex.Net.Client.Converters;
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
        public BitmexSubscribeRequest(BitmexWebSocketOperation op, params string[] args)
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
        //public virtual string Args { get =>JsonConvert.SerializeObject(_args).Replace("\\",""); set => _args = value.Split(',').ToList(); } 
        public List<string> Args { get; set; } = new List<string> { };

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
    }
}
