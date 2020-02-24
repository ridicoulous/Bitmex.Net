using Bitmex.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitmex.Net.Objects.Requests
{
    /// <summary>
    /// Object with patameters to place order
    /// </summary>
    public class PlaceOrderRequest
    {
        public PlaceOrderRequest(string symbol)
        {
            Symbol = symbol;
        }
        /// <summary>
        /// Instrument symbol. e.g. 'XBTUSD'.
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        /// <summary>
        /// Order side. Valid options: Buy, Sell. Defaults to 'Buy' unless orderQty is negative.
        /// </summary>
        [JsonProperty("side"),JsonConverter(typeof(BitmexOrderSideConverter))]
        public BitmexOrderSide? Side { get; set; }
        /// <summary>
        /// Order quantity in units of the instrument(i.e.contracts).
        /// </summary>
        [JsonProperty("orderQty")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// Optional limit price for 'Limit', 'StopLimit', and 'LimitIfTouched' orders.
        /// </summary>
        [JsonProperty("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// Optional quantity to display in the book. Use 0 for a fully hidden order.
        /// </summary>
        [JsonProperty("displayQty")]
        public decimal? DisplayQuantity { get; set; }
        /// <summary>
        /// Optional trigger price for 'Stop', 'StopLimit', 'MarketIfTouched', and 'LimitIfTouched' orders. Use a price below the current price for stop-sell orders and buy-if-touched orders. Use execInst of 'MarkPrice' or 'LastPrice' to define the current price used for triggering.
        /// </summary>
        [JsonProperty("stopPx")]
        public decimal? StopPx { get; set; }
        /// <summary>
        /// Optional Client Order ID. This clOrdID will come back on the order and any related executions.
        /// </summary>
        [JsonProperty("clOrdID")]
        public string ClientOrderId { get; set; }
        /// <summary>
        /// Optional trailing offset from the current price for 'Stop', 'StopLimit', 'MarketIfTouched', and 'LimitIfTouched' orders; use a negative offset for stop-sell orders and buy-if-touched orders. Optional offset from the peg price for 'Pegged' orders.
        /// </summary>       
        [JsonProperty("pegOffsetValue")]
        public decimal? PegOffsetValue { get; set; }
        /// <summary>
        /// Optional peg price type. Valid options: LastPeg, MidPricePeg, MarketPeg, PrimaryPeg, TrailingStopPeg.
        /// </summary>
        [JsonProperty("pegPriceType")]
        public BitmexPegPriceType? PegPriceType { get; set; }
        /// <summary>
        /// Order type. Valid options: Market, Limit, Stop, StopLimit, MarketIfTouched, LimitIfTouched, Pegged. Defaults to 'Limit' when price is specified. Defaults to 'Stop' when stopPx is specified. Defaults to 'StopLimit' when price and stopPx are specified.
        /// </summary>        
        [JsonProperty("ordType"), JsonConverter(typeof(BitmexOrderTypeConverter))]
        public BitmexOrderType? BitmexOrderType { get; set; }
        /// <summary>
        /// Time in force. Valid options: Day, GoodTillCancel, ImmediateOrCancel, FillOrKill.
        /// Defaults to 'GoodTillCancel' for 'Limit', 'StopLimit', and 'LimitIfTouched' orders.
        /// </summary>
        [JsonProperty("timeInForce")]
        public BitmexTimeInForce? TimeInForce { get; set; }
        /// <summary>
        /// Optional execution instructions. Valid options: ParticipateDoNotInitiate, AllOrNone, 
        /// MarkPrice, IndexPrice, LastPrice, Close, ReduceOnly, Fixed.
        /// 'AllOrNone' instruction requires displayQty to be 0. 'MarkPrice', 'IndexPrice' or
        /// 'LastPrice' instruction valid for 'Stop', 'StopLimit', 'MarketIfTouched', and 
        /// 'LimitIfTouched' orders.
        /// </summary>
        [JsonProperty("execInst")]
        public string ExecInst { get; set; }
        /// <summary>
        /// Optional order annotation. e.g. 'Take profit'.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

    }
}
