using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{

    /// <summary>Raw Order and Balance Data</summary>

    public class Execution
    {
        [JsonProperty("execID", Required = Required.Always)]

        public System.Guid ExecID { get; set; }

        [JsonProperty("orderID", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid? OrderID { get; set; }

        [JsonProperty("clOrdID", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string ClOrdID { get; set; }

        [JsonProperty("clOrdLinkID", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string ClOrdLinkID { get; set; }

        [JsonProperty("account", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? Account { get; set; }

        [JsonProperty("symbol", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Symbol { get; set; }

        [JsonProperty("side", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Side { get; set; }

        [JsonProperty("lastQty", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? LastQty { get; set; }

        [JsonProperty("lastPx", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? LastPx { get; set; }

        [JsonProperty("underlyingLastPx", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? UnderlyingLastPx { get; set; }

        [JsonProperty("lastMkt", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string LastMkt { get; set; }

        [JsonProperty("lastLiquidityInd", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string LastLiquidityInd { get; set; }

        [JsonProperty("simpleOrderQty", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? SimpleOrderQty { get; set; }

        [JsonProperty("orderQty", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? OrderQty { get; set; }

        [JsonProperty("price", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? Price { get; set; }

        [JsonProperty("displayQty", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? DisplayQty { get; set; }

        [JsonProperty("stopPx", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? StopPx { get; set; }

        [JsonProperty("pegOffsetValue", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? PegOffsetValue { get; set; }

        [JsonProperty("pegPriceType", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string PegPriceType { get; set; }

        [JsonProperty("currency", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty("settlCurrency", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string SettlCurrency { get; set; }

        [JsonProperty("execType", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string ExecType { get; set; }

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
        public double? SimpleLeavesQty { get; set; }

        [JsonProperty("leavesQty", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? LeavesQty { get; set; }

        [JsonProperty("simpleCumQty", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? SimpleCumQty { get; set; }

        [JsonProperty("cumQty", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? CumQty { get; set; }

        [JsonProperty("avgPx", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? AvgPx { get; set; }

        [JsonProperty("commission", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? Commission { get; set; }

        [JsonProperty("tradePublishIndicator", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string TradePublishIndicator { get; set; }

        [JsonProperty("multiLegReportingType", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string MultiLegReportingType { get; set; }

        [JsonProperty("text", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        [JsonProperty("trdMatchID", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid? TrdMatchID { get; set; }

        [JsonProperty("execCost", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? ExecCost { get; set; }

        [JsonProperty("execComm", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? ExecComm { get; set; }

        [JsonProperty("homeNotional", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? HomeNotional { get; set; }

        [JsonProperty("foreignNotional", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? ForeignNotional { get; set; }

        [JsonProperty("transactTime", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? TransactTime { get; set; }

        [JsonProperty("timestamp", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? Timestamp { get; set; }


    }

}

