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
        CallResult<UpdateSubscription> SubscribeToTrades(Action<BitmexTradeEvent> OnData, string symbol = "");
        Task<CallResult<UpdateSubscription>> SubscribeToTradesAsync(Action<BitmexTradeEvent> OnData, string symbol = "");

    }
}
