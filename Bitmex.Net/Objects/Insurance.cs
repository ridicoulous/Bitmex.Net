using Newtonsoft.Json;

namespace    Bitmex.Net.Client.Objects
{
        /// <summary>Insurance Fund Data</summary>

        public class Insurance
    {
        [JsonProperty("currency", Required = Required.Always)]
      
        public string Currency { get; set; }

        [JsonProperty("timestamp", Required = Required.Always)]
      
        public System.DateTime Timestamp { get; set; }

        [JsonProperty("walletBalance")]
        public decimal WalletBalance { get; set; }


    }

}

