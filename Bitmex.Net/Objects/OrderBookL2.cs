using Bitmex.Net.Client.Converters;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Bitmex.Net.Client.Objects
{

    public class BitmexOrderBookL10
    {
        public string Symbol { get; set; }
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

        public void SetPrice(int instrumentIndex, decimal tickSize = 0.01m)
        {
            if (tickSize == 0.5m)
            {
                tickSize = 0.01m;
            }
            Price = ((1e8m * instrumentIndex) - Id) * tickSize;
        }
    }

}

