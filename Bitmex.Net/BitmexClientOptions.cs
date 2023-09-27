using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Options;
using System;

namespace Bitmex.Net.Client
{
    public class BitmexRestOptions : RestExchangeOptions
    {

        private const string ProductionEndpoint = "https://www.bitmex.com/api/v1";
        private const string TestNetEndpoint = "https://testnet.bitmex.com/api/v1";

        readonly bool isTestnet = false;
        public BitmexRestOptions(string key, string secret, bool isTest = false, bool outputOriginalData = false) : this(new ApiCredentials(key, secret), isTest, outputOriginalData)
        {
        }
        public BitmexRestOptions() : this(null)
        {
        }
        public BitmexRestOptions(bool isTest) : this(null, isTest)
        {
        }
        
        private BitmexRestOptions(bool isTest, bool outputOriginalData) : base()
        {
            isTestnet = isTest;
            CommonApiOptions = new() { OutputOriginalData = outputOriginalData };
        }

        public BitmexRestOptions(ApiCredentials apiCredentials, bool isTest = false, bool outputOriginalData = false, RateLimitingBehaviour rateLimitingBehaviour = RateLimitingBehaviour.Wait) 
        : this(isTest, outputOriginalData)
        {
            ApiCredentials = apiCredentials;
            UpdateRateLimiters();
            CommonApiOptions.RateLimitingBehaviour = rateLimitingBehaviour;
        }

          /// <summary>
        /// Default options
        /// </summary>
        public static BitmexRestOptions Default { get; set; } = new BitmexRestOptions()
        {
        };

        internal RestApiOptions CommonApiOptions { get; set; }
        internal string BaseAddress => isTestnet ? TestNetEndpoint : ProductionEndpoint;
        public void SetApiCredentials(ApiCredentials credentials)
        {
            ApiCredentials = credentials;
            UpdateRateLimiters();
        }
        public BitmexRestOptions Copy()
        {
            var newOne = Copy<BitmexRestOptions>();
            newOne.CommonApiOptions = CommonApiOptions;
            return newOne;
        }

        private void UpdateRateLimiters()
        {
            CommonApiOptions.RateLimiters.Clear();
            CommonApiOptions.RateLimiters.Add(
                new RateLimiter().AddTotalRateLimit(
                    ApiCredentials is null ? 30 : 120,
                    TimeSpan.FromMinutes(1))
            );
            CommonApiOptions.RateLimiters.Add(
                    new RateLimiter().AddEndpointLimit(
                    BitmexMarginClient.GetEndPointsWithAdditionalRateLimiter(BaseAddress),
                    10,
                    TimeSpan.FromSeconds(1))
            );
        }
    }
}
