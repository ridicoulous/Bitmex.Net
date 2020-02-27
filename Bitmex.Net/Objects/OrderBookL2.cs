using Bitmex.Net.Client.Converters;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bitmex.Net.Client.Objects
{

    public class OrderBookL2
    {
        public OrderBookL2(List<BitmexOrderBookEntry> entries)
        {
            foreach (var e in entries)
            {
                if (e.Side == OrderBookEntryType.Bid)
                {
                    Bids.Add(e);
                }
                else
                {
                    Asks.Add(e);
                }
            }
        }

        public List<BitmexOrderBookEntry> Asks { get; set; } = new List<BitmexOrderBookEntry>(100);
        public List<BitmexOrderBookEntry> Bids { get; set; } = new List<BitmexOrderBookEntry>(100);


    }
    public class BitmexOrderBookEntry : ISymbolOrderBookEntry
    {
        [JsonIgnore]
        public decimal Quantity { get => Size ?? 0; set => Size = value; }

        [JsonProperty("symbol", Required = Required.Always)]
        public string Symbol { get; set; }

        [JsonProperty("id", Required = Required.Always)]
        public long Id { get; set; }

        [JsonProperty("side"), JsonConverter(typeof(BitmexOrderSideToOrderBookEntryTypeConverter))]
        public OrderBookEntryType Side { get; set; }

        [JsonProperty("size")]
        public decimal? Size { get; set; }

        [JsonProperty("price")]
        public decimal? _price { get; set; }

        public decimal Price { get => _price ?? 0; set => _price = value; }

        public void SetPrice(int instrumentIndex, decimal tickSize=0.01m)
        {
            Price = ((1e8m * instrumentIndex) - Id) * tickSize;
        }
    }
}

