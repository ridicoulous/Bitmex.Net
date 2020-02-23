using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{
    /// <summary>Exchange Statistics</summary>

    public class Stats
    {
        [JsonProperty("rootSymbol", Required = Required.Always)]

        public string RootSymbol { get; set; }

        [JsonProperty("currency", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty("volume24h", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Volume24h { get; set; }

        [JsonProperty("turnover24h", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Turnover24h { get; set; }

        [JsonProperty("openInterest", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? OpenInterest { get; set; }

        [JsonProperty("openValue", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? OpenValue { get; set; }


    }

}

