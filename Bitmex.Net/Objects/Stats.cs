using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{
    /// <summary>Exchange Statistics</summary>

    public class Stats
    {
        [JsonProperty("rootSymbol", Required = Required.Always)]

        public string RootSymbol { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("volume24h")]
        public decimal Volume24h { get; set; }

        [JsonProperty("turnover24h")]
        public decimal Turnover24h { get; set; }

        [JsonProperty("openInterest")]
        public decimal OpenInterest { get; set; }

        [JsonProperty("openValue")]
        public decimal OpenValue { get; set; }


    }

}

