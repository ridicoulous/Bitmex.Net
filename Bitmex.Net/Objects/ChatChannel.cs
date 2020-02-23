using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{
    
    
        public class ChatChannel
    {
        [JsonProperty("id", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? Id { get; set; }

        [JsonProperty("name", Required = Required.Always)]
      
        public string Name { get; set; }


    }

}

