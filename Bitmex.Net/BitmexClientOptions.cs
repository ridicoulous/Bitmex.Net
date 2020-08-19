using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Bitmex.Net.Client
{
    public class BitmexClientOptions : RestClientOptions
    {
        public BitmexClientOptions() : base("https://www.bitmex.com/api/v1")
        {
            this.LogVerbosity = CryptoExchange.Net.Logging.LogVerbosity.Debug;
            LogWriters = new List<System.IO.TextWriter>() { new DebugTextWriter() };

        }
        public BitmexClientOptions(HttpClient client, string key, string secret, bool isTest = false) : base(client, "https://www.bitmex.com/api/v1")
        {
            ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials(key, secret);
            if (isTest)
            {
                BaseAddress = "https://testnet.bitmex.com/api/v1";
            }
            this.LogVerbosity = CryptoExchange.Net.Logging.LogVerbosity.Debug;
            LogWriters = new List<System.IO.TextWriter>() { new DebugTextWriter() };

        }
        public BitmexClientOptions(HttpClient client) : base(client, "https://www.bitmex.com/api/v1")
        {            
            this.LogVerbosity = CryptoExchange.Net.Logging.LogVerbosity.Debug;
            LogWriters = new List<System.IO.TextWriter>() { new DebugTextWriter() };

        }
        public BitmexClientOptions(string key, string secret, bool isTest = false) : base("https://www.bitmex.com/api/v1")
        {
            ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials(key, secret);
            if (isTest)
            {
                BaseAddress = "https://testnet.bitmex.com/api/v1";
            }
            this.LogVerbosity = CryptoExchange.Net.Logging.LogVerbosity.Debug;
            LogWriters = new List<System.IO.TextWriter>() { new DebugTextWriter() };
        }

        public BitmexClientOptions(bool isTest = false) : base("https://www.bitmex.com/api/v1")
        {
            if (isTest)
            {
                BaseAddress = "https://testnet.bitmex.com/api/v1";
            }
            this.LogVerbosity = CryptoExchange.Net.Logging.LogVerbosity.Debug;
            LogWriters = new List<System.IO.TextWriter>() { new DebugTextWriter() };
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
