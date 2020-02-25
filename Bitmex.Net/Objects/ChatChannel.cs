using Newtonsoft.Json;

namespace    Bitmex.Net.Client.Objects
{
    
    
        public class ChatChannel
    {
        [JsonProperty("id")]
        public double? Id { get; set; }

        [JsonProperty("name", Required = Required.Always)]
      
        public string Name { get; set; }


    }

}

