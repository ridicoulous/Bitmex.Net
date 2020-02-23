using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{
   

    /// <summary>Public Announcements</summary>

    public class Announcement
    {
        [JsonProperty("id", Required = Required.Always)]
        public double Id { get; set; }

        [JsonProperty("link", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Link { get; set; }

        [JsonProperty("title", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("content", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Content { get; set; }

        [JsonProperty("date", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? Date { get; set; }


    }

}

