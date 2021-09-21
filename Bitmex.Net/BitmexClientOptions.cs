using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;

namespace Bitmex.Net.Client
{
    public class BitmexClientOptions : RestClientOptions
    {
        public BitmexClientOptions() : base("https://www.bitmex.com/api/v1")
        {
            LogLevel = Microsoft.Extensions.Logging.LogLevel.Debug;
            LogWriters = new List<ILogger> { new DebugLogger() };
        }
        public BitmexClientOptions(HttpClient client, string key, string secret, bool isTest = false) : base(client, "https://www.bitmex.com/api/v1")
        {
            ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials(key, secret);
            if (isTest)
            {
                BaseAddress = "https://testnet.bitmex.com/api/v1";
            }
            LogLevel = Microsoft.Extensions.Logging.LogLevel.Debug;
            LogWriters = new List<ILogger> { new DebugLogger() };

        }
        public BitmexClientOptions(HttpClient client) : base(client, "https://www.bitmex.com/api/v1")
        {
            LogLevel = Microsoft.Extensions.Logging.LogLevel.Debug;
            LogWriters = new List<ILogger> { new DebugLogger() };
        }
        public BitmexClientOptions(string key, string secret, bool isTest = false) : base("https://www.bitmex.com/api/v1")
        {
            ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials(key, secret);
            if (isTest)
            {
                BaseAddress = "https://testnet.bitmex.com/api/v1";
            }
            LogLevel = Microsoft.Extensions.Logging.LogLevel.Debug;
            LogWriters = new List<ILogger> { new DebugLogger() };

        }

        public BitmexClientOptions(bool isTest = false) : base("https://www.bitmex.com/api/v1")
        {
            if (isTest)
            {
                BaseAddress = "https://testnet.bitmex.com/api/v1";
            }
            LogLevel = Microsoft.Extensions.Logging.LogLevel.Debug;
            LogWriters = new List<ILogger> { new DebugLogger() };
        }

        public void SetApiCredentials(ApiCredentials credentials)
        {
            ApiCredentials = credentials;
        }
        public BitmexClientOptions Copy()
        {
            var copy = Copy<BitmexClientOptions>();

            return copy;
        }
    }
}
