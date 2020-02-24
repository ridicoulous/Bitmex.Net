using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{

    /// <summary>Raw Order and Balance Data</summary>

    public class Execution
    {
        [JsonProperty("execID", Required = Required.Always)]

        public System.Guid ExecID { get; set; }

        [JsonProperty("orderID")]
        public System.Guid? OrderID { get; set; }

        [JsonProperty("clOrdID")]
        public string ClOrdID { get; set; }

        [JsonProperty("clOrdLinkID")]
        public string ClOrdLinkID { get; set; }

        [JsonProperty("account")]
        public double? Account { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("side")]
        public string Side { get; set; }

        [JsonProperty("lastQty")]
        public double? LastQty { get; set; }

        [JsonProperty("lastPx")]
        public double? LastPx { get; set; }

        [JsonProperty("underlyingLastPx")]
        public double? UnderlyingLastPx { get; set; }

        [JsonProperty("lastMkt")]
        public string LastMkt { get; set; }

        [JsonProperty("lastLiquidityInd")]
        public string LastLiquidityInd { get; set; }

        [JsonProperty("simpleOrderQty")]
        public double? SimpleOrderQty { get; set; }

        [JsonProperty("orderQty")]
        public double? OrderQty { get; set; }

        [JsonProperty("price")]
        public double? Price { get; set; }

        [JsonProperty("displayQty")]
        public double? DisplayQty { get; set; }

        [JsonProperty("stopPx")]
        public double? StopPx { get; set; }

        [JsonProperty("pegOffsetValue")]
        public double? PegOffsetValue { get; set; }

        [JsonProperty("pegPriceType")]
        public string PegPriceType { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("settlCurrency")]
        public string SettlCurrency { get; set; }

        [JsonProperty("execType")]
        public string ExecType { get; set; }

        [JsonProperty("ordType")]
        public string OrdType { get; set; }

        [JsonProperty("timeInForce")]
        public string TimeInForce { get; set; }

        [JsonProperty("execInst")]
        public string ExecInst { get; set; }

        [JsonProperty("contingencyType")]
        public string ContingencyType { get; set; }

        [JsonProperty("exDestination")]
        public string ExDestination { get; set; }

        [JsonProperty("ordStatus")]
        public string OrdStatus { get; set; }

        [JsonProperty("triggered")]
        public string Triggered { get; set; }

        [JsonProperty("workingIndicator")]
        public bool? WorkingIndicator { get; set; }

        [JsonProperty("ordRejReason")]
        public string OrdRejReason { get; set; }

        [JsonProperty("simpleLeavesQty")]
        public double? SimpleLeavesQty { get; set; }

        [JsonProperty("leavesQty")]
        public double? LeavesQty { get; set; }

        [JsonProperty("simpleCumQty")]
        public double? SimpleCumQty { get; set; }

        [JsonProperty("cumQty")]
        public double? CumQty { get; set; }

        [JsonProperty("avgPx")]
        public double? AvgPx { get; set; }

        [JsonProperty("commission")]
        public double? Commission { get; set; }

        [JsonProperty("tradePublishIndicator")]
        public string TradePublishIndicator { get; set; }

        [JsonProperty("multiLegReportingType")]
        public string MultiLegReportingType { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("trdMatchID")]
        public System.Guid? TrdMatchID { get; set; }

        [JsonProperty("execCost")]
        public double? ExecCost { get; set; }

        [JsonProperty("execComm")]
        public double? ExecComm { get; set; }

        [JsonProperty("homeNotional")]
        public double? HomeNotional { get; set; }

        [JsonProperty("foreignNotional")]
        public double? ForeignNotional { get; set; }

        [JsonProperty("transactTime")]
        public System.DateTimeOffset? TransactTime { get; set; }

        [JsonProperty("timestamp")]
        public System.DateTimeOffset? Timestamp { get; set; }


    }

}

