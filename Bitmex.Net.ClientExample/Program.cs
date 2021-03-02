using Bitmex.Net.Client;
using System;
using System.Linq;
using System.Threading.Tasks;
using Bitmex.Net.Client.Helpers.Extensions;
using Microsoft.Extensions.Configuration;
using Bitmex.Net.Client.Objects.Socket.Requests;
using Bitmex.Net.Client.Objects.Socket;
using System.Threading;
using System.Globalization;
using Newtonsoft.Json;
using Bitmex.Net.Client.Objects;
using Bitmex.Net.Client.Objects.Requests;
using System.Reactive.Linq;
using Bitmex.Net.Client.HistoricalData;
using System.Collections.Generic;
using CryptoExchange.Net.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Polly.Extensions.Http;
using Polly;
using System.Collections.Concurrent;

namespace Bitmex.Net.ClientExample
{   
    class Program
    {
     
        static async Task  Main(string[] args)
        {

            var builder = new ConfigurationBuilder()
           .AddJsonFile("appconfig.json", optional: true, reloadOnChange: true);
            //await TestHistoricalDataLoading();

          
            var configuration = builder.Build();

            var client = new BitmexClient(new BitmexClientOptions()
            {
                ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials(configuration["prod:key"], configuration["prod:secret"]),
            });
            var wallet = await client.GetUserWalletHistoryAsync();
            foreach(var r in wallet.Data)
            {
                Console.WriteLine($"[{r.Timestamp}]: {r.TransactStatus} - {r.WalletBalanceInBtc} BTC ({r.WalletBalance}) satoshi");
            }
            Console.WriteLine();
          
            using (var socket = new BitmexSocketClient(new BitmexSocketClientOptions(configuration["testnet:key"], configuration["testnet:secret"], isTestnet: true)
            {
                LogVerbosity = CryptoExchange.Net.Logging.LogVerbosity.Debug,
                SocketNoDataTimeout = TimeSpan.FromSeconds(25),
                ReconnectInterval = TimeSpan.FromSeconds(5),
                AutoReconnect = true,
                LogWriters = new List<System.IO.TextWriter>() { new ThreadSafeFileWriter("testnetreconnect.log"), new DebugTextWriter() }
            }))
            {
                socket.OnUserOrdersUpdate += Socket_OnUserOrdersUpdate; ;
                socket.Subscribe(new BitmexSubscribeRequest()
                 .AddSubscription(BitmexSubscribtions.Order, "XBTUSD"));
                Console.ReadLine();
                await socket.UnsubscribeAll();
                Console.ReadLine();
               

            }
            
            Console.ReadLine();
        }

        private static void Socket_OnUserOrdersUpdate(BitmexSocketEvent<Order> obj)
        {
            foreach(var o in obj.Data)
            {
                Console.WriteLine($"{o.Symbol}:{o.Side}:{o.Id}");
            }
        }

        private static async Task TestHistoricalDataLoading()
        {
            BitmexHistoricalTradesLoader bitmexHistoricalTradesLoader = new BitmexHistoricalTradesLoader();
            var data = await bitmexHistoricalTradesLoader.GetDailyTradesAsync(new DateTime(2020, 5, 20));
            var data2 = await bitmexHistoricalTradesLoader.GetTradesByPeriodAsync(new DateTime(2020, 5, 20), new DateTime(2020, 5, 21));

            var quotes = await bitmexHistoricalTradesLoader.GetDailyQuotesAsync(new DateTime(2020, 6, 20));
        }

    }
}
