using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitmex.Net.Client.Objects.Socket
{
 
    internal class BitmexUpdateMessage<T>
    {
        [JsonProperty("subject")]
        public string Subject { get; set; } = "";
        [JsonProperty("topic")]
        public string Topic { get; set; } = "";
        [JsonProperty("type")]
        public string Type { get; set; } = "";
        [JsonProperty("data")]
        public T Data { get; set; } = default!;
    }
}
