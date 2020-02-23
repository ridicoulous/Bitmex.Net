using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{

    /// <summary>Account Operations</summary>

    public class User
    {
        [JsonProperty("id", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Id { get; set; }

        [JsonProperty("ownerId", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? OwnerId { get; set; }

        [JsonProperty("firstname", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Firstname { get; set; }

        [JsonProperty("lastname", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Lastname { get; set; }

        [JsonProperty("username", Required = Required.Always)]

        public string Username { get; set; }

        [JsonProperty("email", Required = Required.Always)]

        public string Email { get; set; }

        [JsonProperty("phone", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }

        [JsonProperty("created", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? Created { get; set; }

        [JsonProperty("lastUpdated", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? LastUpdated { get; set; }

        [JsonProperty("preferences", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public UserPreferences Preferences { get; set; }

        [JsonProperty("TFAEnabled", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string TFAEnabled { get; set; }

        [JsonProperty("affiliateID", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
   
        public string AffiliateID { get; set; }

        [JsonProperty("pgpPubKey", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string PgpPubKey { get; set; }

        [JsonProperty("pgpPubKeyCreated", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? PgpPubKeyCreated { get; set; }

        [JsonProperty("country", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]

        public string Country { get; set; }

        [JsonProperty("geoipCountry", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string GeoipCountry { get; set; }

        [JsonProperty("geoipRegion", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string GeoipRegion { get; set; }

        [JsonProperty("typ", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Typ { get; set; }


    }

}

