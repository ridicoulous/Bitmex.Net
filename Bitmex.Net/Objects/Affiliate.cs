using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{
    public class Affiliate
    {
        [JsonProperty("account", Required = Required.Always)]
        public decimal Account { get; set; }

        [JsonProperty("currency", Required = Required.Always)]

        public string Currency { get; set; }

        [JsonProperty("prevPayout", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PrevPayout { get; set; }

        [JsonProperty("prevTurnover", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PrevTurnover { get; set; }

        [JsonProperty("prevComm", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PrevComm { get; set; }

        [JsonProperty("prevTimestamp", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? PrevTimestamp { get; set; }

        [JsonProperty("execTurnover", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ExecTurnover { get; set; }

        [JsonProperty("execComm", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ExecComm { get; set; }

        [JsonProperty("totalReferrals", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? TotalReferrals { get; set; }

        [JsonProperty("totalTurnover", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? TotalTurnover { get; set; }

        [JsonProperty("totalComm", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? TotalComm { get; set; }

        [JsonProperty("payoutPcnt", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PayoutPcnt { get; set; }

        [JsonProperty("pendingPayout", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PendingPayout { get; set; }

        [JsonProperty("timestamp", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? Timestamp { get; set; }

        [JsonProperty("referrerAccount", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ReferrerAccount { get; set; }

        [JsonProperty("referralDiscount", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ReferralDiscount { get; set; }

        [JsonProperty("affiliatePayout", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? AffiliatePayout { get; set; }


    }

}

