using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{

    public class TradeBin
    {
        [JsonProperty("timestamp", Required = Required.Always)]

        public System.DateTimeOffset Timestamp { get; set; }

        [JsonProperty("symbol", Required = Required.Always)]

        public string Symbol { get; set; }

        [JsonProperty("open", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Open { get; set; }

        [JsonProperty("high", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? High { get; set; }

        [JsonProperty("low", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Low { get; set; }

        [JsonProperty("close", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Close { get; set; }

        [JsonProperty("trades", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Trades { get; set; }

        [JsonProperty("volume", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Volume { get; set; }

        [JsonProperty("vwap", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Vwap { get; set; }

        [JsonProperty("lastSize", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? LastSize { get; set; }

        [JsonProperty("turnover", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Turnover { get; set; }

        [JsonProperty("homeNotional", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? HomeNotional { get; set; }

        [JsonProperty("foreignNotional", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ForeignNotional { get; set; }


    }

}

