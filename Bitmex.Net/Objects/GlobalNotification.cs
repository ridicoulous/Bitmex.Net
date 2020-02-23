using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{
   


   
   
        /// <summary>Account Notifications</summary>

        public class GlobalNotification
    {
        [JsonProperty("id", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Id { get; set; }

        [JsonProperty("date", Required = Required.Always)]
      
        public System.DateTimeOffset Date { get; set; }

        [JsonProperty("title", Required = Required.Always)]
      
        public string Title { get; set; }

        [JsonProperty("body", Required = Required.Always)]
      
        public string Body { get; set; }

        [JsonProperty("ttl", Required = Required.Always)]
        public decimal Ttl { get; set; }

        [JsonProperty("type", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public GlobalNotificationType? Type { get; set; }

        [JsonProperty("closable", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Closable { get; set; } = true;

        [JsonProperty("persist", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Persist { get; set; } = true;

        [JsonProperty("waitForVisibility", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? WaitForVisibility { get; set; } = true;

        [JsonProperty("sound", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Sound { get; set; }


    }

}

