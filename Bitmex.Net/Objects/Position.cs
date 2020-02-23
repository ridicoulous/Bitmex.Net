using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{
   


   
   
        /// <summary>Summary of Open and Closed Positions</summary>

        public class Position
    {
        [JsonProperty("account", Required = Required.Always)]
        public decimal Account { get; set; } = 0;

        [JsonProperty("symbol", Required = Required.Always)]
      
        public string Symbol { get; set; }

        [JsonProperty("currency", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty("underlying", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Underlying { get; set; }

        [JsonProperty("quoteCurrency", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string QuoteCurrency { get; set; }

        [JsonProperty("commission", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Commission { get; set; } =0;

        [JsonProperty("initMarginReq", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? InitMarginReq { get; set; } =0;

        [JsonProperty("maintMarginReq", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? MaintMarginReq { get; set; } =0;

        [JsonProperty("riskLimit", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? RiskLimit { get; set; } = 0;

        [JsonProperty("leverage", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Leverage { get; set; } =0;

        [JsonProperty("crossMargin", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? CrossMargin { get; set; }

        [JsonProperty("deleveragePercentile", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? DeleveragePercentile { get; set; } =0;

        [JsonProperty("rebalancedPnl", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? RebalancedPnl { get; set; } = 0;

        [JsonProperty("prevRealisedPnl", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PrevRealisedPnl { get; set; } = 0;

        [JsonProperty("prevUnrealisedPnl", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PrevUnrealisedPnl { get; set; } = 0;

        [JsonProperty("prevClosePrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PrevClosePrice { get; set; } =0;

        [JsonProperty("openingTimestamp", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? OpeningTimestamp { get; set; }

        [JsonProperty("openingQty", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? OpeningQty { get; set; } = 0;

        [JsonProperty("openingCost", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? OpeningCost { get; set; } = 0;

        [JsonProperty("openingComm", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? OpeningComm { get; set; } = 0;

        [JsonProperty("openOrderBuyQty", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? OpenOrderBuyQty { get; set; } = 0;

        [JsonProperty("openOrderBuyCost", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? OpenOrderBuyCost { get; set; } = 0;

        [JsonProperty("openOrderBuyPremium", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? OpenOrderBuyPremium { get; set; } = 0;

        [JsonProperty("openOrderSellQty", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? OpenOrderSellQty { get; set; } = 0;

        [JsonProperty("openOrderSellCost", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? OpenOrderSellCost { get; set; } = 0;

        [JsonProperty("openOrderSellPremium", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? OpenOrderSellPremium { get; set; } = 0;

        [JsonProperty("execBuyQty", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ExecBuyQty { get; set; } = 0;

        [JsonProperty("execBuyCost", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ExecBuyCost { get; set; } = 0;

        [JsonProperty("execSellQty", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ExecSellQty { get; set; } = 0;

        [JsonProperty("execSellCost", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ExecSellCost { get; set; } = 0;

        [JsonProperty("execQty", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ExecQty { get; set; } = 0;

        [JsonProperty("execCost", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ExecCost { get; set; } = 0;

        [JsonProperty("execComm", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ExecComm { get; set; } = 0;

        [JsonProperty("currentTimestamp", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? CurrentTimestamp { get; set; }

        [JsonProperty("currentQty", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? CurrentQty { get; set; } = 0;

        [JsonProperty("currentCost", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? CurrentCost { get; set; } = 0;

        [JsonProperty("currentComm", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? CurrentComm { get; set; } = 0;

        [JsonProperty("realisedCost", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? RealisedCost { get; set; } = 0;

        [JsonProperty("unrealisedCost", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnrealisedCost { get; set; } = 0;

        [JsonProperty("grossOpenCost", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? GrossOpenCost { get; set; } = 0;

        [JsonProperty("grossOpenPremium", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? GrossOpenPremium { get; set; } = 0;

        [JsonProperty("grossExecCost", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? GrossExecCost { get; set; } = 0;

        [JsonProperty("isOpen", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsOpen { get; set; }

        [JsonProperty("markPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? MarkPrice { get; set; } =0;

        [JsonProperty("markValue", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? MarkValue { get; set; } = 0;

        [JsonProperty("riskValue", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? RiskValue { get; set; } = 0;

        [JsonProperty("homeNotional", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? HomeNotional { get; set; } =0;

        [JsonProperty("foreignNotional", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ForeignNotional { get; set; } =0;

        [JsonProperty("posState", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string PosState { get; set; }

        [JsonProperty("posCost", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PosCost { get; set; } = 0;

        [JsonProperty("posCost2", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PosCost2 { get; set; } = 0;

        [JsonProperty("posCross", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PosCross { get; set; } = 0;

        [JsonProperty("posInit", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PosInit { get; set; } = 0;

        [JsonProperty("posComm", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PosComm { get; set; } = 0;

        [JsonProperty("posLoss", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PosLoss { get; set; } = 0;

        [JsonProperty("posMargin", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PosMargin { get; set; } = 0;

        [JsonProperty("posMaint", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PosMaint { get; set; } = 0;

        [JsonProperty("posAllowance", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PosAllowance { get; set; } = 0;

        [JsonProperty("taxableMargin", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? TaxableMargin { get; set; } = 0;

        [JsonProperty("initMargin", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? InitMargin { get; set; } = 0;

        [JsonProperty("maintMargin", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? MaintMargin { get; set; } = 0;

        [JsonProperty("sessionMargin", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SessionMargin { get; set; } = 0;

        [JsonProperty("targetExcessMargin", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? TargetExcessMargin { get; set; } = 0;

        [JsonProperty("varMargin", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? VarMargin { get; set; } = 0;

        [JsonProperty("realisedGrossPnl", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? RealisedGrossPnl { get; set; } = 0;

        [JsonProperty("realisedTax", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? RealisedTax { get; set; } = 0;

        [JsonProperty("realisedPnl", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? RealisedPnl { get; set; } = 0;

        [JsonProperty("unrealisedGrossPnl", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnrealisedGrossPnl { get; set; } = 0;

        [JsonProperty("longBankrupt", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? LongBankrupt { get; set; } = 0;

        [JsonProperty("shortBankrupt", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ShortBankrupt { get; set; } = 0;

        [JsonProperty("taxBase", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? TaxBase { get; set; } = 0;

        [JsonProperty("indicativeTaxRate", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? IndicativeTaxRate { get; set; } =0;

        [JsonProperty("indicativeTax", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? IndicativeTax { get; set; } = 0;

        [JsonProperty("unrealisedTax", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnrealisedTax { get; set; } = 0;

        [JsonProperty("unrealisedPnl", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnrealisedPnl { get; set; } = 0;

        [JsonProperty("unrealisedPnlPcnt", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnrealisedPnlPcnt { get; set; } =0;

        [JsonProperty("unrealisedRoePcnt", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnrealisedRoePcnt { get; set; } =0;

        [JsonProperty("simpleQty", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SimpleQty { get; set; } =0;

        [JsonProperty("simpleCost", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SimpleCost { get; set; } =0;

        [JsonProperty("simpleValue", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SimpleValue { get; set; } =0;

        [JsonProperty("simplePnl", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SimplePnl { get; set; } =0;

        [JsonProperty("simplePnlPcnt", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SimplePnlPcnt { get; set; } =0;

        [JsonProperty("avgCostPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? AvgCostPrice { get; set; } =0;

        [JsonProperty("avgEntryPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? AvgEntryPrice { get; set; } =0;

        [JsonProperty("breakEvenPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? BreakEvenPrice { get; set; } =0;

        [JsonProperty("marginCallPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? MarginCallPrice { get; set; } =0;

        [JsonProperty("liquidationPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? LiquidationPrice { get; set; } =0;

        [JsonProperty("bankruptPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? BankruptPrice { get; set; } =0;

        [JsonProperty("timestamp", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? Timestamp { get; set; }

        [JsonProperty("lastPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? LastPrice { get; set; } =0;

        [JsonProperty("lastValue", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? LastValue { get; set; } = 0;


    }

}

