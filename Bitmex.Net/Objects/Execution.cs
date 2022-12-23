using System;
using Bitmex.Net.Client.Converters;
using CryptoExchange.Net.CommonObjects;
using Newtonsoft.Json;

namespace    Bitmex.Net.Client.Objects
{

    /// <summary>Raw Order and Balance Data</summary>

    public class Execution
    {
        [JsonProperty("execID", Required = Required.Always)]

        public string ExecID { get; set; }

        [JsonProperty("orderID")]
        public string OrderID { get; set; }

        [JsonProperty("clOrdID")]
        public string ClOrdID { get; set; }

        [JsonProperty("clOrdLinkID")]
        public string ClOrdLinkID { get; set; }

        [JsonProperty("account")]
        public decimal? Account { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("side"), JsonConverter(typeof(BitmexOrderSideConverter))]
        public BitmexOrderSide Side { get; set; }

        [JsonProperty("lastQty")]
        public decimal? LastQty { get; set; }

        [JsonProperty("lastPx")]
        public decimal? LastPx { get; set; }

        [JsonProperty("lastLiquidityInd")]
        public string LastLiquidityInd { get; set; }

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

        [JsonProperty("ordStatus")]
        public string OrdStatus { get; set; }

        [JsonProperty("triggered")]
        public string Triggered { get; set; }

        [JsonProperty("workingIndicator")]
        public bool? WorkingIndicator { get; set; }

        [JsonProperty("ordRejReason")]
        public string OrdRejReason { get; set; }

        [JsonProperty("leavesQty")]
        public decimal? LeavesQty { get; set; }

        [JsonProperty("cumQty")]
        public decimal? CumQty { get; set; }

        [JsonProperty("avgPx")]
        public decimal? AvgPx { get; set; }

        [JsonProperty("commission")]
        public decimal? Commission { get; set; }

        [JsonProperty("tradePublishIndicator")]
        public string TradePublishIndicator { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("trdMatchID")]
        public string TrdMatchID { get; set; }

        [JsonProperty("execCost")]
        public decimal? ExecCost { get; set; }

        [JsonProperty("execComm")]
        public decimal? ExecComm { get; set; }

        [JsonProperty("homeNotional")]
        public decimal? HomeNotional { get; set; }

        [JsonProperty("foreignNotional")]
        public decimal? ForeignNotional { get; set; }

        [JsonProperty("transactTime")]
        public System.DateTime? TransactTime { get; set; }

        [JsonProperty("timestamp")]
        public System.DateTime? Timestamp { get; set; }


        internal UserTrade ToCryptoExchangeUserTrade()
        {
            return new UserTrade()
            {
                SourceObject = this,
                Fee = ExecComm,
                FeeAsset = SettlCurrency,
                Id = ExecID,
                OrderId = OrderID,
                Price = Price.GetValueOrDefault(),
                Quantity = LastQty.GetValueOrDefault(),
                Symbol = Symbol,
                Timestamp = Timestamp.GetValueOrDefault()
            };
        }
    }

}

