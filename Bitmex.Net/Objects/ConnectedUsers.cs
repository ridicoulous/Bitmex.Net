using Newtonsoft.Json;

namespace    Bitmex.Net.Client.Objects
{


    public class ConnectedUsers
    {
        [JsonProperty("users")]
        public decimal? Users { get; set; }

        [JsonProperty("bots")]
        public decimal? Bots { get; set; }


    }

}

