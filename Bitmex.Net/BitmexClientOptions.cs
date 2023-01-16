using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Bitmex.Net.Client
{
    public class BitmexClientOptions : ClientOptions
    {

        private const string ProductionEndpoint = "https://www.bitmex.com/api/v1";
        private const string TestNetEndpoint = "https://testnet.bitmex.com/api/v1";

        public BitmexClientOptions(HttpClient client, string key, string secret, bool isTest = false, bool outputOriginalData = false) : this(new ApiCredentials(key, secret), isTest, outputOriginalData)
        {
            CommonApiOptions.HttpClient = client;
        }
        public BitmexClientOptions(HttpClient client) : this(false)
        {
            CommonApiOptions.HttpClient = client;
        }
        public BitmexClientOptions(string key, string secret, bool isTest = false, bool outputOriginalData = false) : this(new ApiCredentials(key, secret), isTest, outputOriginalData)
        {
        }

        public BitmexClientOptions(bool isTest = false, bool outputOriginalData = false) : base()
        {
            CommonApiOptions = new(isTest ? TestNetEndpoint : ProductionEndpoint) { OutputOriginalData = outputOriginalData };
            LogLevel = Microsoft.Extensions.Logging.LogLevel.Debug;
        }

        public BitmexClientOptions(ApiCredentials apiCredentials, bool isTest, bool outputOriginalData = false) : this(isTest, outputOriginalData)
        {
            ApiCredentials = apiCredentials;
            CommonApiOptions.RateLimiters.Add(
                new RateLimiter().AddTotalRateLimit(
                    ApiCredentials is null ? 30 : 120,
                    TimeSpan.FromMinutes(1))
            );
            CommonApiOptions.RateLimiters.Add(
                    new RateLimiter().AddEndpointLimit(
                    BitmexMarginClient.GetEndPointsWithAdditionalRateLimiter(CommonApiOptions.BaseAddress),
                    10,
                    TimeSpan.FromSeconds(1))
            );
            CommonApiOptions.RateLimitingBehaviour = RateLimitingBehaviour.Wait;
        }

        // for cloning this instance only
        private BitmexClientOptions(BitmexClientOptions baseOn) : base(baseOn)
        {
            CommonApiOptions = baseOn.CommonApiOptions;
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
