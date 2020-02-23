using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{
        public class StatsHistory
    {
        [JsonProperty("date", Required = Required.Always)]
      
        public System.DateTimeOffset Date { get; set; }

        [JsonProperty("rootSymbol", Required = Required.Always)]
      
        public string RootSymbol { get; set; }

        [JsonProperty("currency", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty("volume", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Volume { get; set; }

        [JsonProperty("turnover", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Turnover { get; set; }


    }

}

