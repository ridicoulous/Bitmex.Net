
using System;
using System.Text.Json;
using Xunit;

namespace Bitmex.Net.Objects.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var client = new BitmexClient(new BitmexClientOptions(true), new BitmexAuthenticationProvider(new CryptoExchange.Net.Authentication.ApiCredentials("xWklWsCaUCrEjMFGl7oO1tFG", "BetX2XzPbFJuALcorDdq_S_8WRJ9MvvZflBouBQix-EjkYtf")));
            var result = client.GetUserAccount();
            Assert.True(result);
        }
    }
}
