using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{
    public class IndexComposite
    {
        [JsonProperty("timestamp", Required = Required.Always)]

        public System.DateTimeOffset Timestamp { get; set; }

        [JsonProperty("symbol", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Symbol { get; set; }

        [JsonProperty("indexSymbol", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string IndexSymbol { get; set; }

        [JsonProperty("reference", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Reference { get; set; }

        [JsonProperty("lastPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? LastPrice { get; set; }

        [JsonProperty("weight", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? Weight { get; set; }

        [JsonProperty("logged", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? Logged { get; set; }


    }

}

