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
        /// Either an exchangeOrderId or a clientOrderId must be provided,
        /// but not both
        /// </summary>
        /// <param name="exchangeOrderId">Id assigned from bitmex, 36 characters</param>
        /// <param name="clientOrderId">Your own id assigned at posting order, at most 36 characters</param>
        /// <param name="text">optional reason for cancelling</param>
        public CancelOrderRequest(string exchangeOrderId = null, string clientOrderId = null, string text = null)
        {
            if (!string.IsNullOrWhiteSpace(exchangeOrderId))
            {
                Id = new HashSet<string>() { exchangeOrderId };
            }
            else if (!string.IsNullOrWhiteSpace(clientOrderId))
            {
                ClientOrderId = new HashSet<string>() { clientOrderId };
            }
            else
            {
                throw new ArgumentNullException("An exchangeOrderId or a clientOrderId must be provided");
            }
            Text = text;
        }
        /// <summary>
        /// Cancels many orders
        /// Either a set of the exchangeOrderIds or of the clientOrderIds must be provided,
        /// but not both
        /// </summary>
        /// <param name="exchangeOrderIds"></param>
        /// <param name="clientOrderIds"></param>
        /// <param name="text"></param>
        public CancelOrderRequest(IEnumerable<string> exchangeOrderIds = null, IEnumerable<string> clientOrderIds = null, string text = null)
        {
            if (exchangeOrderIds != null && exchangeOrderIds.Any())
            {
                Id = new HashSet<string>(exchangeOrderIds);
            }
            else if (clientOrderIds != null && clientOrderIds.Any())
            {
                ClientOrderId = new HashSet<string>(clientOrderIds);
            }
            else
            {
                throw new ArgumentNullException("An exchangeOrderIds or a clientOrderIds must be provided");
            }
            Text = text;
        }

        /// <summary>
        /// Order ID(s).
        /// </summary>
        [JsonProperty("orderID")]
        public HashSet<string> Id { get; private set; }
        /// <summary>
        /// Client Order ID(s). See POST /order.
        /// </summary>
        [JsonProperty("clOrdID")]
        public HashSet<string> ClientOrderId { get; private set; }
        /// <summary>
        /// Optional cancellation annotation. e.g. 'Spread Exceeded'.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

    }
}
