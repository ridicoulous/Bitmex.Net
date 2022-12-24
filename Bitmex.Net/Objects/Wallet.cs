using Newtonsoft.Json;

namespace Bitmex.Net.Client.Objects
{
    public class Wallet
    {
        [JsonProperty("account", Required = Required.Always)]
        public decimal Account { get; set; }

        [JsonProperty("currency", Required = Required.Always)]

        public string Currency { get; set; }


        [JsonProperty("deposited")]
        public decimal Deposited { get; set; }

        [JsonProperty("withdrawn")]
        public decimal Withdrawn { get; set; }

        [JsonProperty("transferIn")]
        public decimal TransferIn { get; set; }

        [JsonProperty("transferOut")]
        public decimal TransferOut { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("pendingCredit")]
        public decimal PendingCredit { get; set; }

        [JsonProperty("pendingDebit")]
        public decimal PendingDebit { get; set; }

        [JsonProperty("confirmedDebit")]
        public decimal ConfirmedDebit { get; set; }

        [JsonProperty("timestamp")]
        public System.DateTime? Timestamp { get; set; }

        [JsonProperty("addr")]
        public string Addr { get; set; }

        [JsonProperty("script")]
        public string Script { get; set; }

        [JsonProperty("withdrawalLock")]
        public System.Collections.Generic.ICollection<string> WithdrawalLock { get; set; }

        public decimal BtcAmount => Amount / 10e7m;

    }

}

