using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Bitmex.Net.Client;
using Bitmex.Net.Client.Helpers.Extensions;
using Bitmex.Net.Client.Interfaces;
using Bitmex.Net.Client.Objects;
using Bitmex.Net.Client.Objects.Requests;
using CryptoExchange.Net;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Interfaces.CommonClients;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;

namespace Bitmex.Net
{
    
    public abstract class BitmexBaseTradeClient : BitmexBaseClient, IBitmexCommonTradeClient
    {
        #region Endpoints
        private const string GetAllRawExecutionsEndpoint = "execution";
        private const string GetTradeHistoryEndpoint = "execution/tradeHistory";
        private const string InstrumentsEndpoint = "instrument";
        private const string ActiveInstrumentsEndpoint = "instrument/active";
        private const string ActiveInstrumentsAndIndiciesEndpoint = "instrument/activeAndIndices";
        private const string ActiveInstrumentIntervalsEndpoint = "instrument/activeIntervals";
        private const string InstrumentCompositeIndexEndpoint = "instrument/compositeIndex";
        private const string InstrumentIndiciesEndpoint = "instrument/indices";
        private const string InsuranceEndpoint = "insurance";
        protected const string OrderEndpoint = "order";
        protected const string OrderCancelAllEndpoint = "order/all";
        private const string OrderCancelAllAfterEndpoint = "order/cancelAllAfter";
        private const string OrderBookL2Endpoint = "orderBook/L2";
        private const string QuoteEndpoint = "quote";
        private const string QuoteBucketedEndpoint = "quote/bucketed";
        private const string SettlementEndpoint = "settlement";
        private const string StatsEndpoint = "stats";
        private const string StatsHistoryEndpoint = "stats/history";
        private const string StatsHistoryUSDndpoint = "stats/historyUSD";
        private const string TradeEndpoint = "trade";
        private const string TradeBucketedEndpoint = "trade/bucketed";
        private const string UserDepositAddressEndpoint = "user/depositAddress";
        private const string UserWalletEndpoint = "user/wallet";
        private const string UserWalletHistoryEndpoint = "user/walletHistory";
        private const string UserWalletSummaryEndpoint = "user/walletSummary";
        private const string UserExecutionHistoryEndpoint = "user/executionHistory";
        private const string UserMinWithdrawalFeeEndpoint = "user/minWithdrawalFee";
        private const string UserRequestWithdrawalEndpoint = "user/requestWithdrawal";
        private const string UserCancelWithdrawalEndpoint = "user/cancelWithdrawal";
        private const string UserConfirmWithdrawalEndpoint = "user/confirmWithdrawal";
        private const string UserConfirmEmailEndpoint = "user/confirmEmail";
        private const string UserAffiliateStatusEndpoint = "user/affiliateStatus";
        private const string UserCheckReferralCodeEndpoint = "user/checkReferralCode";
        private const string UserquoteFillRatioEndpoint = "user/quoteFillRatio";
        private const string UserLogoutEndpoint = "user/logout";
        private const string UserPreferencesEndpoint = "user/preferences";
        private const string GetUserAccountEndpoint = "user";
        private const string UserCommissionEndpoint = "user/commission";
        private const string UserMarginEndpoint = "user/margin";
        private const string UserCommunicationTokenEndpoint = "user/communicationToken";
        private const string UserEventEndpoint = "userEvent";
        private const string WalletAssetsEndpoint = "wallet/assets";

        #endregion
        protected BitmexBaseTradeClient(string name, BitmexClientOptions options, Log log, BitmexClient client) : base(name, options, log, client)
        {
        }

        public event Action<OrderId> OnOrderPlaced;
        public event Action<OrderId> OnOrderCanceled;

        #region Execution : Raw Order and Balance Data

        ///<inheritdoc/>
        public async Task<WebCallResult<List<Execution>>> GetExecutionsAsync(BitmexRequestWithFilter requestWithFilter = null, CancellationToken ct = default)
        {
            var parameters = GetParameters(requestWithFilter);
            return await SendRequestAsync<List<Execution>>(GetAllRawExecutionsEndpoint, HttpMethod.Get, ct, parameters);
        }

