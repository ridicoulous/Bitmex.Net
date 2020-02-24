using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{
    /// <summary>User Events for auditing</summary>

    public class UserEvent
    {
        [JsonProperty("id")]
        public decimal Id { get; set; }

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

        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("geoipCountry")]

        public string GeoipCountry { get; set; }

        [JsonProperty("geoipRegion")]
        public string GeoipRegion { get; set; }

        [JsonProperty("geoipSubRegion")]

        public string GeoipSubRegion { get; set; }

        [JsonProperty("eventMeta")]
        public object EventMeta { get; set; }

        [JsonProperty("created", Required = Required.Always)]

        public System.DateTimeOffset Created { get; set; }


    }

}

