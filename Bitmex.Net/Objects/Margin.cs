using CryptoExchange.Net.CommonObjects;
using Newtonsoft.Json;

namespace    Bitmex.Net.Client.Objects
{





    public class Margin
    {
        [JsonProperty("account", Required = Required.Always)]
        public long Account { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("riskLimit")]
        public decimal? RiskLimit { get; set; }

        [JsonProperty("prevState")]
        public string PrevState { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("amount")]
        public decimal? Amount { get; set; }

        [JsonProperty("pendingCredit")]
        public decimal? PendingCredit { get; set; }

        [JsonProperty("pendingDebit")]
        public decimal? PendingDebit { get; set; }

        [JsonProperty("confirmedDebit")]
        public decimal? ConfirmedDebit { get; set; }

        [JsonProperty("prevRealisedPnl")]
        public decimal? PrevRealisedPnl { get; set; }

        [JsonProperty("prevUnrealisedPnl")]
        public decimal? PrevUnrealisedPnl { get; set; }

        [JsonProperty("grossComm")]
        public decimal? GrossComm { get; set; }

        [JsonProperty("grossOpenCost")]
        public decimal? GrossOpenCost { get; set; }

        [JsonProperty("grossOpenPremium")]
        public decimal? GrossOpenPremium { get; set; }

        [JsonProperty("grossExecCost")]
        public decimal? GrossExecCost { get; set; }

        [JsonProperty("grossMarkValue")]
        public decimal? GrossMarkValue { get; set; }

        [JsonProperty("riskValue")]
        public decimal? RiskValue { get; set; }

        [JsonProperty("taxableMargin")]
        public decimal? TaxableMargin { get; set; }

        [JsonProperty("initMargin")]
        public decimal? InitMargin { get; set; }

        [JsonProperty("maintMargin")]
        public decimal? MaintMargin { get; set; }

        [JsonProperty("sessionMargin")]
        public decimal? SessionMargin { get; set; }

        [JsonProperty("targetExcessMargin")]
        public decimal? TargetExcessMargin { get; set; }

        [JsonProperty("varMargin")]
        public decimal? VarMargin { get; set; }

        [JsonProperty("realisedPnl")]
        public decimal? RealisedPnl { get; set; }

        [JsonProperty("unrealisedPnl")]
        public decimal? UnrealisedPnl { get; set; }

        [JsonProperty("indicativeTax")]
        public decimal? IndicativeTax { get; set; }

        [JsonProperty("unrealisedProfit")]
        public decimal? UnrealisedProfit { get; set; }

        [JsonProperty("syntheticMargin")]
        public decimal? SyntheticMargin { get; set; }

        [JsonProperty("walletBalance")]
        public decimal? WalletBalance { get; set; }

        [JsonProperty("marginBalance")]
        public decimal? MarginBalance { get; set; }

        [JsonProperty("marginBalancePcnt")]
        public decimal? MarginBalancePcnt { get; set; }

        [JsonProperty("marginLeverage")]
        public decimal? MarginLeverage { get; set; }

        [JsonProperty("marginUsedPcnt")]
        public decimal? MarginUsedPcnt { get; set; }

        [JsonProperty("excessMargin")]
        public decimal? ExcessMargin { get; set; }

        [JsonProperty("excessMarginPcnt")]
        public decimal? ExcessMarginPcnt { get; set; }

        [JsonProperty("availableMargin")]
        public decimal? AvailableMargin { get; set; }

        [JsonProperty("withdrawableMargin")]
        public decimal? WithdrawableMargin { get; set; }

        [JsonProperty("timestamp")]
        public System.DateTime? Timestamp { get; set; }

        [JsonProperty("grossLastValue")]
        public decimal? GrossLastValue { get; set; }

        [JsonProperty("commission")]
        public decimal? Commission { get; set; }

        internal Balance ToCryptoExchangeBalance()
        {
            return new Balance()
            {
                SourceObject = this,
                Asset = this.Currency,
                Available = this.AvailableMargin,
                Total = this.WalletBalance
            };
        }
    }

}

