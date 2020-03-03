using Newtonsoft.Json;

namespace    Bitmex.Net.Client.Objects
{
   

    /// <summary>Public Announcements</summary>

    public class Announcement
    {
        [JsonProperty("id", Required = Required.Always)]
        public decimal Id { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("date")]
        public System.DateTimeOffset? Date { get; set; }


    }

}

