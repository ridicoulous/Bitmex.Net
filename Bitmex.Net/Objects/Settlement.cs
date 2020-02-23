using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{
        /// <summary>Historical Settlement Data</summary>

        public class Settlement
    {
        [JsonProperty("timestamp", Required = Required.Always)]
      
        public System.DateTimeOffset Timestamp { get; set; }

        [JsonProperty("symbol", Required = Required.Always)]
      
        public string Symbol { get; set; }

        [JsonProperty("settlementType", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string SettlementType { get; set; }

        [JsonProperty("settledPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? SettledPrice { get; set; }

        [JsonProperty("optionStrikePrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? OptionStrikePrice { get; set; }

        [JsonProperty("optionUnderlyingPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? OptionUnderlyingPrice { get; set; }

        [JsonProperty("bankrupt", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Bankrupt { get; set; }

        [JsonProperty("taxBase", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? TaxBase { get; set; }

        [JsonProperty("taxRate", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? TaxRate { get; set; }


    }

}

