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

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("underlying")]
        public string Underlying { get; set; }

        [JsonProperty("quoteCurrency")]
        public string QuoteCurrency { get; set; }

        [JsonProperty("commission")]
        public decimal Commission { get; set; } = 0;

        [JsonProperty("initMarginReq")]
        public decimal InitMarginReq { get; set; } = 0;

        [JsonProperty("maintMarginReq")]
        public decimal MaintMarginReq { get; set; } = 0;

        [JsonProperty("riskLimit")]
        public decimal RiskLimit { get; set; } = 0;

        [JsonProperty("leverage")]
        public decimal Leverage { get; set; } = 0;

        [JsonProperty("crossMargin")]
        public bool? CrossMargin { get; set; }

        [JsonProperty("deleveragePercentile")]
        public decimal DeleveragePercentile { get; set; } = 0;

        [JsonProperty("rebalancedPnl")]
        public decimal RebalancedPnl { get; set; } = 0;

        [JsonProperty("prevRealisedPnl")]
        public decimal PrevRealisedPnl { get; set; } = 0;

        [JsonProperty("prevUnrealisedPnl")]
        public decimal PrevUnrealisedPnl { get; set; } = 0;

        [JsonProperty("prevClosePrice")]
        public decimal PrevClosePrice { get; set; } = 0;

        [JsonProperty("openingTimestamp")]
        public System.DateTimeOffset? OpeningTimestamp { get; set; }

        [JsonProperty("openingQty")]
        public decimal OpeningQty { get; set; } = 0;

        [JsonProperty("openingCost")]
        public decimal OpeningCost { get; set; } = 0;

        [JsonProperty("openingComm")]
        public decimal OpeningComm { get; set; } = 0;

        [JsonProperty("openOrderBuyQty")]
        public decimal OpenOrderBuyQty { get; set; } = 0;

        [JsonProperty("openOrderBuyCost")]
        public decimal OpenOrderBuyCost { get; set; } = 0;

        [JsonProperty("openOrderBuyPremium")]
        public decimal OpenOrderBuyPremium { get; set; } = 0;

        [JsonProperty("openOrderSellQty")]
        public decimal OpenOrderSellQty { get; set; } = 0;

        [JsonProperty("openOrderSellCost")]
        public decimal OpenOrderSellCost { get; set; } = 0;

        [JsonProperty("openOrderSellPremium")]
        public decimal OpenOrderSellPremium { get; set; } = 0;

        [JsonProperty("execBuyQty")]
        public decimal ExecBuyQty { get; set; } = 0;

        [JsonProperty("execBuyCost")]
        public decimal ExecBuyCost { get; set; } = 0;

        [JsonProperty("execSellQty")]
        public decimal ExecSellQty { get; set; } = 0;

        [JsonProperty("execSellCost")]
        public decimal ExecSellCost { get; set; } = 0;

        [JsonProperty("execQty")]
        public decimal ExecQty { get; set; } = 0;

        [JsonProperty("execCost")]
        public decimal ExecCost { get; set; } = 0;

        [JsonProperty("execComm")]
        public decimal ExecComm { get; set; } = 0;

        [JsonProperty("currentTimestamp")]
        public System.DateTimeOffset? CurrentTimestamp { get; set; }

        [JsonProperty("currentQty")]
        public decimal CurrentQty { get; set; } = 0;

        [JsonProperty("currentCost")]
        public decimal CurrentCost { get; set; } = 0;

        [JsonProperty("currentComm")]
        public decimal CurrentComm { get; set; } = 0;

        [JsonProperty("realisedCost")]
        public decimal RealisedCost { get; set; } = 0;

        [JsonProperty("unrealisedCost")]
        public decimal UnrealisedCost { get; set; } = 0;

        [JsonProperty("grossOpenCost")]
        public decimal GrossOpenCost { get; set; } = 0;

        [JsonProperty("grossOpenPremium")]
        public decimal GrossOpenPremium { get; set; } = 0;

        [JsonProperty("grossExecCost")]
        public decimal GrossExecCost { get; set; } = 0;

        [JsonProperty("isOpen")]
        public bool? IsOpen { get; set; }

        [JsonProperty("markPrice")]
        public decimal MarkPrice { get; set; } = 0;

        [JsonProperty("markValue")]
        public decimal MarkValue { get; set; } = 0;

        [JsonProperty("riskValue")]
        public decimal RiskValue { get; set; } = 0;

        [JsonProperty("homeNotional")]
        public decimal HomeNotional { get; set; } = 0;

        [JsonProperty("foreignNotional")]
        public decimal ForeignNotional { get; set; } = 0;

        [JsonProperty("posState")]
        public string PosState { get; set; }

        [JsonProperty("posCost")]
        public decimal PosCost { get; set; } = 0;

        [JsonProperty("posCost2")]
        public decimal PosCost2 { get; set; } = 0;

        [JsonProperty("posCross")]
        public decimal PosCross { get; set; } = 0;

        [JsonProperty("posInit")]
        public decimal PosInit { get; set; } = 0;

        [JsonProperty("posComm")]
        public decimal PosComm { get; set; } = 0;

        [JsonProperty("posLoss")]
        public decimal PosLoss { get; set; } = 0;

        [JsonProperty("posMargin")]
        public decimal PosMargin { get; set; } = 0;

        [JsonProperty("posMaint")]
        public decimal PosMaint { get; set; } = 0;

        [JsonProperty("posAllowance")]
        public decimal PosAllowance { get; set; } = 0;

        [JsonProperty("taxableMargin")]
        public decimal TaxableMargin { get; set; } = 0;

        [JsonProperty("initMargin")]
        public decimal InitMargin { get; set; } = 0;

        [JsonProperty("maintMargin")]
        public decimal MaintMargin { get; set; } = 0;

        [JsonProperty("sessionMargin")]
        public decimal SessionMargin { get; set; } = 0;

        [JsonProperty("targetExcessMargin")]
        public decimal TargetExcessMargin { get; set; } = 0;

        [JsonProperty("varMargin")]
        public decimal VarMargin { get; set; } = 0;

        [JsonProperty("realisedGrossPnl")]
        public decimal RealisedGrossPnl { get; set; } = 0;

        [JsonProperty("realisedTax")]
        public decimal RealisedTax { get; set; } = 0;

        [JsonProperty("realisedPnl")]
        public decimal RealisedPnl { get; set; } = 0;

        [JsonProperty("unrealisedGrossPnl")]
        public decimal UnrealisedGrossPnl { get; set; } = 0;

        [JsonProperty("longBankrupt")]
        public decimal LongBankrupt { get; set; } = 0;

        [JsonProperty("shortBankrupt")]
        public decimal ShortBankrupt { get; set; } = 0;

        [JsonProperty("taxBase")]
        public decimal TaxBase { get; set; } = 0;

        [JsonProperty("indicativeTaxRate")]
        public decimal IndicativeTaxRate { get; set; } = 0;

        [JsonProperty("indicativeTax")]
        public decimal IndicativeTax { get; set; } = 0;

        [JsonProperty("unrealisedTax")]
        public decimal UnrealisedTax { get; set; } = 0;

        [JsonProperty("unrealisedPnl")]
        public decimal UnrealisedPnl { get; set; } = 0;

        [JsonProperty("unrealisedPnlPcnt")]
        public decimal UnrealisedPnlPcnt { get; set; } = 0;

        [JsonProperty("unrealisedRoePcnt")]
        public decimal UnrealisedRoePcnt { get; set; } = 0;

        [JsonProperty("simpleQty")]
        public decimal SimpleQty { get; set; } = 0;

        [JsonProperty("simpleCost")]
        public decimal SimpleCost { get; set; } = 0;

        [JsonProperty("simpleValue")]
        public decimal SimpleValue { get; set; } = 0;

        [JsonProperty("simplePnl")]
        public decimal SimplePnl { get; set; } = 0;

        [JsonProperty("simplePnlPcnt")]
        public decimal SimplePnlPcnt { get; set; } = 0;

        [JsonProperty("avgCostPrice")]
        public decimal AvgCostPrice { get; set; } = 0;

        [JsonProperty("avgEntryPrice")]
        public decimal AvgEntryPrice { get; set; } = 0;

        [JsonProperty("breakEvenPrice")]
        public decimal BreakEvenPrice { get; set; } = 0;

        [JsonProperty("marginCallPrice")]
        public decimal MarginCallPrice { get; set; } = 0;

        [JsonProperty("liquidationPrice")]
        public decimal LiquidationPrice { get; set; } = 0;

        [JsonProperty("bankruptPrice")]
        public decimal BankruptPrice { get; set; } = 0;

        [JsonProperty("timestamp")]
        public System.DateTimeOffset? Timestamp { get; set; }

        [JsonProperty("lastPrice")]
        public decimal LastPrice { get; set; } = 0;

        [JsonProperty("lastValue")]
        public decimal LastValue { get; set; } = 0;


    }

}

