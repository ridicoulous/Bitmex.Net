using Bitmex.Net.Client.Converters;
using Bitmex.Net.Client.Interfaces;
using CryptoExchange.Net.CommonObjects;
using Newtonsoft.Json;
using System;

namespace Bitmex.Net.Client.Objects
{
    /// <summary>Individual &amp; Bucketed Trades</summary>

    public class BitmexTrade : IBitmexHistoricalDataEntry
    {
        [JsonProperty("timestamp", Required = Required.Always)]
        public DateTime Timestamp { get; set; }
        [JsonProperty("symbol", Required = Required.Always)]
        public string Symbol { get; set; }
        [JsonProperty("side"), JsonConverter(typeof(BitmexOrderSideConverter))]
        public BitmexOrderSide Side { get; set; }
        [JsonProperty("size")]
        public decimal Size { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("tickDirection"), JsonConverter(typeof(BitmexTickDirectionConverter))]
        public BitmexTickDirection TickDirection { get; set; }
        [JsonProperty("trdMatchID")]
        public string TrdMatchID { get; set; }
        [JsonProperty("grossValue")]
        public decimal? GrossValue { get; set; }
        [JsonProperty("homeNotional")]
        public decimal? HomeNotional { get; set; }
        [JsonProperty("foreignNotional")]
        public decimal? ForeignNotional { get; set; }

        internal Trade ToCryptoExchangeTrade()
        {
            return new Trade()
            {
                SourceObject = this,
                Price = this.Price,
                Quantity = this.Size,
                Symbol = this.Symbol,
                Timestamp = this.Timestamp,
            };
        }
    }

}

