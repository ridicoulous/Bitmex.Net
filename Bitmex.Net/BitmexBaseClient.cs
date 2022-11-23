using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Bitmex.Net.Client;
using Bitmex.Net.Client.Helpers.Extensions;
using Bitmex.Net.Client.Interfaces;
using Bitmex.Net.Client.Objects.Requests;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;

namespace Bitmex.Net
{
    public abstract class BitmexBaseClient : RestApiClient
    {
        internal static TimeSyncState TimeSyncState = new TimeSyncState(String.Empty);
        protected readonly BitmexClient baseClient;
        protected readonly Log log;
        protected BitmexBaseClient(string name, BitmexClientOptions options, CryptoExchange.Net.Logging.Log log, BitmexClient client) : base(options, options.CommonApiOptions)
        {
            ExchangeName = name;
            baseClient = client;
            this.log = log;
        }
        
        public string ExchangeName { get; }
        
        protected Uri GetUrl(string endpoint)
        {
            return new Uri($"{BaseAddress}{endpoint}");
        }
        
        /// <summary>
        /// Fill parameters in a path. Parameters are specified by '{}' and should be specified in occuring sequence
        /// </summary>
        /// <param name="path">The total path string</param>
        /// <param name="values">The values to fill</param>
        /// <returns></returns>
        protected static string FillPathParameter(string path, params string[] values)
        {
            foreach (var value in values)
            {
                var indexB = path.IndexOf("{", StringComparison.Ordinal);
                var indexE = path.IndexOf("}", StringComparison.Ordinal);
                if (indexB >= 0 && indexE > indexB)
                {
                    path = path.Remove(indexB, indexE - indexB + 1);
                    path = path.Insert(indexB, value);
                }
            }
            return path;
        }

        protected Dictionary<string, object> GetParameters(BitmexRequestWithFilter requestWithFilter = null)
        {
            return requestWithFilter?.AsDictionary() ?? new Dictionary<string, object>();
        }
        protected async Task<WebCallResult<T>> SendRequestAsync<T>(string endpoint, HttpMethod method, CancellationToken ct = default, Dictionary<string, object> request = null, ArrayParametersSerialization? arraySerialization = null) where T : class
        {
            return await baseClient.SendRequestInternal<T>(
                this,
                GetUrl(endpoint),
                method,
                ct,
                request,
                AuthenticationProvider is not null,
                method == HttpMethod.Get ? HttpMethodParameterPosition.InUri : HttpMethodParameterPosition.InBody,
                arraySerialization
            ).ConfigureAwait(false);
        }

        #region RestApiClient methods
        public override TimeSpan GetTimeOffset() => TimeSpan.Zero;

        /// <inheritdoc />
        protected override TimeSyncInfo GetTimeSyncInfo() => new TimeSyncInfo(log, false, TimeSpan.MaxValue, TimeSyncState);

        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
        {
            // it doesn't need to implement because we don't use time synchronization
            throw new NotImplementedException();
        }

        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
        {
            return new BitmexAuthenticationProvider(credentials);
        }
        #endregion
    }
}