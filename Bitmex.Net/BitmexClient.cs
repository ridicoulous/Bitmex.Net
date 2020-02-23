using Bitmex.Net.Interfaces;
using Bitmex.Net.Objects;
using Bitmex.Net.Objects.Requests;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Bitmex.Net
{
    public class BitmexClient : RestClient, IBitmexClient
    {
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
        private const string InsuranceEndpoint = "insurance";
        private const string LeaderBoardEndpoint = "leaderboard";
        private const string LeaderBoardByNameEndpoint = "leaderboard/name";
        private const string LiquidationEndpoint = "liquidation";
        private const string OrderEndpoint = "order";
        private const string OrderBulkEndpoint = "order/bulk";
        private const string OrderClosePositionEndpoint = "order/closePosition";
        private const string OrderAllEndpoint = "order/all";
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
        private const string ChemaWebsokcetHelpEndpoint = "schema/websocketHelp";
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
       
        public BitmexClient(BitmexClientOptions exchangeOptions, BitmexAuthenticationProvider authenticationProvider) : base(exchangeOptions, authenticationProvider)
        {
        }

        public WebCallResult<User> GetUserAccount() => GetUserAccountAsync().Result;

        public async Task<WebCallResult<User>> GetUserAccountAsync(CancellationToken ct = default)
        {
            return await SendRequest<User>(GetUrl(GetUserAccountEndpoint), HttpMethod.Get, ct, null, true, true);
        }
        protected Uri GetUrl(string endpoint)
        {           
            return new Uri($"{BaseAddress}/{endpoint}");
        }
    }
}
