using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{
   
        public class OrderBookL2
    {
        [JsonProperty("symbol", Required = Required.Always)]
      
        public string Symbol { get; set; }

        [JsonProperty("id", Required = Required.Always)]
        public decimal Id { get; set; }

        [JsonProperty("side", Required = Required.Always)]
      
        public string Side { get; set; }

        [JsonProperty("size", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Size { get; set; }

        [JsonProperty("price", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Price { get; set; }


    }

}

