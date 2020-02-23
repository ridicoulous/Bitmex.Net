using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{
   


            /// <summary>Swap Funding History</summary>

        public class Funding
    {
        [JsonProperty("timestamp", Required = Required.Always)]
      
        public System.DateTimeOffset Timestamp { get; set; }

        [JsonProperty("symbol", Required = Required.Always)]
      
        public string Symbol { get; set; }

        [JsonProperty("fundingInterval", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? FundingInterval { get; set; }

        [JsonProperty("fundingRate", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? FundingRate { get; set; }

        [JsonProperty("fundingRateDaily", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? FundingRateDaily { get; set; }


    }

}

