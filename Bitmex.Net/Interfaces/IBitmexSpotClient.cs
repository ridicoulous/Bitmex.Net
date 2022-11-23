using System;
using Bitmex.Net.Client.Interfaces;
using CryptoExchange.Net.Interfaces.CommonClients;

namespace Bitmex.Net.Client.Interfaces
{
    public interface IBitmexSpotClient : IBitmexCommonTradeClient, ISpotClient
    {
    }
}