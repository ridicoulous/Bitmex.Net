using Newtonsoft.Json;

namespace    Bitmex.Net.Client.Objects
{





    public class Margin
    {
        [JsonProperty("account", Required = Required.Always)]
        public decimal Account { get; set; } = 0;

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("riskLimit")]
        public decimal RiskLimit { get; set; } = 0;

        [JsonProperty("prevState")]
        public string PrevState { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; } = 0;

        [JsonProperty("pendingCredit")]
        public decimal PendingCredit { get; set; } = 0;

        [JsonProperty("pendingDebit")]
        public decimal PendingDebit { get; set; } = 0;

        [JsonProperty("confirmedDebit")]
        public decimal ConfirmedDebit { get; set; } = 0;

        [JsonProperty("prevRealisedPnl")]
        public decimal PrevRealisedPnl { get; set; } = 0;

        [JsonProperty("prevUnrealisedPnl")]
        public decimal PrevUnrealisedPnl { get; set; } = 0;

        [JsonProperty("grossComm")]
        public decimal GrossComm { get; set; } = 0;

        [JsonProperty("grossOpenCost")]
        public decimal GrossOpenCost { get; set; } = 0;

        [JsonProperty("grossOpenPremium")]
        public decimal GrossOpenPremium { get; set; } = 0;

        [JsonProperty("grossExecCost")]
        public decimal GrossExecCost { get; set; } = 0;

        [JsonProperty("grossMarkValue")]
        public decimal GrossMarkValue { get; set; } = 0;

        [JsonProperty("riskValue")]
        public decimal RiskValue { get; set; } = 0;

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

        [JsonProperty("realisedPnl")]
        public decimal RealisedPnl { get; set; } = 0;

        [JsonProperty("unrealisedPnl")]
        public decimal UnrealisedPnl { get; set; } = 0;

        [JsonProperty("indicativeTax")]
        public decimal IndicativeTax { get; set; } = 0;

        [JsonProperty("unrealisedProfit")]
        public decimal UnrealisedProfit { get; set; } = 0;

        [JsonProperty("syntheticMargin")]
        public decimal SyntheticMargin { get; set; } = 0;

        [JsonProperty("walletBalance")]
        public decimal WalletBalance { get; set; } = 0;

        [JsonProperty("marginBalance")]
        public decimal MarginBalance { get; set; } = 0;

        [JsonProperty("marginBalancePcnt")]
        public decimal MarginBalancePcnt { get; set; } = 0;

        [JsonProperty("marginLeverage")]
        public decimal MarginLeverage { get; set; } = 0;

        [JsonProperty("marginUsedPcnt")]
        public decimal MarginUsedPcnt { get; set; } = 0;

        [JsonProperty("excessMargin")]
        public decimal ExcessMargin { get; set; } = 0;

        [JsonProperty("excessMarginPcnt")]
        public decimal ExcessMarginPcnt { get; set; } = 0;

        [JsonProperty("availableMargin")]
        public decimal AvailableMargin { get; set; } = 0;

        [JsonProperty("withdrawableMargin")]
        public decimal WithdrawableMargin { get; set; } = 0;

        [JsonProperty("timestamp")]
        public System.DateTimeOffset? Timestamp { get; set; }

        [JsonProperty("grossLastValue")]
        public decimal GrossLastValue { get; set; } = 0;

        [JsonProperty("commission")]
        public decimal Commission { get; set; } = 0;


    }

}

