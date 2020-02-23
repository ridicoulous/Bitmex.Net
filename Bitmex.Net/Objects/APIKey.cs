using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{
   

    /// <summary>Persistent API Keys for Developers</summary>

    public class APIKey
    {
        [JsonProperty("id", Required = Required.Always)]     
        public string Id { get; set; }

        [JsonProperty("secret", Required = Required.Always)]     
        public string Secret { get; set; }

        [JsonProperty("name", Required = Required.Always)]

        public string Name { get; set; }

        [JsonProperty("nonce", Required = Required.Always)]
        public double Nonce { get; set; } = 0;

        [JsonProperty("cidr", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Cidr { get; set; }

        [JsonProperty("permissions", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<string> Permissions { get; set; }

        [JsonProperty("enabled", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Enabled { get; set; } = false;

        [JsonProperty("userId", Required = Required.Always)]
        public double UserId { get; set; }

        [JsonProperty("created", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? Created { get; set; }


    }

}

