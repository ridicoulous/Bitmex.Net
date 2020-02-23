using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{
    /// <summary>User Events for auditing</summary>

    public class UserEvent
    {
        [JsonProperty("id", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Id { get; set; }

        [JsonProperty("type", Required = Required.Always)]

        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public UserEventType Type { get; set; }

        [JsonProperty("status", Required = Required.Always)]

        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public UserEventStatus Status { get; set; }

        [JsonProperty("userId", Required = Required.Always)]
        public decimal UserId { get; set; }

        [JsonProperty("createdById", Required = Required.Always)]
        public decimal CreatedById { get; set; }

        [JsonProperty("ip", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Ip { get; set; }

        [JsonProperty("geoipCountry", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]

        public string GeoipCountry { get; set; }

        [JsonProperty("geoipRegion", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string GeoipRegion { get; set; }

        [JsonProperty("geoipSubRegion", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]

        public string GeoipSubRegion { get; set; }

        [JsonProperty("eventMeta", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public object EventMeta { get; set; }

        [JsonProperty("created", Required = Required.Always)]

        public System.DateTimeOffset Created { get; set; }


    }

}

