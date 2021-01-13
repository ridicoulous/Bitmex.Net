using Bitmex.Net.Client.Helpers.Extensions;
using Bitmex.Net.Client.Interfaces;
using Bitmex.Net.Client.Objects;
using Bitmex.Net.Client.Objects.Requests;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Bitmex.Net.Client
{
    public class BitmexClient : RestClient, IBitmexClient
    {
        private static BitmexClientOptions defaultOptions = new BitmexClientOptions();
        private static BitmexClientOptions DefaultOptions => defaultOptions.Copy();
        #region Endpoints
        private const string GetAnnouncementsEndpoint = "announcement";
        private const string GetUrgentAnnouncementsEndpoint = "announcement/urgent";
        private const string GetApiKeysEndpoint = "apiKey";
        private const string ChatMessagesEndpoint = "chat";
        private const string GetAvailableChannelsEndpoint = "/chat/channels";
        private const string GetConnectedUsersEndpoint = "/chat/connected";
        private const string GetAllRawExecutionsEndpoint = "execution";
        private const string GetTradeHistoryEndpoint = "execution/tradeHistory";
        private const string FundingEndpoint = "funding";
        private const string GlobalNotificationEndpoint = "globalNotification";
        private const string InstrumentsEndpoint = "instrument";
        private const string ActiveInstrumentsEndpoint = "instrument/active";
        private const string ActiveInstrumentsAndIndiciesEndpoint = "instrument/activeAndIndices";
        private const string ActiveInstrumentIntervalsEndpoint = "instrument/activeIntervals";
        private const string InstrumentCompositeIndexEndpoint = "instrument/compositeIndex";
        private const string InstrumentIndiciesEndpoint = "instrument/indicies";
        private const string InsuranceEndpoint = "insurance";
        private const string LeaderBoardEndpoint = "leaderboard";
        private const string LeaderBoardByNameEndpoint = "leaderboard/name";
        private const string LiquidationEndpoint = "liquidation";
        private const string OrderEndpoint = "order";
        private const string OrderBulkEndpoint = "order/bulk";
        private const string OrderClosePositionEndpoint = "order/closePosition";
        private const string OrderCancelAllEndpoint = "order/all";
        private const string OrderCancelAllAfterEndpoint = "order/cancelAllAfter";
        private const string OrderBookL2Endpoint = "orderBook/L2";
        private const string PositionEndpoint = "position";
        private const string PositionIsolateEndpoint = "position/isolate";
        private const string PositionRiskLimitEndpoint = "position/riskLimit";
        private const string PositionTransferMarginEndpoint = "position/transferMargin";
        private const string PositionLeverageEndpoint = "position/leverage";
        private const string QuoteEndpoint = "quote";
        private const string QuoteBucketedEndpoint = "quote/bucketed";
        private const string Schemandpoint = "schema";
        private const string SchemaWebsokcetHelpEndpoint = "schema/websocketHelp";
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
        #endregion       
        public BitmexClient() : this(DefaultOptions)
        {

        }
        public BitmexClient(HttpClient client) : base(nameof(BitmexClient),new BitmexClientOptions(client), null)
        {

        }

        public BitmexClient(BitmexClientOptions options) : base(nameof(BitmexClient),options, options.ApiCredentials == null ? null : new BitmexAuthenticationProvider(options.ApiCredentials))
        {

        }
        public BitmexClient(BitmexClientOptions exchangeOptions, BitmexAuthenticationProvider authenticationProvider) : base(nameof(BitmexClient), exchangeOptions, authenticationProvider)
        {
        }        
        public void SetApiCredentials(string key, string secret)
        {
            log.Write(CryptoExchange.Net.Logging.LogVerbosity.Debug, "Setting api credentials");
            this.authProvider = new BitmexAuthenticationProvider(new ApiCredentials(key, secret));
        }

        private Dictionary<string, object> GetParameters(BitmexRequestWithFilter requestWithFilter = null)
        {
            return requestWithFilter?.AsDictionary() ?? new Dictionary<string, object>();
        }

        public WebCallResult<List<Order>> CancelAllOrders(string symbol = null, BitmexRequestWithFilter requestWithFilter = null, string text = null) => CancelAllOrdersAsync(symbol, requestWithFilter, text).Result;
        public async Task<WebCallResult<List<Order>>> CancelAllOrdersAsync(string symbol = null, BitmexRequestWithFilter requestWithFilter = null, string text = null, CancellationToken ct = default)
        {
            Dictionary<string, object> parameters = GetParameters(requestWithFilter);
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("text", text);
            return await SendRequest<List<Order>>(GetUrl(OrderCancelAllEndpoint), HttpMethod.Delete, ct, parameters, true, false).ConfigureAwait(false);
        }

        public WebCallResult<object> CancellAllAfter(TimeSpan timeOut) => CancellAllAfterAsync(timeOut).Result;

        public async Task<WebCallResult<object>> CancellAllAfterAsync(TimeSpan timeOut, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("timeOut", (long)timeOut.TotalMilliseconds);
            return await SendRequest<object>(GetUrl(OrderCancelAllAfterEndpoint), HttpMethod.Post, ct, parameters, true, false).ConfigureAwait(false);
        }

        public WebCallResult<List<Order>> CancelOrder(CancelOrderRequest cancelOrderRequest) => CancelOrderAsync(cancelOrderRequest).Result;

        public async Task<WebCallResult<List<Order>>> CancelOrderAsync(CancelOrderRequest cancelOrderRequest, CancellationToken ct = default)
        {
            var parameters = cancelOrderRequest.AsDictionary();
            var result = await SendRequest<List<Order>>(GetUrl(OrderEndpoint), HttpMethod.Delete, ct, parameters, true, false).ConfigureAwait(false);
            if (result && result.Data.Any(c => !String.IsNullOrEmpty(c.Error)))
            {
                foreach (var o in result.Data)
                {
                    if (!String.IsNullOrEmpty(o.Error))
                    {
                        log.Write(CryptoExchange.Net.Logging.LogVerbosity.Error, $"Order {o.Id} cancelling error: {o.Error}");                        
                    }
                }
            }
            return result;
        }

        public WebCallResult<List<Instrument>> GetActiveInstruments() => GetActiveInstrumentsAsync().Result;
        /// <inheritdoc cref="IBitmexClient"/>        
        public async Task<WebCallResult<List<Instrument>>> GetActiveInstrumentsAsync(CancellationToken ct = default)
        {
            return await SendRequest<List<Instrument>>(GetUrl(InstrumentsEndpoint), HttpMethod.Get, ct, null, false).ConfigureAwait(false);
        }
        public WebCallResult<List<Instrument>> GetActiveInstrumentsAndIndicies() => GetActiveInstrumentsAndIndiciesAsync().Result;

        public async Task<WebCallResult<List<Instrument>>> GetActiveInstrumentsAndIndiciesAsync(CancellationToken ct = default)
        {
            return await SendRequest<List<Instrument>>(GetUrl(ActiveInstrumentsAndIndiciesEndpoint), HttpMethod.Get, ct, null, false, false).ConfigureAwait(false);
        }

        public WebCallResult<InstrumentInterval> GetActiveIntervals() => GetActiveIntervalsAsync().Result;

        public async Task<WebCallResult<InstrumentInterval>> GetActiveIntervalsAsync(CancellationToken ct = default)
        {
            return await SendRequest<InstrumentInterval>(GetUrl(ActiveInstrumentsAndIndiciesEndpoint), HttpMethod.Get, ct, null, false).ConfigureAwait(false);
        }

        public WebCallResult<List<Announcement>> GetAnnouncements(List<string> columns = null) => GetAnnouncementsAsync(columns).Result;

        public async Task<WebCallResult<List<Announcement>>> GetAnnouncementsAsync(List<string> columns = null, CancellationToken ct = default)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("columns", JsonConvert.SerializeObject(columns));
            return await SendRequest<List<Announcement>>(GetUrl(GetAnnouncementsEndpoint), HttpMethod.Get, ct, parameters, false, false).ConfigureAwait(false);
        }

        public WebCallResult<List<APIKey>> GetApiKeys(bool reverse = false) => GetApiKeysAsync(reverse).Result;

        public async Task<WebCallResult<List<APIKey>>> GetApiKeysAsync(bool reverse = false, CancellationToken ct = default)
        {
            return await SendRequest<List<APIKey>>(GetUrl(GetApiKeysEndpoint), HttpMethod.Get, ct, null, true, false).ConfigureAwait(false);
        }

        public WebCallResult<List<Quote>> GetBucketedQuotes(string binSize, BitmexRequestWithFilter requestWithFilter = null) => GetBucketedQuotesAsync(binSize, requestWithFilter).Result;
        public async Task<WebCallResult<List<Quote>>> GetBucketedQuotesAsync(string binSize, BitmexRequestWithFilter requestWithFilter = null, CancellationToken ct = default)
        {
            var parameters = GetParameters(requestWithFilter);
            parameters.Add("binSize", binSize);
            return await SendRequest<List<Quote>>(GetUrl(QuoteBucketedEndpoint), HttpMethod.Get, ct, parameters, false, false).ConfigureAwait(false);
        }
        public WebCallResult<List<ChatChannel>> GetChannels() => GetChannelsAsync().Result;

        public async Task<WebCallResult<List<ChatChannel>>> GetChannelsAsync(CancellationToken ct = default)
        {
            return await SendRequest<List<ChatChannel>>(GetUrl(GetAvailableChannelsEndpoint), HttpMethod.Get, ct, null, false, false).ConfigureAwait(false);
        }

        public WebCallResult<List<Chat>> GetChatMessages(int channelId, BitmexRequestWithFilter requestWithFilter = null) => GetChatMessagesAsync(channelId, requestWithFilter).Result;

        public async Task<WebCallResult<List<Chat>>> GetChatMessagesAsync(int channelId, BitmexRequestWithFilter requestWithFilter = null, CancellationToken ct = default)
        {
            var parameters = GetParameters(requestWithFilter);
            parameters.Add("channelId", channelId);
            return await SendRequest<List<Chat>>(GetUrl(ChatMessagesEndpoint), HttpMethod.Get, ct, null, false, false).ConfigureAwait(false);
        }

        public WebCallResult<object> GetSchema(string model) => GetSchemaAsync(model).Result;

        public async Task<WebCallResult<object>> GetSchemaAsync(string model, CancellationToken ct = default)
        {
            var parameters = GetParameters();
            parameters.Add("model", model);
            return await SendRequest<object>(GetUrl(Schemandpoint), HttpMethod.Get, ct, null, false, false).ConfigureAwait(false);
        }

        public WebCallResult<List<IndexComposite>> GetCompositeIndex(BitmexRequestWithFilter requestWithFilter = null) => GetCompositeIndexAsync(requestWithFilter).Result;

        public async Task<WebCallResult<List<IndexComposite>>> GetCompositeIndexAsync(BitmexRequestWithFilter requestWithFilter = null, CancellationToken ct = default)
        {
            var parameters = GetParameters(requestWithFilter);
            return await SendRequest<List<IndexComposite>>(GetUrl(InstrumentCompositeIndexEndpoint), HttpMethod.Get, ct, null, false, false).ConfigureAwait(false);
        }

        public WebCallResult<ConnectedUsers> GetConnectedUsers() => GetConnectedUsersAsync().Result;

        public async Task<WebCallResult<ConnectedUsers>> GetConnectedUsersAsync(CancellationToken ct = default)
        {
            return await SendRequest<ConnectedUsers>(GetUrl(GetConnectedUsersEndpoint), HttpMethod.Get, ct, null, false, false).ConfigureAwait(false);
        }
        public WebCallResult<List<Stats>> GetExchangeStats() => GetExchangeStatsAsync().Result;
        public async Task<WebCallResult<List<Stats>>> GetExchangeStatsAsync(CancellationToken ct = default)
        {
            return await SendRequest<List<Stats>>(GetUrl(StatsEndpoint), HttpMethod.Get, ct, null, false, false).ConfigureAwait(false);
        }

        public WebCallResult<List<Execution>> GetExecutions(BitmexRequestWithFilter requestWithFilter = null) => GetExecutionsAsync(requestWithFilter).Result;
        public async Task<WebCallResult<List<Execution>>> GetExecutionsAsync(BitmexRequestWithFilter requestWithFilter = null, CancellationToken ct = default)
        {
            var parameters = GetParameters(requestWithFilter);
            return await SendRequest<List<Execution>>(GetUrl(GetAllRawExecutionsEndpoint), HttpMethod.Get, ct, parameters, true, false).ConfigureAwait(false);
        }

        public WebCallResult<List<Execution>> GetExecutionsTradeHistory(BitmexRequestWithFilter requestWithFilter = null) => GetExecutionsTradeHistoryAsync(requestWithFilter).Result;

        public async Task<WebCallResult<List<Execution>>> GetExecutionsTradeHistoryAsync(BitmexRequestWithFilter requestWithFilter = null, CancellationToken ct = default)
        {
            var parameters = GetParameters(requestWithFilter);
            return await SendRequest<List<Execution>>(GetUrl(GetTradeHistoryEndpoint), HttpMethod.Get, ct, parameters, true, false).ConfigureAwait(false);
        }

        public WebCallResult<List<Funding>> GetFunding(BitmexRequestWithFilter requestWithFilter = null) => GetFundingAsync(requestWithFilter).Result;

        public async Task<WebCallResult<List<Funding>>> GetFundingAsync(BitmexRequestWithFilter requestWithFilter = null, CancellationToken ct = default)
        {
            var parameters = GetParameters(requestWithFilter);
            return await SendRequest<List<Funding>>(GetUrl(FundingEndpoint), HttpMethod.Get, ct, parameters, false, false).ConfigureAwait(false);
        }

        public WebCallResult<List<GlobalNotification>> GetGlobalNotifications() => GetGlobalNotificationsAsync().Result;

        public async Task<WebCallResult<List<GlobalNotification>>> GetGlobalNotificationsAsync(CancellationToken ct = default)
        {
            return await SendRequest<List<GlobalNotification>>(GetUrl(GlobalNotificationEndpoint), HttpMethod.Get, ct, null, false, false).ConfigureAwait(false);
        }
        public WebCallResult<List<Instrument>> GetIndicies() => GetIndiciesAsync().Result;
        public async Task<WebCallResult<List<Instrument>>> GetIndiciesAsync(CancellationToken ct = default)
        {
            return await SendRequest<List<Instrument>>(GetUrl(InstrumentIndiciesEndpoint), HttpMethod.Get, ct, null, false, false).ConfigureAwait(false);
        }

        public WebCallResult<List<Instrument>> GetInstruments(BitmexRequestWithFilter requestWithFilter = null) => GetInstrumentsAsync(requestWithFilter).Result;

        public async Task<WebCallResult<List<Instrument>>> GetInstrumentsAsync(BitmexRequestWithFilter requestWithFilter = null, CancellationToken ct = default)
        {
            var parameters = GetParameters(requestWithFilter);
            return await SendRequest<List<Instrument>>(GetUrl(InstrumentsEndpoint), HttpMethod.Get, ct, parameters, false, false).ConfigureAwait(false);
        }

        public WebCallResult<List<Insurance>> GetInsuranceFundHistory() => GetInsuranceFundHistoryAsync().Result;

        public async Task<WebCallResult<List<Insurance>>> GetInsuranceFundHistoryAsync(CancellationToken ct = default)
        {
            return await SendRequest<List<Insurance>>(GetUrl(InsuranceEndpoint), HttpMethod.Get, ct, null, false, false).ConfigureAwait(false);
        }
        public WebCallResult<List<Leaderboard>> GetLeaderBoard(string method) => GetLeaderBoardAsync(method).Result;

        public async Task<WebCallResult<List<Leaderboard>>> GetLeaderBoardAsync(string method, CancellationToken ct = default)
        {
            var parameters = GetParameters();
            parameters.Add("method", method);
            return await SendRequest<List<Leaderboard>>(GetUrl(LeaderBoardEndpoint), HttpMethod.Get, ct, parameters, false, false).ConfigureAwait(false);
        }

        public WebCallResult<string> GetLeaderBoardName() => GetLeaderBoardNameAsync().Result;

        public async Task<WebCallResult<string>> GetLeaderBoardNameAsync(CancellationToken ct = default)
        {
            return await SendRequest<string>(GetUrl(LeaderBoardEndpoint), HttpMethod.Get, ct, null, true, false).ConfigureAwait(false);
        }
        public WebCallResult<List<Liquidation>> GetLiquidations(BitmexRequestWithFilter requestWithFilter = null) => GetLiquidationsAsync(requestWithFilter).Result;

        public async Task<WebCallResult<List<Liquidation>>> GetLiquidationsAsync(BitmexRequestWithFilter requestWithFilter = null, CancellationToken ct = default)
        {
            var parameters = GetParameters(requestWithFilter);
            return await SendRequest<List<Liquidation>>(GetUrl(LiquidationEndpoint), HttpMethod.Get, ct, parameters, false, false).ConfigureAwait(false);
        }

        public WebCallResult<List<BitmexOrderBookEntry>> GetOrderBook(string symbol, int depth = 25) => GetOrderBookAsync(symbol, depth).Result;

        public async Task<WebCallResult<List<BitmexOrderBookEntry>>> GetOrderBookAsync(string symbol, int depth = 25, CancellationToken ct = default)
        {
            symbol.ValidateNotNull(nameof(symbol));
            var parameters = GetParameters();
            parameters.Add("symbol", symbol);
            parameters.Add("depth", depth);
            return await SendRequest<List<BitmexOrderBookEntry>>(GetUrl(OrderBookL2Endpoint), HttpMethod.Get, ct, parameters, false, false).ConfigureAwait(false);

        }

        public WebCallResult<List<Order>> GetOrders(BitmexRequestWithFilter requestWithFilter = null) => GetOrdersAsync(requestWithFilter).Result;

        public async Task<WebCallResult<List<Order>>> GetOrdersAsync(BitmexRequestWithFilter requestWithFilter = null, CancellationToken ct = default)
        {
            var parameters = GetParameters(requestWithFilter);
            return await SendRequest<List<Order>>(GetUrl(OrderEndpoint), HttpMethod.Get, ct, parameters, true, false).ConfigureAwait(false);
        }

        public WebCallResult<List<Position>> GetPositions(BitmexRequestWithFilter requestWithFilter = null) => GetPositionsAsync(requestWithFilter).Result;

        public async Task<WebCallResult<List<Position>>> GetPositionsAsync(BitmexRequestWithFilter requestWithFilter = null, CancellationToken ct = default)
        {
            var parameters = GetParameters(requestWithFilter);

            return await SendRequest<List<Position>>(GetUrl(PositionEndpoint), HttpMethod.Get, ct, parameters, true, false).ConfigureAwait(false);
        }

        public WebCallResult<List<Quote>> GetQuotes(BitmexRequestWithFilter requestWithFilter = null) => GetQuotesAsync(requestWithFilter).Result;

        public async Task<WebCallResult<List<Quote>>> GetQuotesAsync(BitmexRequestWithFilter requestWithFilter = null, CancellationToken ct = default)
        {
            var parameters = GetParameters(requestWithFilter);
            return await SendRequest<List<Quote>>(GetUrl(QuoteEndpoint), HttpMethod.Get, ct, parameters, false, false).ConfigureAwait(false);
        }

        public WebCallResult<Settlement> GetSettelment(BitmexRequestWithFilter requestWithFilter = null) => GetSettelmentAsync().Result;

        public async Task<WebCallResult<Settlement>> GetSettelmentAsync(BitmexRequestWithFilter requestWithFilter = null, CancellationToken ct = default)
        {
            var parameters = GetParameters(requestWithFilter);
            return await SendRequest<Settlement>(GetUrl(SettlementEndpoint), HttpMethod.Get, ct, parameters, false, false).ConfigureAwait(false);
        }

        public WebCallResult<List<StatsHistory>> GetStatsHistory() => GetStatsHistoryAsync().Result;

        public async Task<WebCallResult<List<StatsHistory>>> GetStatsHistoryAsync(CancellationToken ct = default)
        {
            return await SendRequest<List<StatsHistory>>(GetUrl(StatsHistoryEndpoint), HttpMethod.Get, ct, null, false, false).ConfigureAwait(false);
        }

        public WebCallResult<List<StatsUSD>> GetStatsHistoryUsd() => GetStatsHistoryUsdAsync().Result;

        public async Task<WebCallResult<List<StatsUSD>>> GetStatsHistoryUsdAsync(CancellationToken ct = default)
        {
            return await SendRequest<List<StatsUSD>>(GetUrl(StatsHistoryUSDndpoint), HttpMethod.Get, ct, null, false, false).ConfigureAwait(false);
        }

        public WebCallResult<List<Trade>> GetTrades(BitmexRequestWithFilter requestWithFilter = null) => GetTradesAsync(requestWithFilter).Result;

        public async Task<WebCallResult<List<Trade>>> GetTradesAsync(BitmexRequestWithFilter requestWithFilter = null, CancellationToken ct = default)
        {
            var parameters = GetParameters(requestWithFilter);
            return await SendRequest<List<Trade>>(GetUrl(TradeEndpoint), HttpMethod.Get, ct, parameters, false, false).ConfigureAwait(false);
        }

        public WebCallResult<List<TradeBin>> GetTradesBucketed(string binSize, bool partial = false, BitmexRequestWithFilter requestWithFilter = null) => GetTradesBucketedAsync(binSize, partial, requestWithFilter).Result;

        public async Task<WebCallResult<List<TradeBin>>> GetTradesBucketedAsync(string binSize, bool partial = false, BitmexRequestWithFilter requestWithFilter = null, CancellationToken ct = default)
        {
            var parameters = GetParameters(requestWithFilter);
            parameters.Add("binSize", binSize);
            parameters.Add("partial", partial);
            return await SendRequest<List<TradeBin>>(GetUrl(TradeBucketedEndpoint), HttpMethod.Get, ct, parameters, false, false).ConfigureAwait(false);
        }

        public WebCallResult<List<Announcement>> GetUrgentAnnouncements() => GetUrgentAnnouncementsAsync().Result;

        public async Task<WebCallResult<List<Announcement>>> GetUrgentAnnouncementsAsync(CancellationToken ct = default)
        {
            return await SendRequest<List<Announcement>>(GetUrl(GetUrgentAnnouncementsEndpoint), HttpMethod.Get, ct, null, false, false).ConfigureAwait(false);
        }

        public WebCallResult<User> GetUserAccount() => GetUserAccountAsync().Result;

        public async Task<WebCallResult<User>> GetUserAccountAsync(CancellationToken ct = default)
        {
            return await SendRequest<User>(GetUrl(GetUserAccountEndpoint), HttpMethod.Get, ct, null, true, false).ConfigureAwait(false);
        }

        public WebCallResult<object> GetWebsokcetHelp() => GetWebsokcetHelpAsync().Result;

        public async Task<WebCallResult<object>> GetWebsokcetHelpAsync(CancellationToken ct = default)
        {
            return await SendRequest<object>(GetUrl(SchemaWebsokcetHelpEndpoint), HttpMethod.Get, ct, null, false, false).ConfigureAwait(false);
        }

        public WebCallResult<Order> PlaceOrder(PlaceOrderRequest placeOrderRequest) => PlaceOrderAsync(placeOrderRequest).Result;

        public async Task<WebCallResult<Order>> PlaceOrderAsync(PlaceOrderRequest placeOrderRequest, CancellationToken ct = default)
        {
            placeOrderRequest.Symbol.ValidateNotNull(nameof(placeOrderRequest.Symbol));
            var parameters = placeOrderRequest.AsDictionary();
            return await SendRequest<Order>(GetUrl(OrderEndpoint), HttpMethod.Post, ct, parameters, true, false).ConfigureAwait(false);
        }

        public WebCallResult<List<Order>> PlaceOrdersBulk(List<PlaceOrderRequest> placeOrderRequests) => PlaceOrdersBulkAsync(placeOrderRequests).Result;

        public async Task<WebCallResult<List<Order>>> PlaceOrdersBulkAsync(List<PlaceOrderRequest> placeOrderRequests, CancellationToken ct = default)
        {
            placeOrderRequests.ValidateNotNull(nameof(placeOrderRequests));
            var parameters = GetParameters();
            parameters.Add("orders", placeOrderRequests);
            return await SendRequest<List<Order>>(GetUrl(OrderBulkEndpoint), HttpMethod.Post, ct, parameters, true, false).ConfigureAwait(false);
        }

        public WebCallResult<Chat> SendChatMessage(int channelId, string message) => SendChatMessageAsync(channelId, message).Result;

        public async Task<WebCallResult<Chat>> SendChatMessageAsync(int channelId, string message, CancellationToken ct = default)
        {
            var parameters = GetParameters();
            parameters.Add("channelId", channelId);
            parameters.Add("message", message);
            return await SendRequest<Chat>(GetUrl(ChatMessagesEndpoint), HttpMethod.Post, ct, parameters, true, false).ConfigureAwait(false);
        }

        public WebCallResult<Position> SetPositionIsolation(string symbol, bool isolate) => SetPositionIsolationAsync(symbol, isolate).Result;

        public async Task<WebCallResult<Position>> SetPositionIsolationAsync(string symbol, bool isolate, CancellationToken ct = default)
        {
            symbol.ValidateNotNull(nameof(symbol));
            var parameters = GetParameters();
            parameters.Add("symbol", symbol);
            parameters.Add("isolate", isolate);
            return await SendRequest<Position>(GetUrl(PositionIsolateEndpoint), HttpMethod.Post, ct, parameters, true, false).ConfigureAwait(false);
        }

        public WebCallResult<Position> SetPositionLeverage(string symbol, decimal leverage) => SetPositionLeverageAsync(symbol, leverage).Result;

        public async Task<WebCallResult<Position>> SetPositionLeverageAsync(string symbol, decimal leverage, CancellationToken ct = default)
        {
            symbol.ValidateNotNull(nameof(symbol));
            var parameters = GetParameters();
            parameters.Add("symbol", symbol);
            parameters.Add("leverage", leverage);
            return await SendRequest<Position>(GetUrl(PositionLeverageEndpoint), HttpMethod.Post, ct, parameters, true, false).ConfigureAwait(false);
        }

        public WebCallResult<Position> SetPositionRiskLimit(string symbol, decimal riskLimit) => SetPositionLeverageAsync(symbol, riskLimit).Result;

        public async Task<WebCallResult<Position>> SetPositionRiskLimitAsync(string symbol, decimal riskLimit, CancellationToken ct = default)
        {
            symbol.ValidateNotNull(nameof(symbol));

            var parameters = GetParameters();
            parameters.Add("symbol", symbol);
            parameters.Add("riskLimit", riskLimit);
            return await SendRequest<Position>(GetUrl(PositionRiskLimitEndpoint), HttpMethod.Post, ct, parameters, true, false).ConfigureAwait(false);
        }

        public WebCallResult<Position> SetPositionTransferMargin(string symbol, decimal amount) => SetPositionTransferMarginAsync(symbol, amount).Result;
        public async Task<WebCallResult<Position>> SetPositionTransferMarginAsync(string symbol, decimal amount, CancellationToken ct = default)
        {
            symbol.ValidateNotNull(nameof(symbol));
            var parameters = GetParameters();
            parameters.Add("symbol", symbol);
            parameters.Add("amount", amount);
            return await SendRequest<Position>(GetUrl(PositionTransferMarginEndpoint), HttpMethod.Post, ct, parameters, true, false).ConfigureAwait(false);
        }

        public WebCallResult<Order> UpdateOrder(UpdateOrderRequest updateOrderRequest) => UpdateOrderAsync(updateOrderRequest).Result;

        public async Task<WebCallResult<Order>> UpdateOrderAsync(UpdateOrderRequest updateOrderRequest, CancellationToken ct = default)
        {
            if (String.IsNullOrEmpty(updateOrderRequest.OrigClOrdId))
            {
                updateOrderRequest.Id.ValidateNotNull(nameof(updateOrderRequest.Id) + " (you have to send order id received from Bitmex or your own identifier, sended on order posting");
            }
            if (String.IsNullOrEmpty(updateOrderRequest.Id))
            {
                updateOrderRequest.OrigClOrdId.ValidateNotNull(nameof(updateOrderRequest.OrigClOrdId) + " (you have to send order id received from Bitmex or your own identifier, sended on order posting");
            }
            if (String.IsNullOrEmpty(updateOrderRequest.ClOrdId) && String.IsNullOrEmpty(updateOrderRequest.OrigClOrdId) && updateOrderRequest.ClOrdId == updateOrderRequest.OrigClOrdId)
            {
                updateOrderRequest.ClOrdId = null;
            }
            if (!String.IsNullOrEmpty(updateOrderRequest.ClOrdId) && String.IsNullOrEmpty(updateOrderRequest.OrigClOrdId))
            {
                string clOrderId = updateOrderRequest.ClOrdId;
                updateOrderRequest.OrigClOrdId = clOrderId;
                updateOrderRequest.ClOrdId = null;
            }
            var parameters = updateOrderRequest.AsDictionary();
            parameters.ValidateNotNull(nameof(updateOrderRequest));
            return await SendRequest<Order>(GetUrl(OrderEndpoint), HttpMethod.Put, ct, parameters, true, false).ConfigureAwait(false);
        }

        public WebCallResult<List<Order>> UpdateOrdersBulk(List<UpdateOrderRequest> ordersToUpdate) => UpdateOrdersBulkAsync(ordersToUpdate).Result;

        public async Task<WebCallResult<List<Order>>> UpdateOrdersBulkAsync(List<UpdateOrderRequest> ordersToUpdate, CancellationToken ct = default)
        {
            var parameters = GetParameters();
            parameters.Add("orders", ordersToUpdate);
            parameters.ValidateNotNull(nameof(ordersToUpdate));
            return await SendRequest<List<Order>>(GetUrl(OrderBulkEndpoint), HttpMethod.Put, ct, parameters, true, false).ConfigureAwait(false);
        }

        public WebCallResult<Wallet> GetUserWallet(string currency = "XBt") => GetUserWalletAsync(currency).Result;

        public async Task<WebCallResult<Wallet>> GetUserWalletAsync(string currency = "XBt", CancellationToken ct = default)
        {
            var parameters = GetParameters();
            parameters.Add("currency", currency);
            return await SendRequest<Wallet>(GetUrl(UserWalletEndpoint), HttpMethod.Get, ct, parameters, true, false).ConfigureAwait(false);
        }

        protected Uri GetUrl(string endpoint)
        {
            return new Uri($"{BaseAddress}{endpoint}");
        }
        protected override Error ParseErrorResponse(JToken error)
        {
            if (error["error"] != null)
            {
                var message = error["error"].ToString();// $"{(string)error["error"]["name"]}: {(string)error["error"]["message"]}";
                return new BitmexError(42, message, error);
            }
            return null;
        }
        public async Task<WebCallResult<List<WalletHistory>>> GetUserWalletHistoryAsync(string currency = "XBt", int count = 100, int startFrom=0, bool reverse=true,CancellationToken ct = default)
        {
            var parameters = GetParameters();
            parameters.Add("currency", currency);
            parameters.Add("count", count);

            return await SendRequest<List<WalletHistory>>(GetUrl(UserWalletHistoryEndpoint), HttpMethod.Get, ct, parameters, true, false).ConfigureAwait(false);
        }


        public WebCallResult<List<WalletHistory>> GetUserWalletHistory(string currency = "XBt", int count = 100, int startFrom = 0, bool reverse = true) => GetUserWalletHistoryAsync(currency, count,startFrom,reverse).Result;

        //public WebCallResult<List<Transaction>> GetUserWalletSummary(string currency = "XBt") => GetUserWalletSummaryAsync(currency).Result;
        //public async Task<WebCallResult<List<Transaction>>> GetUserWalletSummaryAsync(string currency = "XBt", CancellationToken ct = default)
        //{
        //    var parameters = GetParameters();
        //    parameters.Add("currency", currency);
        //    return await SendRequest<List<Transaction>>(GetUrl(UserWalletSummaryEndpoint), HttpMethod.Get, ct, parameters, true);
        //}
    }
}
