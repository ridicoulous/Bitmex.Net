using Newtonsoft.Json;

namespace    Bitmex.Net.Client.Objects
{
        public class StatsHistory
    {
        [JsonProperty("date", Required = Required.Always)]
      
        public System.DateTimeOffset Date { get; set; }

        [JsonProperty("rootSymbol", Required = Required.Always)]
      
        public string RootSymbol { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("volume")]
        public decimal Volume { get; set; }

        [JsonProperty("turnover")]
        public decimal Turnover { get; set; }


    }

}

