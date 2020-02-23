using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{
    /// <summary>Placement, Cancellation, Amending, and History</summary>
    public class Order
    {
        [JsonProperty("orderID", Required = Required.Always)]

        public System.Guid OrderID { get; set; }

        [JsonProperty("clOrdID", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string ClOrdID { get; set; }

        [JsonProperty("clOrdLinkID", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string ClOrdLinkID { get; set; }

        [JsonProperty("account", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Account { get; set; }

        [JsonProperty("symbol", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Symbol { get; set; }

        [JsonProperty("side", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Side { get; set; }

        [JsonProperty("simpleOrderQty", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SimpleOrderQty { get; set; }

        [JsonProperty("orderQty", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? OrderQty { get; set; }

        [JsonProperty("price", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Price { get; set; }

        [JsonProperty("displayQty", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? DisplayQty { get; set; }

        [JsonProperty("stopPx", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? StopPx { get; set; }

        [JsonProperty("pegOffsetValue", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PegOffsetValue { get; set; }

        [JsonProperty("pegPriceType", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string PegPriceType { get; set; }

        [JsonProperty("currency", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty("settlCurrency", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string SettlCurrency { get; set; }

        [JsonProperty("ordType", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string OrdType { get; set; }

        [JsonProperty("timeInForce", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string TimeInForce { get; set; }

        [JsonProperty("execInst", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string ExecInst { get; set; }

        [JsonProperty("contingencyType", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string ContingencyType { get; set; }

        [JsonProperty("exDestination", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string ExDestination { get; set; }

        [JsonProperty("ordStatus", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string OrdStatus { get; set; }

        [JsonProperty("triggered", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Triggered { get; set; }

        [JsonProperty("workingIndicator", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? WorkingIndicator { get; set; }

        [JsonProperty("ordRejReason", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string OrdRejReason { get; set; }

        [JsonProperty("simpleLeavesQty", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SimpleLeavesQty { get; set; }

        [JsonProperty("leavesQty", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? LeavesQty { get; set; }

        [JsonProperty("simpleCumQty", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SimpleCumQty { get; set; }

        [JsonProperty("cumQty", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? CumQty { get; set; }

        [JsonProperty("avgPx", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? AvgPx { get; set; }

        [JsonProperty("multiLegReportingType", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string MultiLegReportingType { get; set; }

        [JsonProperty("text", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        [JsonProperty("transactTime", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? TransactTime { get; set; }

        [JsonProperty("timestamp", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? Timestamp { get; set; }


    }

}

