using Bitmex.Net.Client.Objects.Socket.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitmex.Net.Client.Objects.Socket.Repsonses
{
    public class BitmexSubscriptionResponse : BitmexBaseMessage
    {
        /// <summary>
        /// Subscribed topics
        /// </summary>
        [JsonProperty("subscribe")]
        public string Subscribe { get; set; }

        /// <summary>
        /// Unsubscribed topics
        /// </summary>
        [JsonProperty("unsubscribe")]
        public string Unsubscribe { get; set; }
        /// <summary>
        /// Returns true if subscribe or unsubscribe succeed
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }
        //[JsonProperty("request")]
        //public BitmexSubscribeRequest Request { get; set; }
    }

}