        ///<inheritdoc/>
        public async Task<WebCallResult<List<Execution>>> GetExecutionsTradeHistoryAsync(BitmexRequestWithFilter requestWithFilter = null, CancellationToken ct = default)
        {
            var parameters = GetParameters(requestWithFilter);
            return await SendRequestAsync<List<Execution>>(GetTradeHistoryEndpoint, HttpMethod.Get, ct, parameters);
        }

        #endregion
        #region Instrument : Tradeable Contracts, Indices, and History 

        ///<inheritdoc/>
        public async Task<WebCallResult<List<Instrument>>> GetInstrumentsAsync(BitmexRequestWithFilter requestWithFilter = null, CancellationToken ct = default)
        {
            var parameters = GetParameters(requestWithFilter);
            return await SendRequestAsync<List<Instrument>>(InstrumentsEndpoint, HttpMethod.Get, ct, parameters);
        }

        /// <inheritdoc cref="IBitmexMarginClient"/>        
        ///<inheritdoc/>
        public async Task<WebCallResult<List<Instrument>>> GetActiveInstrumentsAsync(CancellationToken ct = default)
        {
            return await SendRequestAsync<List<Instrument>>(ActiveInstrumentsEndpoint, HttpMethod.Get, ct);
        }

        ///<inheritdoc/>
        public async Task<WebCallResult<List<Instrument>>> GetActiveInstrumentsAndIndiciesAsync(CancellationToken ct = default)
        {
            return await SendRequestAsync<List<Instrument>>(ActiveInstrumentsAndIndiciesEndpoint, HttpMethod.Get, ct, null);
        }



        ///<inheritdoc/>
        public async Task<WebCallResult<InstrumentInterval>> GetActiveIntervalsAsync(CancellationToken ct = default)
        {
            return await SendRequestAsync<InstrumentInterval>(ActiveInstrumentIntervalsEndpoint, HttpMethod.Get, ct, null);
        }

        ///<inheritdoc/>
        public async Task<WebCallResult<List<IndexComposite>>> GetCompositeIndexAsync(BitmexRequestWithFilter requestWithFilter = null, CancellationToken ct = default)
        {
            var parameters = GetParameters(requestWithFilter);
            return await SendRequestAsync<List<IndexComposite>>(InstrumentCompositeIndexEndpoint, HttpMethod.Get, ct, parameters);
        }

        ///<inheritdoc/>
        public async Task<WebCallResult<List<Instrument>>> GetIndiciesAsync(CancellationToken ct = default)
        {
            return await SendRequestAsync<List<Instrument>>(InstrumentIndiciesEndpoint, HttpMethod.Get, ct);
        }
        #endregion
        #region Insurance : Insurance Fund Data 


        ///<inheritdoc/>
        public async Task<WebCallResult<List<Insurance>>> GetInsuranceFundHistoryAsync(CancellationToken ct = default)
        {
            return await SendRequestAsync<List<Insurance>>(InsuranceEndpoint, HttpMethod.Get, ct, null);
        }
        #endregion
        #region Order : Placement, Cancellation, Amending, and History 


        ///<inheritdoc/>
        public async Task<WebCallResult<List<BitmexOrder>>> GetOrdersAsync(BitmexRequestWithFilter requestWithFilter = null, CancellationToken ct = default)
        {
            var parameters = GetParameters(requestWithFilter);
            return await SendRequestAsync<List<BitmexOrder>>(OrderEndpoint, HttpMethod.Get, ct, parameters);
        }

