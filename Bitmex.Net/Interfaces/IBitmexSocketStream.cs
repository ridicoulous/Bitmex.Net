using Bitmex.Net.Client.Objects;
using Bitmex.Net.Client.Objects.Socket;
using Bitmex.Net.Client.Objects.Socket.Repsonses;
using Bitmex.Net.Client.Objects.Socket.Requests;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace   Bitmex.Net.Client.Interfaces
{
    public interface IBitmexSocketStream
    {
        event Action<BitmexSocketEvent<Announcement>> OnAnnouncementUpdate;
        event Action<BitmexSocketEvent<Chat>> OnChatMessageUpdate;
        event Action<BitmexSocketEvent<ConnectedUsers>> OnChatConnectionUpdate;
        event Action<BitmexSocketEvent<Funding>> OnFundingUpdate;
        event Action<BitmexSocketEvent<Instrument>> OnInstrimentUpdate;        
        event Action<BitmexSocketEvent<Insurance>> OnInsuranceUpdate;
        event Action<BitmexSocketEvent<Liquidation>> OnLiquidationUpdate;
        event Action<BitmexSocketEvent<BitmexOrderBookEntry>> OnOrderBookL2_25Update;
        event Action<BitmexSocketEvent<BitmexOrderBookEntry>> OnorderBookL2Update;
        event Action<BitmexSocketEvent<BitmexOrderBookL10>> OnOrderBook10Update;
        event Action<BitmexSocketEvent<GlobalNotification>> OnGlobalNotificationUpdate;
        event Action<BitmexSocketEvent<Quote>> OnQuotesUpdate;
        event Action<BitmexSocketEvent<Quote>> OnOneMinuteQuoteBinUpdate;
        event Action<BitmexSocketEvent<Quote>> OnFiveMinuteQuoteBinUpdate;
        event Action<BitmexSocketEvent<Quote>> OnOneHourQuoteBinUpdate;
        event Action<BitmexSocketEvent<Quote>> OnDailyQuoteBinUpdate;
        event Action<BitmexSocketEvent<Settlement>> OnSettlementUpdate;
        event Action<BitmexSocketEvent<BitmexTrade>> OnTradeUpdate;
        event Action<BitmexSocketEvent<TradeBin>> OnOneMinuteTradeBinUpdate;
        event Action<BitmexSocketEvent<TradeBin>> OnFiveMinuteTradeBinUpdate;
        event Action<BitmexSocketEvent<TradeBin>> OnOneHourTradeBinUpdate;
        event Action<BitmexSocketEvent<TradeBin>> OnDailyTradeBinUpdate;
              
        event Action<BitmexSocketEvent<Affiliate>> OnUserAffiliatesUpdate;
        event Action<BitmexSocketEvent<Execution>> OnUserExecutionsUpdate;
        event Action<BitmexSocketEvent<BitmexOrder>> OnUserOrdersUpdate;
        event Action<BitmexSocketEvent<Margin>> OnUserMarginUpdate;
        event Action<BitmexSocketEvent<BitmexPosition>> OnUserPositionsUpdate;
        event Action<BitmexSocketEvent<Transaction>> OnUserTransactionsUpdate;
        event Action<BitmexSocketEvent<Wallet>> OnUserWalletUpdate;

        Task<CallResult<UpdateSubscription>> SubscribeAsync(BitmexSubscribeRequest bitmexSubscribeRequest, CancellationToken ct = default);

        /// subscribe to orderbook updates
        /// </summary>
        /// <param name="onData">orderbook</param>
        /// <param name="symbol"></param>
        /// <param name="full">If true, subscribe to full orderbook, else - to top 25 levels</param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(Action<DataEvent<BitmexSocketEvent<BitmexOrderBookEntry>>> onData, string symbol = "", bool full = false, CancellationToken ct = default);

        
    }
}
