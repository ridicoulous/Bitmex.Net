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
        private readonly HMACSHA256 encryptor;
        private readonly int LifetimeSeconds;

        private static readonly object nonceLock = new object();
        private long lastNonce;
        public long ApiExpires
        {
            get
            {
                lock (nonceLock)
                {
                    var nonce = (long)Math.Round((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds + LifetimeSeconds);
                    if (nonce == lastNonce)
                        nonce += 1;

                    lastNonce = nonce;
                    return lastNonce;
                }
            }
        }        
        public BitmexAuthenticationProvider(ApiCredentials credentials, TimeSpan? requestLifeTime = null) : base(credentials)
        {
            LifetimeSeconds = requestLifeTime.HasValue ? (int)requestLifeTime.Value.TotalSeconds : 60;
            encryptor = new HMACSHA256(Encoding.ASCII.GetBytes(credentials.Secret.GetString()));
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


            var apiexpires = ApiExpires;

            if (!auth)
                return;

            headers.Add("api-key", Credentials.Key.GetString());
            headers.Add("api-expires", apiexpires.ToString(CultureInfo.InvariantCulture));

            string additionalData = String.Empty;
            if (providedParameters != null && providedParameters.Any() && method != HttpMethod.Get)
            {
                additionalData = JsonConvert.SerializeObject(providedParameters.OrderBy(p => p.Key).ToDictionary(p => p.Key, p => p.Value));
            }
            var dataToSign = CreateAuthPayload(method, uri.ToString().Split(uri.Host, StringSplitOptions.None)[1], apiexpires, additionalData);
            var signedData = Sign(dataToSign);
            headers.Add("api-signature", signedData);
        }
        public string ByteArrayToString(byte[] ba)
        {
            var hex = new StringBuilder(ba.Length * 2);
            foreach (var b in ba)
                hex.AppendFormat("{0:X2}", b);
            return hex.ToString();
        }
        public override string Sign(string toSign)
        {
            return ByteArrayToString(encryptor.ComputeHash(Encoding.UTF8.GetBytes(toSign)));
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