        ///<inheritdoc/>
        public async Task<WebCallResult<BitmexOrder>> PlaceOrderAsync(PlaceOrderRequest placeOrderRequest, CancellationToken ct = default)
        {
            placeOrderRequest.Symbol.ValidateNotNull(nameof(placeOrderRequest.Symbol));
            var parameters = placeOrderRequest.AsDictionary();
            var result = await SendRequestAsync<BitmexOrder>(OrderEndpoint, HttpMethod.Post, ct, parameters);
            if (result.Success)
            {
                OnOrderPlaced?.Invoke(result.Data.ToCryptoExchangeOrderId());
            }
            return result;
        }

        ///<inheritdoc/>
        public async Task<WebCallResult<BitmexOrder>> UpdateOrderAsync(UpdateOrderRequest updateOrderRequest, CancellationToken ct = default)
        {
            if (!String.IsNullOrEmpty(updateOrderRequest.ClOrdId) && String.IsNullOrEmpty(updateOrderRequest.OrigClOrdId))
            {
                string clOrderId = updateOrderRequest.ClOrdId;
                updateOrderRequest.OrigClOrdId = clOrderId;
                updateOrderRequest.ClOrdId = null;
            }
            if (String.IsNullOrEmpty(updateOrderRequest.OrigClOrdId))
            {
                updateOrderRequest.Id.ValidateNotNull(nameof(updateOrderRequest.Id) + " (you have to send order id, parameter Id, received from Bitmex or your own identifier, parameter OrigClOrdId, sent on order posting)");
            }
            if (String.IsNullOrEmpty(updateOrderRequest.ClOrdId) && String.IsNullOrEmpty(updateOrderRequest.OrigClOrdId) && updateOrderRequest.ClOrdId == updateOrderRequest.OrigClOrdId)
            {
                updateOrderRequest.ClOrdId = null;
            }
            var parameters = updateOrderRequest.AsDictionary();
            parameters.ValidateNotNull(nameof(updateOrderRequest));
            return await SendRequestAsync<BitmexOrder>(OrderEndpoint, HttpMethod.Put, ct, parameters);
        }

        ///<inheritdoc/>
        public async Task<WebCallResult<List<BitmexOrder>>> CancelOrderAsync(CancelOrderRequest cancelOrderRequest, CancellationToken ct = default)
        {
            var parameters = cancelOrderRequest.AsDictionary();
            var result = await SendRequestAsync<List<BitmexOrder>>(OrderEndpoint, HttpMethod.Delete, ct, parameters, arraySerialization: ArrayParametersSerialization.Array);
            if (result)
            {
                foreach (var o in result.Data)
                {
                    if (!string.IsNullOrEmpty(o.Error))
                    {
                        log.Write(LogLevel.Error, $"Order {o.Id} cancelling error: {o.Error}");
                    }
                    else
                    {
                        OnOrderCanceled?.Invoke(o.ToCryptoExchangeOrderId());
                    }
                }
            }

            return result;
        }


        ///<inheritdoc/>
        public async Task<WebCallResult<List<BitmexOrder>>> CancelAllOrdersAsync(string symbol = null, BitmexRequestWithFilter requestWithFilter = null, string text = null, CancellationToken ct = default)
        {
            Dictionary<string, object> parameters = GetParameters(requestWithFilter);
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("text", text);
            var result = await SendRequestAsync<List<BitmexOrder>>(OrderCancelAllEndpoint, HttpMethod.Delete, ct, parameters);
            if (result.Success && OnOrderCanceled != null)
            {
                foreach (var o in result.Data)
                {
                    OnOrderCanceled.Invoke(o.ToCryptoExchangeOrderId());
                }
            }
            return result;
        }

