using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{





    public class Margin
    {
        [JsonProperty("account", Required = Required.Always)]
        public decimal Account { get; set; } = 0;

        [JsonProperty("currency", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty("riskLimit", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? RiskLimit { get; set; } = 0;

        [JsonProperty("prevState", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string PrevState { get; set; }

        [JsonProperty("state", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }

        [JsonProperty("action", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Action { get; set; }

        [JsonProperty("amount", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Amount { get; set; } = 0;

        [JsonProperty("pendingCredit", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PendingCredit { get; set; } = 0;

        [JsonProperty("pendingDebit", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PendingDebit { get; set; } = 0;

        [JsonProperty("confirmedDebit", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ConfirmedDebit { get; set; } = 0;

        [JsonProperty("prevRealisedPnl", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PrevRealisedPnl { get; set; } = 0;

        [JsonProperty("prevUnrealisedPnl", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PrevUnrealisedPnl { get; set; } = 0;

        [JsonProperty("grossComm", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? GrossComm { get; set; } = 0;

        [JsonProperty("grossOpenCost", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? GrossOpenCost { get; set; } = 0;

        [JsonProperty("grossOpenPremium", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? GrossOpenPremium { get; set; } = 0;

        [JsonProperty("grossExecCost", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? GrossExecCost { get; set; } = 0;

        [JsonProperty("grossMarkValue", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? GrossMarkValue { get; set; } = 0;

        [JsonProperty("riskValue", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? RiskValue { get; set; } = 0;

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

        [JsonProperty("realisedPnl", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? RealisedPnl { get; set; } = 0;

        [JsonProperty("unrealisedPnl", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnrealisedPnl { get; set; } = 0;

        [JsonProperty("indicativeTax", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? IndicativeTax { get; set; } = 0;

        [JsonProperty("unrealisedProfit", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UnrealisedProfit { get; set; } = 0;

        [JsonProperty("syntheticMargin", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SyntheticMargin { get; set; } = 0;

        [JsonProperty("walletBalance", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? WalletBalance { get; set; } = 0;

        [JsonProperty("marginBalance", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? MarginBalance { get; set; } = 0;

        [JsonProperty("marginBalancePcnt", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? MarginBalancePcnt { get; set; } = 0;

        [JsonProperty("marginLeverage", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? MarginLeverage { get; set; } = 0;

        [JsonProperty("marginUsedPcnt", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? MarginUsedPcnt { get; set; } = 0;

        [JsonProperty("excessMargin", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ExcessMargin { get; set; } = 0;

        [JsonProperty("excessMarginPcnt", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ExcessMarginPcnt { get; set; } = 0;

        [JsonProperty("availableMargin", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? AvailableMargin { get; set; } = 0;

        [JsonProperty("withdrawableMargin", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? WithdrawableMargin { get; set; } = 0;

        [JsonProperty("timestamp", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? Timestamp { get; set; }

        [JsonProperty("grossLastValue", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? GrossLastValue { get; set; } = 0;

        [JsonProperty("commission", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Commission { get; set; } = 0;


    }

}

