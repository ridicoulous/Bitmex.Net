using Newtonsoft.Json;

namespace    Bitmex.Net.Client.Objects
{


    public class ConnectedUsers
    {
        [JsonProperty("users")]
        public double? Users { get; set; }

        [JsonProperty("bots")]
        public double? Bots { get; set; }


    }

}

