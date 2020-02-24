using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{
        public class Wallet
    {
        [JsonProperty("account", Required = Required.Always)]
        public decimal Account { get; set; }

        [JsonProperty("currency", Required = Required.Always)]
      
        public string Currency { get; set; }

        [JsonProperty("prevDeposited")]
        public decimal PrevDeposited { get; set; }

        [JsonProperty("prevWithdrawn")]
        public decimal PrevWithdrawn { get; set; }

        [JsonProperty("prevTransferIn")]
        public decimal PrevTransferIn { get; set; }

        [JsonProperty("prevTransferOut")]
        public decimal PrevTransferOut { get; set; }

        [JsonProperty("prevAmount")]
        public decimal PrevAmount { get; set; }

        [JsonProperty("prevTimestamp")]
        public System.DateTimeOffset? PrevTimestamp { get; set; }

        [JsonProperty("deltaDeposited")]
        public decimal DeltaDeposited { get; set; }

        [JsonProperty("deltaWithdrawn")]
        public decimal DeltaWithdrawn { get; set; }

        [JsonProperty("deltaTransferIn")]
        public decimal DeltaTransferIn { get; set; }

        [JsonProperty("deltaTransferOut")]
        public decimal DeltaTransferOut { get; set; }

        [JsonProperty("deltaAmount")]
        public decimal DeltaAmount { get; set; }

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
        public System.DateTimeOffset? Timestamp { get; set; }

        [JsonProperty("addr")]
        public string Addr { get; set; }

        [JsonProperty("script")]
        public string Script { get; set; }

        [JsonProperty("withdrawalLock")]
        public System.Collections.Generic.ICollection<string> WithdrawalLock { get; set; }


    }

}

