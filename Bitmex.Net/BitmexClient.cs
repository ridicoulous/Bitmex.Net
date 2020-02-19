using Bitmex.Net.Objects;
using System;
using System.Text.Json;

namespace Bitmex.Net
{
    public class BitmexClient
    {
        public void Tedst()
        {
           var result = JsonSerializer.Deserialize<BitmexOrder>("{\"id\":\"42\"}");
        }
    }
}
