using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{


    /// <summary>User communication SNS token</summary>

    public class CommunicationToken
    {
        [JsonProperty("id", Required = Required.Always)]

        public string Id { get; set; }

        [JsonProperty("userId", Required = Required.Always)]
        public decimal UserId { get; set; }

        [JsonProperty("deviceToken", Required = Required.Always)]

        public string DeviceToken { get; set; }

        [JsonProperty("channel", Required = Required.Always)]

        public string Channel { get; set; }


    }

}

