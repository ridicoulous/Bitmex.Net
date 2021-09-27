using Bitmex.Net.Client;
using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Bitmex.Net.Client.Objects.Socket.Requests;
using Bitmex.Net.Client.Objects.Socket;

using Bitmex.Net.Client.Objects;

using Bitmex.Net.Client.HistoricalData;
using System.Collections.Generic;
using CryptoExchange.Net.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Polly.Extensions.Http;
using Polly;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using CryptoExchange.Net.Sockets;
using Newtonsoft.Json;

namespace Bitmex.Net.ClientExample
{

    class Program
    {
        static void onData(DataEvent<BitmexSocketEvent<BitmexOrderBookEntry>> data)
        {
            Console.WriteLine(JsonConvert.SerializeObject(data));
        }
        static async Task Main(string[] args)
        {
            await TestHistoricalDataLoading();

            var s = new BitmexSocketClient();
            s.OnTradeUpdate += S_OnTradeUpdate;
            await s.SubscribeAsync(new BitmexSubscribeRequest().AddSubscription(BitmexSubscribtions.Trade, "XBTUSD"));
            Console.ReadLine();

            var socketBook = new BitmexSymbolOrderBook("XBTUSD");
            socketBook.OnBestOffersChanged += S_OnBestOffersChanged;
            await socketBook.StartAsync();

            var builder = new ConfigurationBuilder()
           .AddJsonFile("appconfig.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();


            var client = new BitmexClient(new BitmexClientOptions()
            {
                ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials(configuration["prod:key"], configuration["prod:secret"]),
            });
            var wallet = await client.GetUserWalletHistoryAsync();
            foreach (var r in wallet.Data)
            {
                Console.WriteLine($"[{r.Timestamp}]: {r.TransactStatus} - {r.WalletBalanceInBtc} BTC ({r.WalletBalance}) satoshi");
            }
            Console.WriteLine();

            using (var socket = new BitmexSocketClient(new BitmexSocketClientOptions(configuration["testnet:key"], configuration["testnet:secret"], isTestnet: true)
            {
                LogLevel = LogLevel.Debug,
                SocketNoDataTimeout = TimeSpan.FromSeconds(25),
                ReconnectInterval = TimeSpan.FromSeconds(5),
                AutoReconnect = true,
            }))
            {
                socket.OnUserOrdersUpdate += Socket_OnUserOrdersUpdate; ;
                socket.Subscribe(new BitmexSubscribeRequest()
                 .AddSubscription(BitmexSubscribtions.Order, "XBTUSD"));
                Console.ReadLine();
                await socket.UnsubscribeAllAsync();
                Console.ReadLine();

            }

            Console.ReadLine();
        }

        private static void S_OnTradeUpdate(BitmexSocketEvent<Trade> obj)
        {
            Console.WriteLine(JsonConvert.SerializeObject(obj));
        }

        private static void S_OnBestOffersChanged((CryptoExchange.Net.Interfaces.ISymbolOrderBookEntry BestBid, CryptoExchange.Net.Interfaces.ISymbolOrderBookEntry BestAsk) obj)
        {
            Console.WriteLine($"{obj.BestAsk.Price}:{obj.BestBid.Price}");
        }

     

        private static void Socket_OnUserOrdersUpdate(BitmexSocketEvent<Order> obj)
        {
            foreach (var o in obj.Data)
            {
                Console.WriteLine($"{o.Symbol}:{o.Side}:{o.Id}");
            }
        }

        private static async Task TestHistoricalDataLoading()
        {
            BitmexHistoricalTradesLoader bitmexHistoricalTradesLoader = new BitmexHistoricalTradesLoader();
            var data = await bitmexHistoricalTradesLoader.GetDailyTradesAsync(new DateTime(2021, 1, 6));
            Console.WriteLine($"Loaded {data.Count} trades");
            //var quotes = await bitmexHistoricalTradesLoader.GetDailyQuotesAsync(new DateTime(2020, 6, 20));
        }


    }
}
