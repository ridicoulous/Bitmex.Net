using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Bitmex.Net.Client
{
    public class BitmexAuthenticationProvider : AuthenticationProvider
    {
        private readonly HMACSHA256 encryptor;
        private readonly int LifetimeSeconds;
        private static readonly DateTime EpochTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private long ApiExpires => (long)(DateTime.UtcNow - EpochTime).TotalSeconds + LifetimeSeconds;
        public BitmexAuthenticationProvider(ApiCredentials credentials, TimeSpan? requestLifeTime = null) : base(credentials)
        {
            LifetimeSeconds = requestLifeTime.HasValue ? (int)requestLifeTime.Value.TotalSeconds : 42;
            encryptor = new HMACSHA256(Encoding.ASCII.GetBytes(credentials.Secret.GetString()));
        }
        public override Dictionary<string, string> AddAuthenticationToHeaders(string uri, HttpMethod method, Dictionary<string, object> parameters, bool signed)
        {
            var apiexpires = ApiExpires;

            if (!signed)
                return new Dictionary<string, string>();

            var result = new Dictionary<string, string>();
            result.Add("api-key", Credentials.Key.GetString());
            result.Add("api-expires", apiexpires.ToString());

            string additionalData = String.Empty;
            if (parameters.Any() && method != HttpMethod.Delete)
            {
                additionalData = JsonConvert.SerializeObject(parameters.OrderBy(p => p.Key).ToDictionary(p => p.Key, p => p.Value));
            }
            var dataToSign = CreateAuthPayload(method, uri.Split(new[] { ".com" }, StringSplitOptions.None)[1], apiexpires, additionalData);
            var signedData = Sign(dataToSign);
            result.Add("api-signature", signedData);
            return result;
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
            return $"{method.ToString()}{requestUrl}{apiExpires}{additionalData}";
        }
        public object[] CreateWebsocketSignatureParams()
        {
            var apiExp = ApiExpires;
            return new object[] { Credentials.Key.GetString(), apiExp, Sign(CreateAuthPayload(HttpMethod.Get, "/realtime", apiExp)) };           
        }
    }
}
