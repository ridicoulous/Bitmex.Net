using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{
    /// <summary>Daily Quote Fill Ratio Statistic</summary>

    public class QuoteFillRatio
    {
        [JsonProperty("date", Required = Required.Always)]

        public System.DateTimeOffset Date { get; set; }

        [JsonProperty("account", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Account { get; set; }

        [JsonProperty("quoteCount", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? QuoteCount { get; set; }

        [JsonProperty("dealtCount", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? DealtCount { get; set; }

        [JsonProperty("quotesMavg7", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? QuotesMavg7 { get; set; }

        [JsonProperty("dealtMavg7", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? DealtMavg7 { get; set; }

        [JsonProperty("quoteFillRatioMavg7", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? QuoteFillRatioMavg7 { get; set; }


    }

}

