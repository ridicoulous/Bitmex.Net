using System;
using Bitmex.Net.Client.Interfaces;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Interfaces.CommonClients;

namespace Bitmex.Net.Client.Interfaces
{
    public interface IBitmexClient : IRestClient
    {

        IBitmexSpotClient SpotClient { get; }
        IBitmexMarginClient MarginClient { get; }
        IBitmexNonTradeFeaturesClient NonTradeFeatureClient { get; }

        ISpotClient CommonSpotClient { get; }
        IFuturesClient CommonMarginClient { get; }
    }
}