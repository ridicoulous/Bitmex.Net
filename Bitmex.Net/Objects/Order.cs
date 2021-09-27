using Bitmex.Net.Client.Converters;
using CryptoExchange.Net.ExchangeInterfaces;
using Newtonsoft.Json;
using System;

namespace Bitmex.Net.Client.Objects
{
    /// <summary>Placement, Cancellation, Amending, and History</summary>
    public class Order : ICommonOrder
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
        public BitmexOrderSide Side { get; set; }

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
        public BitmexOrderType OrdType { get; set; }

        [JsonProperty("timeInForce")]
        public string TimeInForce { get; set; }

        [JsonProperty("execInst")]
        public string ExecInst { get; set; }

        [JsonProperty("contingencyType")]
        public string ContingencyType { get; set; }

        [JsonProperty("exDestination")]
        public string ExDestination { get; set; }

        [JsonProperty("ordStatus"), JsonConverter(typeof(BitmexOrderStatusConverter))]
        public BitmexOrderStatus Status { get; set; }

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

        public string CommonId => Id.ToString();

        public string CommonSymbol => Symbol;

        public decimal CommonPrice => Price.GetValueOrDefault();

        public decimal CommonQuantity => OrderQty.GetValueOrDefault();

        public IExchangeClient.OrderStatus CommonStatus => Status switch
        {
            BitmexOrderStatus.New => IExchangeClient.OrderStatus.Active,
            BitmexOrderStatus.PartiallyFilled => IExchangeClient.OrderStatus.Filled,
            BitmexOrderStatus.Filled => IExchangeClient.OrderStatus.Filled,
            BitmexOrderStatus.Rejected => IExchangeClient.OrderStatus.Canceled,
            BitmexOrderStatus.Canceled => IExchangeClient.OrderStatus.Canceled,
            BitmexOrderStatus.Undefined => IExchangeClient.OrderStatus.Canceled,
            _ => IExchangeClient.OrderStatus.Canceled
        };

        public bool IsActive => throw new NotImplementedException();

        public IExchangeClient.OrderSide CommonSide => throw new NotImplementedException();

        public IExchangeClient.OrderType CommonType => throw new NotImplementedException();

        public DateTime CommonOrderTime => throw new NotImplementedException();
    }

}

