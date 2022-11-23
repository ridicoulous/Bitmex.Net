using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Bitmex.Net.Client;
using Bitmex.Net.Client.Interfaces;
using Bitmex.Net.Client.Objects;
using CryptoExchange.Net;
using CryptoExchange.Net.Interfaces.CommonClients;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json.Linq;

namespace Bitmex.Net
{
    public class BitmexClient: BaseRestClient, IBitmexClient
    {
        private const string ExchangeName = "Bitmex";

        public BitmexClient(BitmexClientOptions options) : this(ExchangeName, options)
        {
        }
        public BitmexClient(string name, BitmexClientOptions options) : base(name, options)
        {
            var opt = options ?? BitmexClientOptions.Default;
            SpotClient = AddApiClient(new BitmexSpotClient(name, opt, log, this));
            MarginClient = AddApiClient(new BitmexMarginClient(name, opt, log, this));
            NonTradeFeatureClient = AddApiClient(new BitmexNonTradeFeatureClient(name, opt, log, this));
        }

        public IBitmexSpotClient SpotClient { get; }
        public IBitmexMarginClient MarginClient { get; }
        public IBitmexNonTradeFeaturesClient NonTradeFeatureClient { get; }

        public ISpotClient CommonSpotClient => SpotClient;
        public IFuturesClient CommonMarginClient => MarginClient;
        internal async Task<WebCallResult<T>> SendRequestInternal<T>(
            RestApiClient apiClient,
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
            return await base.SendRequestAsync<T>(apiClient, uri, method, cancellationToken, parameters, signed, postPosition, arraySerialization, requestWeight: weight);
        }
        protected override Error ParseErrorResponse(JToken error)
        {
            if (error["error"] != null)
            {
                var message = error["error"]?.ToString();// $"{(string)error["error"]["name"]}: {(string)error["error"]["message"]}";
                return new BitmexError(42, message, error);
            }
            return null;
        }
    }
}