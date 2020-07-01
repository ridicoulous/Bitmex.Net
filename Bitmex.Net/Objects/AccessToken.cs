using Newtonsoft.Json;

namespace Bitmex.Net.Client.Objects
{
    public class AccessToken
    {
        [JsonProperty("id", Required = Required.Always)]

        public string Id { get; set; }

        /// <summary>time to live in seconds (2 weeks by default)</summary>
        [JsonProperty("ttl")]
        public decimal Ttl { get; set; } = 1209600m;

        [JsonProperty("created")]
        public System.DateTime? Created { get; set; }

        [JsonProperty("userId")]
        public decimal UserId { get; set; }


    }

}

