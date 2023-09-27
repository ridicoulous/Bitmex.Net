using Bitmex.Net.Client.Interfaces;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Interfaces.CommonClients;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Bitmex.Net.Client
{
    public class BitmexSpotClient : BitmexBaseTradeClient, IBitmexSpotClient
    {
        internal BitmexSpotClient(ILogger logger, HttpClient httpClient, BitmexRestOptions opt)
        : base(logger, httpClient, opt)
        {
        }

        #region IBitmexSpotClient implementation
        public override string GetSymbolName(string baseAsset, string quoteAsset)
        {
            return $"{baseAsset}_{quoteAsset}";
        }

        async Task<WebCallResult<OrderId>> ISpotClient.PlaceOrderAsync(string symbol, CommonOrderSide side, CommonOrderType type, decimal quantity, decimal? price, string accountId, string clientOrderId, CancellationToken ct)
        => await base.PlaceOrderAsync(symbol, side, type, quantity, price, accountId, clientOrderId, ct);

        #endregion
    }
}