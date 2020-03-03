using Newtonsoft.Json;

namespace    Bitmex.Net.Client.Objects
{
   


            /// <summary>Swap Funding History</summary>

        public class Funding
    {
        [JsonProperty("timestamp", Required = Required.Always)]
      
        public System.DateTimeOffset Timestamp { get; set; }

        [JsonProperty("symbol", Required = Required.Always)]
      
        public string Symbol { get; set; }

        [JsonProperty("fundingInterval")]
        public System.DateTimeOffset? FundingInterval { get; set; }

        [JsonProperty("fundingRate")]
        public decimal? FundingRate { get; set; }

        [JsonProperty("fundingRateDaily")]
        public decimal? FundingRateDaily { get; set; }


    }

}

