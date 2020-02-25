using Newtonsoft.Json;

namespace    Bitmex.Net.Client.Objects
{
    public class Transaction
    {
        [JsonProperty("transactID", Required = Required.Always)]

        public System.Guid TransactID { get; set; }

        [JsonProperty("account")]
        public decimal Account { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("transactType")]
        public string TransactType { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("fee")]
        public decimal Fee { get; set; }

        [JsonProperty("transactStatus")]
        public string TransactStatus { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("tx")]
        public string Tx { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("transactTime")]
        public System.DateTimeOffset? TransactTime { get; set; }

        [JsonProperty("timestamp")]
        public System.DateTimeOffset? Timestamp { get; set; }


    }

}

