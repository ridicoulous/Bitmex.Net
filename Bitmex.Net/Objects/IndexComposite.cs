using Newtonsoft.Json;

namespace    Bitmex.Net.Client.Objects
{
    public class IndexComposite
    {
        [JsonProperty("timestamp", Required = Required.Always)]

        public System.DateTimeOffset Timestamp { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("indexSymbol")]
        public string IndexSymbol { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("lastPrice")]
        public double? LastPrice { get; set; }

        [JsonProperty("weight")]
        public double? Weight { get; set; }

        [JsonProperty("logged")]
        public System.DateTimeOffset? Logged { get; set; }


    }

}

