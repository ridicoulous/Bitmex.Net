using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitmex.Net.Objects.Requests
{   
    public class CancelOrderRequest
    {
        /// <summary>
        /// Order ID(s).
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; }
        /// <summary>
        /// Client Order ID(s). See POST /order.
        /// </summary>
        [JsonProperty("clOrdId")]
        public string ClOrdId { get; set; }
        /// <summary>
        /// Optional cancellation annotation. e.g. 'Spread Exceeded'.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

    }
}
