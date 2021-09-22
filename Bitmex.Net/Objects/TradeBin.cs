using System;
using CryptoExchange.Net.ExchangeInterfaces;
using Newtonsoft.Json;

namespace Bitmex.Net.Client.Objects
{

    public class TradeBin : ICommonKline
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

        public decimal CommonHigh => High.GetValueOrDefault();

        public decimal CommonLow => Low.GetValueOrDefault();

        public decimal CommonOpen => Open.GetValueOrDefault();

        public decimal CommonClose => Close.GetValueOrDefault();

        /// <summary>
        /// Timestamps returned by our bucketed endpoints are the end of the period, 
        /// indicating when the bucket was written to disk. 
        /// </summary>
        public DateTime CommonOpenTime => Timestamp;

        public decimal CommonVolume => Volume.GetValueOrDefault();
    }

}

