using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{
    
   
        /// <summary>Information on Top Users</summary>

        public class Leaderboard
    {
        [JsonProperty("name", Required = Required.Always)]
      
        public string Name { get; set; }

        [JsonProperty("isRealName", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsRealName { get; set; }

        [JsonProperty("profit", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Profit { get; set; }


    }

}

