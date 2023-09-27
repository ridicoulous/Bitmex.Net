using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Bitmex.Net.Objects.Errors
{
    internal class BitmexErrorResponse
    {
        [JsonProperty("error")]
        public BitmexError Error { get; set; }
    }

    public class BitmexError
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}