using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Bitmex.Net.Client.Interfaces;
using Bitmex.Net.Client.Objects;
using Bitmex.Net.Client.Objects.Socket;
using Bitmex.Net.Client.Objects.Socket.Repsonses;
using Bitmex.Net.Client.Objects.Socket.Requests;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bitmex.Net.Client
{
    public class BitmexSocketClient : BaseSocketClient, IBitmexSocketClient, IBitmexSocketStreamActions
    {

        public BitmexSocketClient() : this(BitmexSocketClientOptions.Default.Copy())
        {
        }

        public BitmexSocketClient(BitmexSocketClientOptions options) : base("Bitmex", options)
        {
            MainSocketStreams = AddApiClient(new BitmexSocketStream(log, this, options));
            NonTradeSocketStreams = AddApiClient(new BitmexSocketStream(log, this, options.CopyWithNonTradeSocketEndpoint()));
        }
        public BitmexSocketStream MainSocketStreams { get; set; }
        public BitmexSocketStream NonTradeSocketStreams { get; set; }

        #region events
        #region NonTradeSocketStreams events
        public event Action<BitmexSocketEvent<Announcement>> OnAnnouncementUpdate
        {
            add => NonTradeSocketStreams.OnAnnouncementUpdate += value;
            remove => NonTradeSocketStreams.OnAnnouncementUpdate -= value;
        }
        public event Action<BitmexSocketEvent<Chat>> OnChatMessageUpdate
        {
            add => NonTradeSocketStreams.OnChatMessageUpdate += value;
            remove => NonTradeSocketStreams.OnChatMessageUpdate -= value;
        }
        public event Action<BitmexSocketEvent<GlobalNotification>> OnGlobalNotificationUpdate
        {
            add => NonTradeSocketStreams.OnGlobalNotificationUpdate += value;
            remove => NonTradeSocketStreams.OnGlobalNotificationUpdate -= value;
        }
        #endregion NonTradeSocketStreams events
        #region MainSocketStreams events
        public event Action<BitmexSocketEvent<ConnectedUsers>> OnChatConnectionUpdate
        {
            add => MainSocketStreams.OnChatConnectionUpdate += value;
            remove => MainSocketStreams.OnChatConnectionUpdate -= value;
        }
        public event Action<BitmexSocketEvent<Funding>> OnFundingUpdate
        {
            add => MainSocketStreams.OnFundingUpdate += value;
            remove => MainSocketStreams.OnFundingUpdate -= value;
        }
        public event Action<BitmexSocketEvent<Instrument>> OnInstrimentUpdate
        {
            add => MainSocketStreams.OnInstrimentUpdate += value;
            remove => MainSocketStreams.OnInstrimentUpdate -= value;
        }
        public event Action<BitmexSocketEvent<Insurance>> OnInsuranceUpdate
        {
            add => MainSocketStreams.OnInsuranceUpdate += value;
            remove => MainSocketStreams.OnInsuranceUpdate -= value;
        }
        public event Action<BitmexSocketEvent<Liquidation>> OnLiquidationUpdate
        {
            add => MainSocketStreams.OnLiquidationUpdate += value;
            remove => MainSocketStreams.OnLiquidationUpdate -= value;
        }
        public event Action<BitmexSocketEvent<BitmexOrderBookEntry>> OnOrderBookL2_25Update
        {
            add => MainSocketStreams.OnOrderBookL2_25Update += value;
            remove => MainSocketStreams.OnOrderBookL2_25Update -= value;
        }
        public event Action<BitmexSocketEvent<BitmexOrderBookEntry>> OnorderBookL2Update
        {
            add => MainSocketStreams.OnorderBookL2Update += value;
            remove => MainSocketStreams.OnorderBookL2Update -= value;
        }
        public event Action<BitmexSocketEvent<BitmexOrderBookL10>> OnOrderBook10Update
        {
            add => MainSocketStreams.OnOrderBook10Update += value;
            remove => MainSocketStreams.OnOrderBook10Update -= value;
        }
        public event Action<BitmexSocketEvent<Quote>> OnQuotesUpdate
        {
            add => MainSocketStreams.OnQuotesUpdate += value;
            remove => MainSocketStreams.OnQuotesUpdate -= value;
        }
        public event Action<BitmexSocketEvent<Quote>> OnOneMinuteQuoteBinUpdate
        {
            add => MainSocketStreams.OnOneMinuteQuoteBinUpdate += value;
            remove => MainSocketStreams.OnOneMinuteQuoteBinUpdate -= value;
        }
        public event Action<BitmexSocketEvent<Quote>> OnFiveMinuteQuoteBinUpdate
        {
            add => MainSocketStreams.OnFiveMinuteQuoteBinUpdate += value;
            remove => MainSocketStreams.OnFiveMinuteQuoteBinUpdate -= value;
        }
        public event Action<BitmexSocketEvent<Quote>> OnOneHourQuoteBinUpdate
        {
            add => MainSocketStreams.OnOneHourQuoteBinUpdate += value;
            remove => MainSocketStreams.OnOneHourQuoteBinUpdate -= value;
        }
        public event Action<BitmexSocketEvent<Quote>> OnDailyQuoteBinUpdate
        {
            add => MainSocketStreams.OnDailyQuoteBinUpdate += value;
            remove => MainSocketStreams.OnDailyQuoteBinUpdate -= value;
        }
        public event Action<BitmexSocketEvent<Settlement>> OnSettlementUpdate
        {
            add => MainSocketStreams.OnSettlementUpdate += value;
            remove => MainSocketStreams.OnSettlementUpdate -= value;
        }
        public event Action<BitmexSocketEvent<BitmexTrade>> OnTradeUpdate
        {
            add => MainSocketStreams.OnTradeUpdate += value;
            remove => MainSocketStreams.OnTradeUpdate -= value;
        }
        public event Action<BitmexSocketEvent<TradeBin>> OnOneMinuteTradeBinUpdate
        {
            add => MainSocketStreams.OnOneMinuteTradeBinUpdate += value;
            remove => MainSocketStreams.OnOneMinuteTradeBinUpdate -= value;
        }
        public event Action<BitmexSocketEvent<TradeBin>> OnFiveMinuteTradeBinUpdate
        {
            add => MainSocketStreams.OnFiveMinuteTradeBinUpdate += value;
            remove => MainSocketStreams.OnFiveMinuteTradeBinUpdate -= value;
        }
        public event Action<BitmexSocketEvent<TradeBin>> OnOneHourTradeBinUpdate
        {
            add => MainSocketStreams.OnOneHourTradeBinUpdate += value;
            remove => MainSocketStreams.OnOneHourTradeBinUpdate -= value;
        }
        public event Action<BitmexSocketEvent<TradeBin>> OnDailyTradeBinUpdate
        {
            add => MainSocketStreams.OnDailyTradeBinUpdate += value;
            remove => MainSocketStreams.OnDailyTradeBinUpdate -= value;
        }
        public event Action<BitmexSocketEvent<Affiliate>> OnUserAffiliatesUpdate
        {
            add => MainSocketStreams.OnUserAffiliatesUpdate += value;
            remove => MainSocketStreams.OnUserAffiliatesUpdate -= value;
        }
        public event Action<BitmexSocketEvent<Execution>> OnUserExecutionsUpdate
        {
            add => MainSocketStreams.OnUserExecutionsUpdate += value;
            remove => MainSocketStreams.OnUserExecutionsUpdate -= value;
        }
        public event Action<BitmexSocketEvent<BitmexOrder>> OnUserOrdersUpdate
        {
            add => MainSocketStreams.OnUserOrdersUpdate += value;
            remove => MainSocketStreams.OnUserOrdersUpdate -= value;
        }
        public event Action<BitmexSocketEvent<Margin>> OnUserMarginUpdate
        {
            add => MainSocketStreams.OnUserMarginUpdate += value;
            remove => MainSocketStreams.OnUserMarginUpdate -= value;
        }
        public event Action<BitmexSocketEvent<BitmexPosition>> OnUserPositionsUpdate
        {
            add => MainSocketStreams.OnUserPositionsUpdate += value;
            remove => MainSocketStreams.OnUserPositionsUpdate -= value;
        }
        public event Action<BitmexSocketEvent<Transaction>> OnUserTransactionsUpdate
        {
            add => MainSocketStreams.OnUserTransactionsUpdate += value;
            remove => MainSocketStreams.OnUserTransactionsUpdate -= value;
        }
        public event Action<BitmexSocketEvent<Wallet>> OnUserWalletUpdate
        {
            add => MainSocketStreams.OnUserWalletUpdate += value;
            remove => MainSocketStreams.OnUserWalletUpdate -= value;
        }
        #endregion MainSocketStreams events

        public event Action OnPongReceived
        {
            add
            {
                MainSocketStreams.OnPongReceived += value;
                NonTradeSocketStreams.OnPongReceived += value;
            }
            remove
            {
                MainSocketStreams.OnPongReceived -= value;
                NonTradeSocketStreams.OnPongReceived -= value;
            }
        }
        #endregion

        public Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(Action<DataEvent<BitmexSocketEvent<BitmexOrderBookEntry>>> onData, string symbol = "", bool full = false, CancellationToken ct = default)
            => MainSocketStreams.SubscribeToOrderBookUpdatesAsync(onData, symbol, full, ct);

        public async Task<IEnumerable<CallResult<UpdateSubscription>>> SubscribeAsync(BitmexSubscribeRequest bitmexSubscribeRequest, CancellationToken ct = default)
        {
            var nontradeSub = bitmexSubscribeRequest.PopNonTradeSubscriptions();
            var tasks = new List<Task<CallResult<UpdateSubscription>>>();
            if (bitmexSubscribeRequest.Args.Any())
                tasks.Add(MainSocketStreams.SubscribeAsync(bitmexSubscribeRequest, ct));
            if (nontradeSub.Args.Any())
                tasks.Add(NonTradeSocketStreams.SubscribeAsync(nontradeSub, ct));
            return await Task.WhenAll(tasks);
        }

        public async Task UnsubscribeAsync(IEnumerable<UpdateSubscription> subscriptions)
        {
            await Task.WhenAll(subscriptions.Select(sub => UnsubscribeAsync(sub)));
        }
    }
}