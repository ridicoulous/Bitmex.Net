using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;

namespace Bitmex.Net.Client
{
    public class BitmexClientOptions : BaseRestClientOptions
    {

        private const string ProductionEndpoint = "https://www.bitmex.com/api/v1";
        private const string TestNetEndpoint = "https://testnet.bitmex.com/api/v1";

        public BitmexClientOptions(HttpClient client, string key, string secret, bool isTest = false) : this(new ApiCredentials(key, secret), isTest)
        {
            HttpClient = client;
        }
        public BitmexClientOptions(HttpClient client) : this(false)
        {
            HttpClient = client;
        }
        public BitmexClientOptions(string key, string secret, bool isTest = false) : this(new CryptoExchange.Net.Authentication.ApiCredentials(key, secret), isTest)
        {
        }

        public BitmexClientOptions(bool isTest = false) : base()
        {
            CommonApiOptions = new(isTest ? TestNetEndpoint : ProductionEndpoint);
            LogLevel = Microsoft.Extensions.Logging.LogLevel.Debug;
            LogWriters = new List<ILogger> { new DebugLogger() };
        }
        private BitmexClientOptions(BitmexClientOptions baseOn) : base(baseOn)
        {
            CommonApiOptions = baseOn.CommonApiOptions;
        }

        public BitmexClientOptions(ApiCredentials apiCredentials, bool isTest) : this(isTest)
        {
            ApiCredentials = apiCredentials;
        }

        /// <summary>
        /// Default options
        /// </summary>
        public static BitmexClientOptions Default { get; set; } = new BitmexClientOptions()
        {
        };

        internal RestApiClientOptions CommonApiOptions { get; set; }

        public void SetApiCredentials(ApiCredentials credentials)
        {
            ApiCredentials = credentials;
        }
        public BitmexClientOptions Copy()
        {
            return new BitmexClientOptions(this);
        }
    }
}
