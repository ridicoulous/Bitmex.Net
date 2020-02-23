using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{
        public class Wallet
    {
        [JsonProperty("account", Required = Required.Always)]
        public decimal Account { get; set; }

        [JsonProperty("currency", Required = Required.Always)]
      
        public string Currency { get; set; }

        [JsonProperty("prevDeposited", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PrevDeposited { get; set; }

        [JsonProperty("prevWithdrawn", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PrevWithdrawn { get; set; }

        [JsonProperty("prevTransferIn", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PrevTransferIn { get; set; }

        [JsonProperty("prevTransferOut", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PrevTransferOut { get; set; }

        [JsonProperty("prevAmount", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PrevAmount { get; set; }

        [JsonProperty("prevTimestamp", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? PrevTimestamp { get; set; }

        [JsonProperty("deltaDeposited", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? DeltaDeposited { get; set; }

        [JsonProperty("deltaWithdrawn", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? DeltaWithdrawn { get; set; }

        [JsonProperty("deltaTransferIn", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? DeltaTransferIn { get; set; }

        [JsonProperty("deltaTransferOut", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? DeltaTransferOut { get; set; }

        [JsonProperty("deltaAmount", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? DeltaAmount { get; set; }

        [JsonProperty("deposited", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Deposited { get; set; }

        [JsonProperty("withdrawn", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Withdrawn { get; set; }

        [JsonProperty("transferIn", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? TransferIn { get; set; }

        [JsonProperty("transferOut", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? TransferOut { get; set; }

        [JsonProperty("amount", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Amount { get; set; }

        [JsonProperty("pendingCredit", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PendingCredit { get; set; }

        [JsonProperty("pendingDebit", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? PendingDebit { get; set; }

        [JsonProperty("confirmedDebit", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ConfirmedDebit { get; set; }

        [JsonProperty("timestamp", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? Timestamp { get; set; }

        [JsonProperty("addr", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Addr { get; set; }

        [JsonProperty("script", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Script { get; set; }

        [JsonProperty("withdrawalLock", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<string> WithdrawalLock { get; set; }


    }

}

