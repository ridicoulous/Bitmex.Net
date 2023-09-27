using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace Bitmex.Net.Client
{
    public class BitmexAuthenticationProvider : AuthenticationProvider
    {
        private readonly int LifetimeSeconds;

        private static readonly object nonceLock = new();
        private long lastNonce;
        public long ApiExpires
        {
            get
            {
                lock (nonceLock)
                {
                    var nonce = (long)Math.Round((DateTime.UtcNow - DateTime.UnixEpoch).TotalSeconds + LifetimeSeconds);
                    if (nonce == lastNonce)
                        nonce += 1;

                    lastNonce = nonce;
                    return lastNonce;
                }
            }
        }        
        public BitmexAuthenticationProvider(ApiCredentials credentials, TimeSpan? requestLifeTime = null) : base(credentials)
        {
            LifetimeSeconds = (int?)requestLifeTime?.TotalSeconds ?? 60;
        }
        public override void AuthenticateRequest(RestApiClient apiClient,
                                                 Uri uri,
                                                 HttpMethod method,
                                                 Dictionary<string, object> providedParameters,
                                                 bool auth,
                                                 ArrayParametersSerialization arraySerialization,
                                                 HttpMethodParameterPosition parameterPosition,
                                                 out SortedDictionary<string, object> uriParameters,
                                                 out SortedDictionary<string, object> bodyParameters,
                                                 out Dictionary<string, string> headers)
        {
            uriParameters = parameterPosition == HttpMethodParameterPosition.InUri ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
            bodyParameters = parameterPosition == HttpMethodParameterPosition.InBody ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
            headers = new Dictionary<string, string>();
            
            if (!auth)
                return;

            var apiexpires = ApiExpires;

            string additionalData = String.Empty;
            if (providedParameters != null && providedParameters.Any() && method != HttpMethod.Get)
            {
                additionalData = JsonConvert.SerializeObject(providedParameters);
            }
            var dataToSign = CreateAuthPayload(method, uri.PathAndQuery, apiexpires, additionalData);
            var signedData = Sign(dataToSign);
            headers.Add("api-key", _credentials.Key.GetString());
            headers.Add("api-expires", apiexpires.ToString(CultureInfo.InvariantCulture));
            headers.Add("api-signature", signedData);
        }
        public override string Sign(string toSign)
        {
            return SignHMACSHA256(toSign);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodAndUrl">GET/api/v1/orderBookL2 (for websocketauth - GET/realtime)</param>
        /// <param name="additionalData">optinal request data</param>
        /// <returns></returns>
        public string CreateAuthPayload(HttpMethod method, string requestUrl, long apiExpires, string additionalData = "")
        {
            return $"{method}{requestUrl}{apiExpires}{additionalData}";
        }
    }
}
