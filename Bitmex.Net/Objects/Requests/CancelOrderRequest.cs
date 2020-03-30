using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace   Bitmex.Net.Client.Objects.Requests
{
    /// <summary>
    /// Parameters for order cancelling. You must provide orderId
    /// </summary>
    public class CancelOrderRequest
    {
        /// <summary>
        /// cancel single order
        /// </summary>
        /// <param name="exchangeOrderId">Id assigned from bitmex</param>
        /// <param name="clientOrderId">Your own id assigned at posting order</param>
        /// <param name="text">optional reason for cancelling</param>
        public CancelOrderRequest(string exchangeOrderId = null, string clientOrderId = null, string text = null)
        {
            Id = exchangeOrderId;
            ClientOrderId = clientOrderId;
            Text = text;
        }
        /// <summary>
        /// Cancels many orders
        /// </summary>
        /// <param name="exchangeOrderIds"></param>
        /// <param name="clientOrderIds"></param>
        /// <param name="text"></param>
        public CancelOrderRequest(string[] exchangeOrderIds, string[] clientOrderIds = null, string text = null)
        {
            if (exchangeOrderIds != null && exchangeOrderIds.Any())
            {
                Id = String.Join(",", exchangeOrderIds);
            }
            if (clientOrderIds != null && clientOrderIds.Any())
            {
                ClientOrderId = String.Join(",", clientOrderIds);
            }
            Text = text;
        }

        /// <summary>
        /// Order ID(s).
        /// </summary>
        [JsonProperty("orderID")]
        public string Id { get; set; }
        /// <summary>
        /// Client Order ID(s). See POST /order.
        /// </summary>
        [JsonProperty("clOrdID")]
        public string ClientOrderId { get; set; }
        /// <summary>
        /// Optional cancellation annotation. e.g. 'Spread Exceeded'.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

    }
}
