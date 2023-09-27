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
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Bitmex.Net
{
    public class BitmexClient: BaseRestClient, IBitmexClient
    {
        private const string ExchangeName = "Bitmex";

        public BitmexClient() : this(BitmexRestOptions.Default)
        {
        }
        // public BitmexClient(BitmexClientOptions options) : this(ExchangeName, options)
        // {
        // }
        public BitmexClient(BitmexRestOptions options, ILoggerFactory loggerFactory = null, HttpClient httpClient = null ) : base(loggerFactory, ExchangeName)
        {
            var opt = options ?? BitmexRestOptions.Default;
            Initialize(opt);
            SpotClient = AddApiClient(new BitmexSpotClient(_logger, httpClient, opt));
            MarginClient = AddApiClient(new BitmexMarginClient(_logger, httpClient, opt));
            NonTradeFeatureClient = AddApiClient(new BitmexNonTradeFeatureClient(_logger, httpClient, opt));
        }

        public IBitmexSpotClient SpotClient { get; }
        public IBitmexMarginClient MarginClient { get; }
        public IBitmexNonTradeFeaturesClient NonTradeFeatureClient { get; }

        public ISpotClient CommonSpotClient => SpotClient;
        public IFuturesClient CommonMarginClient => MarginClient;
       
    }
}