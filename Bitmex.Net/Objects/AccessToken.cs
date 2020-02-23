using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{
    public class AccessToken
    {
        [JsonProperty("id", Required = Required.Always)]

        public string Id { get; set; }

        /// <summary>time to live in seconds (2 weeks by default)</summary>
        [JsonProperty("ttl", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Ttl { get; set; } = 1209600m;

        [JsonProperty("created", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? Created { get; set; }

        [JsonProperty("userId", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? UserId { get; set; }


    }

}