        [ObsoleteAttribute(@"/order/bulk endpoints will no longer be supported, 
            this method just invokes PlaceOrderAsync() method for each PlaceOrderRequest object from placeOrderRequests.
            Use PlaceOrderAsync() instead of PlaceOrdersBulkAsync(), please.", false)]
        ///<inheritdoc/>
        public async Task<WebCallResult<List<BitmexOrder>>> PlaceOrdersBulkAsync(List<PlaceOrderRequest> placeOrderRequests, CancellationToken ct = default)
        {
            placeOrderRequests.ValidateNotNull(nameof(placeOrderRequests));
            List<Task<WebCallResult<BitmexOrder>>> results = new List<Task<WebCallResult<BitmexOrder>>>();
            await Task.Run(() =>
            {
                foreach (var req in placeOrderRequests)
                {
                    results.Add(PlaceOrderAsync(req));
                }
            });
            return results.FirstOrDefault().Result.As<List<BitmexOrder>>(results.Select(x => x.Result.Data).ToList());
        }

        [ObsoleteAttribute(@"/order/bulk endpoints will no longer be supported, 
            this method just invokes UpdateOrderAsync() method for each UpdateOrderRequest object from ordersToUpdate parameter.
            Use UpdateOrderAsync() instead of UpdateOrdersBulkAsync(), please.", false)]
        ///<inheritdoc/>
        public async Task<WebCallResult<List<BitmexOrder>>> UpdateOrdersBulkAsync(List<UpdateOrderRequest> ordersToUpdate, CancellationToken ct = default)
        {
            ordersToUpdate.ValidateNotNull(nameof(ordersToUpdate));
            List<Task<WebCallResult<BitmexOrder>>> results = new List<Task<WebCallResult<BitmexOrder>>>();
            await Task.Run(() =>
            {
                foreach (var req in ordersToUpdate)
                {
                    results.Add(UpdateOrderAsync(req));
                }
            });
            foreach (var faulted in results.Where(t => t.Exception != null))
            {
                log.Write(LogLevel.Error, faulted.Exception.Message);
            }
            return results.FirstOrDefault().Result.As<List<BitmexOrder>>(results.Select(x => x.Result.Data).ToList());
        }


        ///<inheritdoc/>
        public async Task<WebCallResult<object>> CancellAllAfterAsync(TimeSpan timeOut, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("timeOut", (long)timeOut.TotalMilliseconds);
            return await SendRequestAsync<object>(OrderCancelAllAfterEndpoint, HttpMethod.Post, ct, parameters);
        }

        #endregion
        #region OrderBook : Level 2 Book Data 
        ///<inheritdoc/>
        public async Task<WebCallResult<OrderBookL2>> GetOrderBookAsync(string symbol, int depth = 25, CancellationToken ct = default)
        {
            symbol.ValidateNotNull(nameof(symbol));
            var parameters = GetParameters();
            parameters.Add("symbol", symbol);
            parameters.Add("depth", depth);
            return await SendRequestAsync<OrderBookL2>(OrderBookL2Endpoint, HttpMethod.Get, ct, parameters);

        }
        #endregion
        #region Quote : Best Bid/Offer Snapshots & Historical Bins

        ///<inheritdoc/>
        public async Task<WebCallResult<List<Quote>>> GetQuotesAsync(BitmexRequestWithFilter requestWithFilter = null, CancellationToken ct = default)
        {
            var parameters = GetParameters(requestWithFilter);
            return await SendRequestAsync<List<Quote>>(QuoteEndpoint, HttpMethod.Get, ct, parameters);
        }

        ///<inheritdoc/>
        public async Task<WebCallResult<List<Quote>>> GetBucketedQuotesAsync(string binSize, BitmexRequestWithFilter requestWithFilter = null, CancellationToken ct = default)
        {
            var parameters = GetParameters(requestWithFilter);
            parameters.Add("binSize", binSize);
            return await SendRequestAsync<List<Quote>>(QuoteBucketedEndpoint, HttpMethod.Get, ct, parameters);
        }
        #endregion
        #region Settlement : Historical Settlement Data 

        ///<inheritdoc/>
        public async Task<WebCallResult<Settlement>> GetSettelmentAsync(BitmexRequestWithFilter requestWithFilter = null, CancellationToken ct = default)
        {
            var parameters = GetParameters(requestWithFilter);
            return await SendRequestAsync<Settlement>(SettlementEndpoint, HttpMethod.Get, ct, parameters);
        }
        #endregion
        #region Stats : Exchange Statistics 

        ///<inheritdoc/>
        public async Task<WebCallResult<List<Stats>>> GetExchangeStatsAsync(CancellationToken ct = default)
        {
            return await SendRequestAsync<List<Stats>>(StatsEndpoint, HttpMethod.Get, ct, null);
        }

        ///<inheritdoc/>
        public async Task<WebCallResult<List<StatsHistory>>> GetStatsHistoryAsync(CancellationToken ct = default)
        {
            return await SendRequestAsync<List<StatsHistory>>(StatsHistoryEndpoint, HttpMethod.Get, ct, null);
        }

        ///<inheritdoc/>
        public async Task<WebCallResult<List<StatsUSD>>> GetStatsHistoryUsdAsync(CancellationToken ct = default)
        {
            return await SendRequestAsync<List<StatsUSD>>(StatsHistoryUSDndpoint, HttpMethod.Get, ct, null);
        }

        #endregion
        #region Trade : Individual & Bucketed Trades 
        ///<inheritdoc/>
        public async Task<WebCallResult<List<BitmexTrade>>> GetTradesAsync(BitmexRequestWithFilter requestWithFilter = null, CancellationToken ct = default)
        {
            var parameters = GetParameters(requestWithFilter);
            return await SendRequestAsync<List<BitmexTrade>>(TradeEndpoint, HttpMethod.Get, ct, parameters);
        }

        ///<inheritdoc/>
        public async Task<WebCallResult<List<TradeBin>>> GetTradesBucketedAsync(string binSize, bool partial = false, BitmexRequestWithFilter requestWithFilter = null, CancellationToken ct = default)
        {
            var parameters = GetParameters(requestWithFilter);
            parameters.Add("binSize", binSize);
            parameters.Add("partial", partial);
            if (!parameters.ContainsKey("reverse"))
            {
                parameters.Add("reverse", true);
            }
            var result = await SendRequestAsync<List<TradeBin>>(TradeBucketedEndpoint, HttpMethod.Get, ct, parameters);
            return result.As(result ? result.Data.OrderBy(t => t.Timestamp).ToList() : null);
        }
        #endregion
        #region User : Account Operations

        ///<inheritdoc/>
        public async Task<WebCallResult<User>> GetUserAccountAsync(CancellationToken ct = default)
        {
            return await SendRequestAsync<User>(GetUserAccountEndpoint, HttpMethod.Get, ct, null);
        }
        ///<inheritdoc/>
        public async Task<WebCallResult<Wallet>> GetUserWalletOneCurrencyAsync(string currency = "XBt", CancellationToken ct = default)
        {
            var parameters = GetParameters();
            parameters.Add("currency", currency);
            return await SendRequestAsync<Wallet>(UserWalletEndpoint, HttpMethod.Get, ct, parameters);
        }
        ///<inheritdoc/>
        public async Task<WebCallResult<List<Wallet>>> GetUserWalletAllCurrenciesAsync(CancellationToken ct = default)
        {
            var parameters = GetParameters();
            parameters.Add("currency", "all");
            return await SendRequestAsync<List<Wallet>>(UserWalletEndpoint, HttpMethod.Get, ct, parameters);
        }

        ///<inheritdoc/>
        public async Task<WebCallResult<List<WalletHistory>>> GetUserWalletHistoryAsync(string currency = "all", int count = 100, int startFrom = 0, CancellationToken ct = default)
        {
            var parameters = GetParameters(new BitmexRequestWithFilter(){ Start = startFrom, Count = count});
            parameters.Add("currency", currency);

            return await SendRequestAsync<List<WalletHistory>>(UserWalletHistoryEndpoint, HttpMethod.Get, ct, parameters);
        }
        ///<inheritdoc/>
        public async Task<WebCallResult<List<Execution>>> GetUserExecutionHistoryAsync(BitmexRequestWithFilter requestWithFilter = null, CancellationToken ct = default)
        {
            var parameters = GetParameters(requestWithFilter);
            return await SendRequestAsync<List<Execution>>(UserExecutionHistoryEndpoint, HttpMethod.Get, ct, parameters);
        }

        ///<inheritdoc/>
        public async Task<WebCallResult<List<Margin>>> GetMarginStatusAllAssets(CancellationToken ct = default)
        {
            var parameters = GetParameters();
            parameters.Add("currency", "all");
            return await SendRequestAsync<List<Margin>>(UserMarginEndpoint, HttpMethod.Get, ct, parameters);
        }
        ///<inheritdoc/>
        public async Task<WebCallResult<Margin>> GetMarginStatusForAsset(string currency, CancellationToken ct = default)
        {
            var parameters = GetParameters();
            parameters.Add("currency", currency);
            return await SendRequestAsync<Margin>(UserMarginEndpoint, HttpMethod.Get, ct, parameters);
        }
        #endregion
        #region Assets
        ///<inheritdoc/>
        public async Task<WebCallResult<List<WalletAsset>>> GetWalletAssetsAsync(CancellationToken ct = default)
        {
            return await SendRequestAsync<List<WalletAsset>>(WalletAssetsEndpoint, HttpMethod.Get, ct);
        }
        #endregion

        #region IBaseRestClient Implementation
        public abstract string GetSymbolName(string baseAsset, string quoteAsset);

        async Task<WebCallResult<IEnumerable<Order>>> IBaseRestClient.GetOpenOrdersAsync(string symbol, CancellationToken ct)
        {
            var request = new BitmexRequestWithFilter()
            {
                Symbol = symbol,
            };
            request.AddFilter("open", true);
            var result = await GetOrdersAsync(request, ct);
            return result.As<IEnumerable<Order>>(result.Data?.Select(o => o.ToCryptoExchangeOrder()));
        }

        async Task<WebCallResult<IEnumerable<Order>>> IBaseRestClient.GetClosedOrdersAsync(string symbol, CancellationToken ct)
        {
            var request = new BitmexRequestWithFilter()
            {
                Symbol = symbol,
            };
            var result = await GetOrdersAsync(request.AddFilter("leavesQty", 0), ct);
            return result.As(result.Data?.OrderBy(x => x.Timestamp).Select(o => o.ToCryptoExchangeOrder()));
        }

        async Task<WebCallResult<OrderId>> IBaseRestClient.CancelOrderAsync(string orderId, string symbol, CancellationToken ct)
        {
            var foo = await CancelOrderAsync(new CancelOrderRequest(exchangeOrderId: orderId), ct);
            return foo.As(foo ? foo.Data.FirstOrDefault()?.ToCryptoExchangeOrderId() : null);
        }

        async Task<WebCallResult<Order>> IBaseRestClient.GetOrderAsync(string orderId, string symbol, CancellationToken ct)
        {
            var request = new BitmexRequestWithFilter()
            {
                Symbol = symbol
            };
            request.AddFilter("orderID", orderId);
            var foo = await GetOrdersAsync(request, ct);
            return foo.As(foo.Data?.FirstOrDefault()?.ToCryptoExchangeOrder());
        }

        async Task<WebCallResult<IEnumerable<Symbol>>> IBaseRestClient.GetSymbolsAsync(CancellationToken ct)
        {
            var result = await GetActiveInstrumentsAsync(ct);
            return result.As<IEnumerable<Symbol>>(result.Data.Select(s => s.ToCryptoExchangeSymbol()));
        }

        async Task<WebCallResult<IEnumerable<Ticker>>> IBaseRestClient.GetTickersAsync(CancellationToken ct)
        {
            var result = await GetActiveInstrumentsAsync(ct);
            return result.As<IEnumerable<Ticker>>(result.Data?.Select(ticker => ticker.ToCryptoExchangeTicker()));
        }

        async Task<WebCallResult<Ticker>> IBaseRestClient.GetTickerAsync(string symbol, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                throw new ArgumentException($"No value provided for parameter \"symbol\"");
            }
            var result = await GetInstrumentsAsync(new BitmexRequestWithFilter() { Symbol = symbol }, ct);

            return result.As<Ticker>(result.Data?.FirstOrDefault()?.ToCryptoExchangeTicker());
        }

        async Task<WebCallResult<IEnumerable<Trade>>> IBaseRestClient.GetRecentTradesAsync(string symbol, CancellationToken ct)
        {
            var result = await GetTradesAsync(new BitmexRequestWithFilter() { Symbol = symbol }, ct);
            return result.As<IEnumerable<Trade>>(result.Data?.Select(pt => pt.ToCryptoExchangeTrade()));
        }

        async Task<WebCallResult<IEnumerable<Balance>>> IBaseRestClient.GetBalancesAsync(string accountId, CancellationToken ct)
        {
            var result = await GetMarginStatusAllAssets(ct);
            return result.As<IEnumerable<Balance>>(result.Data?.Where(margin => accountId == null || margin.Account.ToString() == accountId)
                                                               .Select(margin => margin.ToCryptoExchangeBalance()));
        }
        async Task<WebCallResult<IEnumerable<UserTrade>>> IBaseRestClient.GetOrderTradesAsync(string orderId, string symbol, CancellationToken ct)
        {
            var request = new BitmexRequestWithFilter()
            {
                Symbol = symbol,
            };
            request.AddFilter("orderID", orderId);
            // Without "execType=Trade" filter the result will contain fundings payments
            // if orderID do not match with any order's ID
            request.AddFilter("execType", "Trade");
            var foo = await GetExecutionsTradeHistoryAsync(request, ct);
            return foo.As<IEnumerable<UserTrade>>(foo.Data?.Select(ex => ex.ToCryptoExchangeUserTrade()));
        }

        async Task<WebCallResult<OrderBook>> IBaseRestClient.GetOrderBookAsync(string symbol, CancellationToken ct)
        {
            var result = await GetOrderBookAsync(symbol, ct: ct);
            return result.As<OrderBook>(result.Data?.ToCryptoExchangeOrderBook());
        }


        /// <summary>
        /// timespan can be either 1 minute, 5 minutes, 1 hour, 1 day
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="timespan"> </param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        async Task<WebCallResult<IEnumerable<Kline>>> IBaseRestClient.GetKlinesAsync(string symbol, TimeSpan timespan, DateTime? startTime, DateTime? endTime, int? limit, CancellationToken ct)
        {
            var request = new BitmexRequestWithFilter()
            {
                Symbol = symbol,
                StartTime = startTime,
                EndTime = endTime,
                Count = limit ?? 100,
            };
            var binSize = timespan.ToBitmexTimeFrameString();
            var result = await GetTradesBucketedAsync(binSize, false, request, ct);
            return result.As<IEnumerable<Kline>>(result.Data?.Select(bin => bin.ToCryptoExchangeKline(timespan)));
        }
        #endregion

        protected async Task<WebCallResult<OrderId>> PlaceOrderAsync(string symbol, CommonOrderSide side, CommonOrderType type, decimal quantity, decimal? price, string accountId, string clientOrderId, CancellationToken ct)
        {
            var request = new PlaceOrderRequest(symbol)
            {
                Quantity = quantity,
                Side = side == CommonOrderSide.Buy ? BitmexOrderSide.Buy : BitmexOrderSide.Sell,
                Price = price,
                ClientOrderId = clientOrderId,
                BitmexOrderType = type switch
                {
                    CommonOrderType.Limit => BitmexOrderType.Limit,
                    CommonOrderType.Market => BitmexOrderType.Market,
                    _ when price is null => BitmexOrderType.Market,
                    _ => BitmexOrderType.Market,
                }
            };
            var result = await PlaceOrderAsync(request, ct);
            return result.As(result.Data?.ToCryptoExchangeOrderId());
        }



    }
}