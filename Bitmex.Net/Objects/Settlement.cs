using Newtonsoft.Json;

namespace    Bitmex.Net.Client.Objects
{
        /// <summary>Historical Settlement Data</summary>

        public class Settlement
    {
        [JsonProperty("timestamp", Required = Required.Always)]
      
        public System.DateTimeOffset Timestamp { get; set; }

        [JsonProperty("symbol", Required = Required.Always)]
      
        public string Symbol { get; set; }

        [JsonProperty("settlementType")]
        public string SettlementType { get; set; }

        [JsonProperty("settledPrice")]
        public decimal SettledPrice { get; set; }

        [JsonProperty("optionStrikePrice")]
        public decimal OptionStrikePrice { get; set; }

        [JsonProperty("optionUnderlyingPrice")]
        public decimal OptionUnderlyingPrice { get; set; }

        [JsonProperty("bankrupt")]
        public decimal Bankrupt { get; set; }

        [JsonProperty("taxBase")]
        public decimal TaxBase { get; set; }

        [JsonProperty("taxRate")]
        public decimal TaxRate { get; set; }


    }

}

