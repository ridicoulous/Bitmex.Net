using Newtonsoft.Json;

namespace    Bitmex.Net.Client.Objects
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

        [JsonProperty("cidr")]
        public string Cidr { get; set; }

        [JsonProperty("permissions")]
        public System.Collections.Generic.ICollection<string> Permissions { get; set; }

        [JsonProperty("enabled")]
        public bool? Enabled { get; set; } = false;

        [JsonProperty("userId", Required = Required.Always)]
        public double UserId { get; set; }

        [JsonProperty("created")]
        public System.DateTimeOffset? Created { get; set; }


    }

}

