using Bitmex.Net.Objects;
using System;
using System.Text.Json;
using Xunit;

namespace Bitmex.Net.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var result = JsonSerializer.Deserialize<BitmexOrder>("{\"id\":\"42\"}");
        }
    }
}
