using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{
    public class Transaction
    {
        [JsonProperty("transactID", Required = Required.Always)]

        public System.Guid TransactID { get; set; }

        [JsonProperty("account", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Account { get; set; }

        [JsonProperty("currency", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty("transactType", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string TransactType { get; set; }

        [JsonProperty("amount", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Amount { get; set; }

        [JsonProperty("fee", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Fee { get; set; }

        [JsonProperty("transactStatus", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string TransactStatus { get; set; }

        [JsonProperty("address", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }

        [JsonProperty("tx", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Tx { get; set; }

        [JsonProperty("text", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        [JsonProperty("transactTime", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? TransactTime { get; set; }

        [JsonProperty("timestamp", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? Timestamp { get; set; }


    }

}

