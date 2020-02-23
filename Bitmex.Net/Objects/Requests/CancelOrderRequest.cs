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
        public string OrderId { get; set; }
        /// <summary>
        /// Client Order ID(s). See POST /order.
        /// </summary>
        public string ClOrdId { get; set; }
        /// <summary>
        /// Optional cancellation annotation. e.g. 'Spread Exceeded'.
        /// </summary>
        public string Text { get; set; }

    }
}
