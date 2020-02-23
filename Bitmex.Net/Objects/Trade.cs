using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{
    /// <summary>Individual &amp; Bucketed Trades</summary>

    public class Trade
    {
        [JsonProperty("timestamp", Required = Required.Always)]

        public System.DateTimeOffset Timestamp { get; set; }

        [JsonProperty("symbol", Required = Required.Always)]

        public string Symbol { get; set; }

        [JsonProperty("side", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Side { get; set; }

        [JsonProperty("size", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Size { get; set; }

        [JsonProperty("price", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Price { get; set; }

        [JsonProperty("tickDirection", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string TickDirection { get; set; }

        [JsonProperty("trdMatchID", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid? TrdMatchID { get; set; }

        [JsonProperty("grossValue", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? GrossValue { get; set; }

        [JsonProperty("homeNotional", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? HomeNotional { get; set; }

        [JsonProperty("foreignNotional", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ForeignNotional { get; set; }


    }

}

