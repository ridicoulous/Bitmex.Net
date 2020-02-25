using Newtonsoft.Json;
using System;

namespace    Bitmex.Net.Client.Objects
{
    /// <summary>Best Bid/Offer Snapshots &amp; Historical Bins</summary>

    public class Quote
    {
        [JsonProperty("timestamp", Required = Required.Always)]

        public DateTimeOffset Timestamp { get; set; }

        [JsonProperty("symbol", Required = Required.Always)]

        public string Symbol { get; set; }

        [JsonProperty("bidSize")]
        public decimal BidSize { get; set; }

        [JsonProperty("bidPrice")]
        public decimal BidPrice { get; set; }

        [JsonProperty("askPrice")]
        public decimal AskPrice { get; set; }

        [JsonProperty("askSize")]
        public decimal AskSize { get; set; }


    }

}

