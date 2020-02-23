using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{


    /// <summary>Trollbox Data</summary>

    public class Chat
    {
        [JsonProperty("id", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? Id { get; set; }

        [JsonProperty("date", Required = Required.Always)]

        public System.DateTimeOffset Date { get; set; }

        [JsonProperty("user", Required = Required.Always)]

        public string User { get; set; }

        [JsonProperty("message", Required = Required.Always)]

        public string Message { get; set; }

        [JsonProperty("html", Required = Required.Always)]

        public string Html { get; set; }

        [JsonProperty("fromBot", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? FromBot { get; set; } = false;

        [JsonProperty("channelID", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? ChannelID { get; set; }


    }

}

