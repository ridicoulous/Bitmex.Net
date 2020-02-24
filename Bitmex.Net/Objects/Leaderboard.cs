using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{
    
   
        /// <summary>Information on Top Users</summary>

        public class Leaderboard
    {
        [JsonProperty("name", Required = Required.Always)]
      
        public string Name { get; set; }

        [JsonProperty("isRealName")]
        public bool? IsRealName { get; set; }

        [JsonProperty("profit")]
        public decimal Profit { get; set; }


    }

}

