using Newtonsoft.Json;

namespace    Bitmex.Net.Client.Objects
{
   


   
    
        /// <summary>Active Liquidations</summary>

        public class Liquidation
    {
        [JsonProperty("orderID", Required = Required.Always)]
      
        public string OrderID { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("side")]
        public string Side { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("leavesQty")]
        public decimal LeavesQty { get; set; }


    }

}

