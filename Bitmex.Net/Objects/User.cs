using Newtonsoft.Json;

namespace    Bitmex.Net.Client.Objects
{

    /// <summary>Account Operations</summary>

    public class User
    {
        [JsonProperty("id")]
        public decimal Id { get; set; }

        [JsonProperty("ownerId")]
        public decimal OwnerId { get; set; }

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("username", Required = Required.Always)]

        public string Username { get; set; }

        [JsonProperty("email", Required = Required.Always)]

        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("created")]
        public System.DateTimeOffset? Created { get; set; }

        [JsonProperty("lastUpdated")]
        public System.DateTimeOffset? LastUpdated { get; set; }

        [JsonProperty("preferences")]
        public UserPreferences Preferences { get; set; }

        [JsonProperty("TFAEnabled")]
        public string TFAEnabled { get; set; }

        [JsonProperty("affiliateID")]
   
        public string AffiliateID { get; set; }

        [JsonProperty("pgpPubKey")]
        public string PgpPubKey { get; set; }

        [JsonProperty("pgpPubKeyCreated")]
        public System.DateTimeOffset? PgpPubKeyCreated { get; set; }

        [JsonProperty("country")]

        public string Country { get; set; }

        [JsonProperty("geoipCountry")]
        public string GeoipCountry { get; set; }

        [JsonProperty("geoipRegion")]
        public string GeoipRegion { get; set; }

        [JsonProperty("typ")]
        public string Typ { get; set; }


    }

}

