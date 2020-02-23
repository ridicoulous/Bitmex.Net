using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{
    /// <summary>Best Bid/Offer Snapshots &amp; Historical Bins</summary>

    public class Quote
    {
        [JsonProperty("timestamp", Required = Required.Always)]

        public System.DateTimeOffset Timestamp { get; set; }

        [JsonProperty("symbol", Required = Required.Always)]

        public string Symbol { get; set; }

        [JsonProperty("bidSize", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? BidSize { get; set; }

        [JsonProperty("bidPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? BidPrice { get; set; }

        [JsonProperty("askPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? AskPrice { get; set; }

        [JsonProperty("askSize", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? AskSize { get; set; }


    }

}

