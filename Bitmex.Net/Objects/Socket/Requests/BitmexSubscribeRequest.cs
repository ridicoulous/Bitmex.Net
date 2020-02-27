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
        [JsonProperty("args")]
        public virtual List<object> Args { get; set; } = new List<object> { };
    }
}
