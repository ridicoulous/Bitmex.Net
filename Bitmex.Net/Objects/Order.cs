using System;
using Bitmex.Net.Client.Converters;
using CryptoExchange.Net.CommonObjects;
using Newtonsoft.Json;

namespace Bitmex.Net.Client.Objects
{
    /// <summary>Placement, Cancellation, Amending, and History</summary>
    public class BitmexOrder
    {
        [JsonProperty("orderID")]
        public string Id { get; set; }

        [JsonProperty("clOrdID")]
        public string ClOrdID { get; set; }

        [JsonProperty("clOrdLinkID")]
        public string ClOrdLinkID { get; set; }

        [JsonProperty("account")]
        public long Account { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("side")]
        [JsonConverter(typeof(BitmexOrderSideConverter))]
        public BitmexOrderSide? Side { get; set; }

        [JsonProperty("simpleOrderQty")]
        public decimal? SimpleOrderQty { get; set; }

        [JsonProperty("orderQty")]
        public decimal? OrderQty { get; set; }

        [JsonProperty("price")]
        public decimal? Price { get; set; }

        [JsonProperty("displayQty")]
        public decimal? DisplayQty { get; set; }

        [JsonProperty("stopPx")]
        public decimal? StopPx { get; set; }

        [JsonProperty("pegOffsetValue")]
        public decimal? PegOffsetValue { get; set; }

        [JsonProperty("pegPriceType")]
        public string PegPriceType { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("settlCurrency")]
        public string SettlCurrency { get; set; }

        [JsonProperty("ordType"), JsonConverter(typeof(BitmexOrderTypeConverter))]
        public BitmexOrderType? OrdType { get; set; }

        [JsonProperty("timeInForce")]
        public string TimeInForce { get; set; }

        [JsonProperty("execInst")]
        public string ExecInst { get; set; }

        [JsonProperty("contingencyType")]
        public string ContingencyType { get; set; }

        [JsonProperty("exDestination")]
        public string ExDestination { get; set; }

        [JsonProperty("ordStatus"), JsonConverter(typeof(BitmexOrderStatusConverter))]
        public BitmexOrderStatus? Status { get; set; }

        [JsonProperty("triggered")]
        public string Triggered { get; set; }

        [JsonProperty("workingIndicator")]
        public bool? WorkingIndicator { get; set; }

        [JsonProperty("ordRejReason")]
        public string OrdRejReason { get; set; }

        [JsonProperty("simpleLeavesQty")]
        public decimal? SimpleLeavesQty { get; set; }

        [JsonProperty("leavesQty")]
        public decimal? LeavesQty { get; set; }

        [JsonProperty("simpleCumQty")]
        public decimal? SimpleCumQty { get; set; }

        [JsonProperty("cumQty")]
        public decimal? CumQty { get; set; }

        [JsonProperty("avgPx")]
        public decimal? AvgPx { get; set; }

        [JsonProperty("multiLegReportingType")]
        public string MultiLegReportingType { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("transactTime")]
        public System.DateTime? TransactTime { get; set; }

        [JsonProperty("timestamp")]
        public System.DateTime? Timestamp { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }


        internal OrderId ToCryptoExchangeOrderId()
        {
            return new OrderId() { Id = Id, SourceObject = this };
        }
        internal Order ToCryptoExchangeOrder()
        {
            return new Order()
            {
                SourceObject = this,
                Id = Id,
                Price = Price,
                Quantity = OrderQty,
                QuantityFilled = CumQty,
                Symbol = Symbol,
                Timestamp = Timestamp.GetValueOrDefault(),
                Side = Side switch
                {
                    BitmexOrderSide.Buy => CommonOrderSide.Buy,
                    BitmexOrderSide.Sell => CommonOrderSide.Sell,
                    _ => throw new NotImplementedException("Undefined order side")
                },
                Type = OrdType switch
                {
                    BitmexOrderType.Limit => CommonOrderType.Limit,
                    BitmexOrderType.LimitIfTouched => CommonOrderType.Limit,
                    BitmexOrderType.Market => CommonOrderType.Market,
                    BitmexOrderType.MarketIfTouched => CommonOrderType.Market,
                    BitmexOrderType.StopLimit => CommonOrderType.Other,
                    BitmexOrderType.Stop => CommonOrderType.Other,
                    _ => CommonOrderType.Other
                },
                Status = Status switch
                {
                    BitmexOrderStatus.Canceled => CommonOrderStatus.Canceled,
                    BitmexOrderStatus.Rejected => CommonOrderStatus.Canceled,
                    BitmexOrderStatus.Filled => CommonOrderStatus.Filled,
                    BitmexOrderStatus.New => CommonOrderStatus.Active,
                    BitmexOrderStatus.PartiallyFilled => CommonOrderStatus.Active,
                    _ => throw new NotImplementedException("Undefined order status")
                },
            };

        }
    }

}

