using Dahomey.Json.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace Bitmex.Net.Objects
{
    public class BitmexOrder
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
