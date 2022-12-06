using Bitmex.Net.Client.Converters;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bitmex.Net.Client.Objects
{

    public class BitmexOrderBookL10
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
        [JsonProperty("asks")]
        public List<BitmexOrderBook10entry> Asks { get; set; } = new List<BitmexOrderBook10entry>();
        [JsonProperty("bids")]
        public List<BitmexOrderBook10entry> Bids { get; set; } = new List<BitmexOrderBook10entry>();

    }
    [JsonConverter(typeof(ArrayConverter))]
    public class BitmexOrderBook10entry : ISymbolOrderBookEntry
    {
        [ArrayProperty(0)]
        public decimal Price { get; set; }
        [ArrayProperty(1)]
        public decimal Quantity { get; set; }
    }

    public class OrderBookL2 : List<BitmexOrderBookEntry>
    {
        public IEnumerable<ISymbolOrderBookEntry> CommonBids => this.Where(i => i.Side == OrderBookEntryType.Bid);

        public IEnumerable<ISymbolOrderBookEntry> CommonAsks => this.Where(i => i.Side == OrderBookEntryType.Ask);

        internal OrderBook ToCryptoExchangeOrderBook()
        {
            return new OrderBook()
            {
                SourceObject = this,
                Asks = CommonAsks
                    .Select(entry => new OrderBookEntry()
                    {
                        Price = entry.Price,
                        Quantity = entry.Quantity
                    }),
                Bids = CommonBids
                    .Select(entry => new OrderBookEntry()
                    {
                        Price = entry.Price,
                        Quantity = entry.Quantity
                    })
            };
        } 
    }
    public class BitmexOrderBookEntry : ISymbolOrderBookEntry
    {
        [JsonIgnore]
        public decimal Quantity { get => Size ?? 0; set => Size = value; }

        [JsonProperty("symbol", Required = Required.Always)]
        public string Symbol { get; set; }

        [JsonProperty("id", Required = Required.Always)]
        public long Id { get; set; }

        [JsonProperty("side"), JsonConverter(typeof(BitmexOrderBookEntryTypeConverter))]
        public OrderBookEntryType Side { get; set; }

        [JsonProperty("size")]
        public decimal? Size { get; set; }

        [JsonProperty("price")]
        public decimal? _price { get; set; }

        public decimal Price { get => _price ?? 0; set => _price = value; }

        public void SetPrice(int instrumentIndex, decimal tickSize = 0.01m)
        {           
            Price = ((1e8m * instrumentIndex) - Id) * tickSize;
        }
    }

}

