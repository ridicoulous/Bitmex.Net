using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{
    public class StatsUSD
    {
        [JsonProperty("rootSymbol", Required = Required.Always)]

        public string RootSymbol { get; set; }

        [JsonProperty("currency", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty("turnover24h", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Turnover24h { get; set; }

        [JsonProperty("turnover30d", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Turnover30d { get; set; }

        [JsonProperty("turnover365d", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Turnover365d { get; set; }

        [JsonProperty("turnover", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Turnover { get; set; }


    }

}

