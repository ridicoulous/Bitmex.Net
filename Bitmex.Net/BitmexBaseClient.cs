using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Bitmex.Net.Client;
using Bitmex.Net.Client.Helpers.Extensions;
using Bitmex.Net.Client.Objects.Requests;
using Bitmex.Net.Objects.Errors;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Bitmex.Net
{
    public abstract class BitmexBaseClient : RestApiClient
    {
        protected BitmexBaseClient(ILogger logger, HttpClient? httpClient, BitmexRestOptions options)
            : base(logger, httpClient, options.BaseAddress, options, options.CommonApiOptions)
        {
        }
        
        
        protected Uri GetUrl(string endpoint)
        {
            return new Uri($"{BaseAddress}/{endpoint}");
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
            return await SendRequestInternal<T>(
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
        public override TimeSpan? GetTimeOffset() => null;

        /// <inheritdoc />
        public override TimeSyncInfo GetTimeSyncInfo() => null;

        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
        {
            return new BitmexAuthenticationProvider(credentials);
        }

        protected override Error ParseErrorResponse(int httpStatusCode, IEnumerable<KeyValuePair<string, IEnumerable<string>>> responseHeaders, string data)
        {
            var response = JsonConvert.DeserializeObject<BitmexErrorResponse>(data);
            if (response.Error != null)
            {
                return new ServerError(httpStatusCode, response.Error?.Message, response);
            }
            return null;
        }
        #endregion

        internal async Task<WebCallResult<T>> SendRequestInternal<T>(
           Uri uri,
           HttpMethod method,
           CancellationToken cancellationToken,
           Dictionary<string, object> parameters = null,
           bool signed = false,
           HttpMethodParameterPosition? postPosition = null,
           ArrayParametersSerialization? arraySerialization = null,
           int weight = 1
        ) where T : class
        {
            return await base.SendRequestAsync<T>(
                uri,
                method,
                cancellationToken,
                parameters,
                signed,
                postPosition,
                arraySerialization,
                requestWeight: weight);
        }
    }
}