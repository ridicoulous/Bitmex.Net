using Newtonsoft.Json;

namespace    Bitmex.Net.Client.Objects
{
    public class IndexComposite
    {
        [JsonProperty("timestamp", Required = Required.Always)]

        public System.DateTime Timestamp { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("indexSymbol")]
        public string IndexSymbol { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("lastPrice")]
        public decimal? LastPrice { get; set; }

        [JsonProperty("weight")]
        public decimal? Weight { get; set; }

        [JsonProperty("logged")]
        public System.DateTime? Logged { get; set; }


    }

}

