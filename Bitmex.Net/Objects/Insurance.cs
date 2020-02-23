using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{
        /// <summary>Insurance Fund Data</summary>

        public class Insurance
    {
        [JsonProperty("currency", Required = Required.Always)]
      
        public string Currency { get; set; }

        [JsonProperty("timestamp", Required = Required.Always)]
      
        public System.DateTimeOffset Timestamp { get; set; }

        [JsonProperty("walletBalance", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? WalletBalance { get; set; }


    }

}

