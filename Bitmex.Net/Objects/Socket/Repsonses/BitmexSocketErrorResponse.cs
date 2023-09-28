using Bitmex.Net.Client.Objects.Socket.Requests;
using Newtonsoft.Json;

namespace Bitmex.Net.Objects.Socket.Repsonses
{
    public class BitmexSocketErrorResponse
    {
        [JsonProperty("status")]
        public int? Status { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("request")]
        public BitmexSubscribeRequest Request { get; set; }
    }

    public class Meta
    {
        [JsonProperty("note")]
        public string Note { get; set; }
    }
}