using Newtonsoft.Json;

namespace    Bitmex.Net.Client.Objects
{
    public class Affiliate
    {
        [JsonProperty("account", Required = Required.Always)]
        public long Account { get; set; }

        [JsonProperty("currency", Required = Required.Always)]

        public string Currency { get; set; }

        [JsonProperty("prevPayout")]
        public decimal? PrevPayout { get; set; }

        [JsonProperty("prevTurnover")]
        public decimal? PrevTurnover { get; set; }

        [JsonProperty("prevComm")]
        public decimal? PrevComm { get; set; }

        [JsonProperty("prevTimestamp")]
        public System.DateTimeOffset? PrevTimestamp { get; set; }

        [JsonProperty("execTurnover")]
        public decimal? ExecTurnover { get; set; }

        [JsonProperty("execComm")]
        public decimal? ExecComm { get; set; }

        [JsonProperty("totalReferrals")]
        public decimal? TotalReferrals { get; set; }

        [JsonProperty("totalTurnover")]
        public decimal? TotalTurnover { get; set; }

        [JsonProperty("totalComm")]
        public decimal? TotalComm { get; set; }

        [JsonProperty("payoutPcnt")]
        public decimal? PayoutPcnt { get; set; }

        [JsonProperty("pendingPayout")]
        public decimal? PendingPayout { get; set; }

        [JsonProperty("timestamp")]
        public System.DateTimeOffset? Timestamp { get; set; }

        [JsonProperty("referrerAccount")]
        public decimal? ReferrerAccount { get; set; }

        [JsonProperty("referralDiscount")]
        public decimal? ReferralDiscount { get; set; }

        [JsonProperty("affiliatePayout")]
        public decimal? AffiliatePayout { get; set; }


    }

}

