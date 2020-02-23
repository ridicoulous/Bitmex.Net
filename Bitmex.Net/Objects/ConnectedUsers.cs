using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{


    public class ConnectedUsers
    {
        [JsonProperty("users", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? Users { get; set; }

        [JsonProperty("bots", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? Bots { get; set; }


    }

}

