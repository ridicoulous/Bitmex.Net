using Bitmex.Net.Client.Objects;
using Bitmex.Net.Client.Objects.Socket.Repsonses;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace   Bitmex.Net.Client.Interfaces
{
    public interface IBitmexSocketClient
    {
        CallResult<UpdateSubscription> SubscribeToAllTrades(Action<BitmexTradeEvent> onData, string symbol = "");
        Task<CallResult<UpdateSubscription>> SubscribeToAllTradesAsync(Action<BitmexTradeEvent> onData, string symbol = "");
        CallResult<UpdateSubscription> SubscribeToUserOrderUpdates(Action<BitmexOrderUpdateEvent> onData, string symbol = "");
        Task<CallResult<UpdateSubscription>> SubscribeToUserOrderUpdatesAsync(Action<BitmexOrderUpdateEvent> onData, string symbol = "");
        /// <summary>
        /// subscribe to orderbook updates
        /// </summary>
        /// <param name="onData">orderbook</param>
        /// <param name="symbol"></param>
        /// <param name="orderBookLevelType">
        /// allowed: orderBookL2_25 - Top 25 levels of level 2 order book;
        /// orderBookL2 - Full level 2 order book;
        /// orderBook10 - Top 10 levels using traditional full book push;
        /// </param>
        /// <returns></returns>
        CallResult<UpdateSubscription> SubscribeToOrderBookUpdates(Action<BitmexOrderBookUpdateEvent> onData, string symbol="", string orderBookLevelType= "orderBookL2_25");
        /// <summary>
        /// subscribe to orderbook updates
        /// </summary>
        /// <param name="onData">orderbook</param>
        /// <param name="symbol"></param>
        /// <param name="orderBookLevelType"></param>
        /// <returns></returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(Action<BitmexOrderBookUpdateEvent> onData, string symbol = "", string orderBookLevelType = "orderBookL2_25");

        
    }
}
