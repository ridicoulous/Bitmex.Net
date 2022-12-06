using System;
using CryptoExchange.Net.CommonObjects;
using Newtonsoft.Json;

namespace Bitmex.Net.Client.Objects
{

    public class TradeBin
    {
        [JsonProperty("timestamp", Required = Required.Always)]

        public System.DateTime Timestamp { get; set; }

        [JsonProperty("symbol", Required = Required.Always)]

        public string Symbol { get; set; }

        [JsonProperty("open")]
        public decimal? Open { get; set; }

        [JsonProperty("high")]
        public decimal? High { get; set; }

        [JsonProperty("low")]
        public decimal? Low { get; set; }

        [JsonProperty("close")]
        public decimal? Close { get; set; }

        [JsonProperty("trades")]
        public decimal? Trades { get; set; }

        [JsonProperty("volume")]
        public decimal? Volume { get; set; }

        [JsonProperty("vwap")]
        public decimal? Vwap { get; set; }

        [JsonProperty("lastSize")]
        public decimal? LastSize { get; set; }

        [JsonProperty("turnover")]
        public decimal? Turnover { get; set; }

        [JsonProperty("homeNotional")]
        public decimal? HomeNotional { get; set; }

        [JsonProperty("foreignNotional")]
        public decimal? ForeignNotional { get; set; }

        internal Kline ToCryptoExchangeKline(TimeSpan shift)
        {
            return new Kline()
            {
                SourceObject = this,
                OpenPrice = this.Open,
                HighPrice = this.High,
                LowPrice = this.Low,
                ClosePrice = this.Close,
                OpenTime = this.Timestamp.Add(-shift),
                Volume = this.Volume
            };
        }
    }

}

