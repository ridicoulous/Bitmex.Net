using CryptoExchange.Net.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace    Bitmex.Net.Client.Objects
{
    
    public class OrderBookL2
    {
        public OrderBookL2(List<BitmexOrderBookEntry> entries)
        {
            foreach(var e in entries)
            {
                if(e.Side==BitmexOrderSide.Buy)
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
        public decimal Quantity { get => Size; set => Size=value; }
       
        [JsonProperty("symbol", Required = Required.Always)]

        public string Symbol { get; set; }

        [JsonProperty("id", Required = Required.Always)]
        public decimal Id { get; set; }

        [JsonProperty("side", Required = Required.Always)]

        public BitmexOrderSide Side { get; set; }

        [JsonProperty("size")]
        public decimal Size { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }
    }
}

