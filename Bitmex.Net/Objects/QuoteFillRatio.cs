using Newtonsoft.Json;

namespace    Bitmex.Net.Client.Objects
{
    /// <summary>Daily Quote Fill Ratio Statistic</summary>

    public class QuoteFillRatio
    {
        [JsonProperty("date", Required = Required.Always)]

        public System.DateTimeOffset Date { get; set; }

        [JsonProperty("account")]
        public decimal Account { get; set; }

        [JsonProperty("quoteCount")]
        public decimal QuoteCount { get; set; }

        [JsonProperty("dealtCount")]
        public decimal DealtCount { get; set; }

        [JsonProperty("quotesMavg7")]
        public decimal QuotesMavg7 { get; set; }

        [JsonProperty("dealtMavg7")]
        public decimal DealtMavg7 { get; set; }

        [JsonProperty("quoteFillRatioMavg7")]
        public decimal QuoteFillRatioMavg7 { get; set; }


    }

}

