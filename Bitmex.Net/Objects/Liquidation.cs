using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{
   


   
    
        /// <summary>Active Liquidations</summary>

        public class Liquidation
    {
        [JsonProperty("orderID", Required = Required.Always)]
      
        public System.Guid OrderID { get; set; }

        [JsonProperty("symbol", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Symbol { get; set; }

        [JsonProperty("side", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Side { get; set; }

        [JsonProperty("price", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Price { get; set; }

        [JsonProperty("leavesQty", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? LeavesQty { get; set; }


    }

}

