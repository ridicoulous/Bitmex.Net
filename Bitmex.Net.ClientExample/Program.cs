using Bitmex.Net.Client;
using System;
using System.Linq;
using System.Threading.Tasks;
using Bitmex.Net.Client.Helpers.Extensions;
using Microsoft.Extensions.Configuration;
using Bitmex.Net.Client.Objects.Socket;
using Newtonsoft.Json;
using Bitmex.Net.Client.Objects;
using System.Reactive.Linq;
using Bitmex.Net.Client.HistoricalData;
using System.Collections.Generic;
using CryptoExchange.Net.Logging;
using Bitmex.Net.Client.Objects.Socket.Requests;

namespace Bitmex.Net.ClientExample
{
    class Program
    {
        static List<BitmexOrderBookEntry> entries = new List<BitmexOrderBookEntry>();
        static async Task Main(string[] args)
        {

            var builder = new ConfigurationBuilder()
           .AddJsonFile("appconfig.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();

            //await TestHistoricalDataLoading();

            //var orderBook = new BitmexSymbolOrderBook("XBTUSD", new BitmexSocketOrderBookOptions("bmc", true)
            //{
            //    LogVerbosity = CryptoExchange.Net.Logging.LogVerbosity.Debug,
            //    LogWriters = new List<System.IO.TextWriter>() { new ThreadSafeFileWriter("bitmexob.log") },

            //});
            //orderBook.OnBestOffersChanged += OnBestOffersChanged;
            // await orderBook.StartAsync();
            //Console.ReadLine();

            //var c = new BitmexClient(new BitmexClientOptions(true)
            //{
            //    ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials(configuration["testnet:key"], configuration["testnet:secret"]),
            //});

            //  var o = await c.PlaceOrderAsync(new PlaceOrderRequest() { BitmexOrderType = BitmexOrderType.Limit, Price=42000,Side=BitmexOrderSide.Sell,Quantity=12, Symbol="XBTUSD",ClientOrderId= "e8QJEyRKyTxs254" } );
            //     var placed = await c.GetOrdersAsync(new BitmexRequestWithFilter().WithClientOrderIdFilter("e8QJEyRKyTxs254"));
            //var placed2 = await c.GetOrdersAsync(new BitmexRequestWithFilter().WithOrderIdFilter(o.Data.Id));

            // await Task.Delay(1000);
            // var o2 = await c.PlaceOrderAsync(new PlaceOrderRequest() { BitmexOrderType = BitmexOrderType.Limit, Price=43000,Side=BitmexOrderSide.Sell,Quantity=12, Symbol="XBTUSD" } );

            //  var cancel = await c.CancelOrderAsync(new CancelOrderRequest(new string[] { o.Data.Id }));


            //var socket = new BitmexSocketClient(new BitmexSocketClientOptions(configuration["prod:key"], configuration["prod:secret"], isTestnet: false)
            //{
            //    LogVerbosity = CryptoExchange.Net.Logging.LogVerbosity.Debug,
            //    SocketNoDataTimeout = TimeSpan.FromSeconds(45)
            //});
            var socket = new BitmexSocketClient(new BitmexSocketClientOptions() { LogVerbosity = LogVerbosity.Debug, LogWriters = new List<System.IO.TextWriter>() { new ThreadSafeFileWriter("socket.log") } });

            //socket.OnUserWalletUpdate += Socket_OnUserWalletUpdate;
            socket.OnOrderBook10Update += Socket_OnOrderBook10Update;

            //socket.OnUserExecutionsUpdate += OnExecution;
            // socket.OnUserPositionsUpdate += BitmexSocketClient_OnUserPositionsUpdate;
            // socket.OnorderBookL2Update += Socket_OnOrderBookL2_25Update;
            socket.OnTradeUpdate += Socket_OnTradeUpdate;
            //  socket.OnSocketClose += Socket_OnSocketClose;
            // socket.OnSocketException += Socket_OnSocketException;
            //socket.OnChatMessageUpdate += Socket_OnChatMessageUpdate;
             socket.Subscribe(new BitmexSubscribeRequest()
                .AddSubscription(BitmexSubscribtions.Trade, "XBTUSD"));
            // .AddSubscription(BitmexSubscribtions.Execution, "XBTUSD")
            //.AddSubscription(BitmexSubscribtions.Wallet));
            Console.ReadLine();           
            await socket.UnsubscribeAll();
            Console.ReadLine();

        }

        private static void Socket_OnOrderBookL2_25Update1(BitmexSocketEvent<BitmexOrderBookEntry> obj)
        {
            Console.WriteLine("obl25");
        }

        private static async Task TestHistoricalDataLoading()
        {
            BitmexHistoricalTradesLoader bitmexHistoricalTradesLoader = new BitmexHistoricalTradesLoader();
            var data = await bitmexHistoricalTradesLoader.GetDailyTradesAsync(new DateTime(2020, 5, 20));
            var data2 = await bitmexHistoricalTradesLoader.GetTradesByPeriodAsync(new DateTime(2020, 5, 20), new DateTime(2020, 5, 21));

            var quotes = await bitmexHistoricalTradesLoader.GetDailyQuotesAsync(new DateTime(2020, 6, 20));
        }
        private static void Socket_OnOrderBook10Update(BitmexSocketEvent<BitmexOrderBookL10> obj)
        {
            foreach (var u in obj.Data)
            {
                Console.WriteLine($"{DateTime.UtcNow.Subtract(u.Timestamp).TotalMilliseconds} ms diff");

            }
        }

        private static void OnBestOffersChanged((CryptoExchange.Net.Interfaces.ISymbolOrderBookEntry BestBid, CryptoExchange.Net.Interfaces.ISymbolOrderBookEntry BestAsk) tuple)
        {
            Console.WriteLine($"up {tuple.BestAsk.Price}:{tuple.BestBid.Price}");
        }

        private static void Socket_OnUserWalletUpdate(BitmexSocketEvent<Wallet> obj)
        {
            Console.WriteLine("Current balance is " + obj.Data[0].BtcAmount);
        }

        private static void Socket_OnChatMessageUpdate(BitmexSocketEvent<Chat> obj)
        {
            if (obj.Data.Any())
                Console.WriteLine($"{obj.Data[0]?.Date}{obj.Data[0]?.Message}");
        }

        private static void Socket_OnSocketException(Exception obj)
        {
            Console.WriteLine($"{DateTime.UtcNow}:EXCEPTION: {obj}");
        }

        private static void Socket_OnSocketClose()
        {
            Console.WriteLine($"{DateTime.UtcNow}:CLOSED");

        }

        private static void Socket_OnTradeUpdate(BitmexSocketEvent<Trade> obj)
        {
            Console.WriteLine($"{DateTime.UtcNow}:trade");
        }

        private static void Socket_OnOrderBookL2_25Update(BitmexSocketEvent<BitmexOrderBookEntry> obj)
        {
            entries.AddRange(obj.Data);
            foreach (var u in obj.Data)
            {
                Console.WriteLine($"{u.Size} by  { ((1e8m * 88) - u.Id) * 0.01m}");
            }
        }

        private static void BitmexSocketClient_OnUserPositionsUpdate(BitmexSocketEvent<Position> obj)
        {
            Console.WriteLine($"{DateTime.UtcNow}:Catched POSITION {JsonConvert.SerializeObject(obj.AsDictionary())}");
            Console.WriteLine();
        }

        private static void OnExecution(BitmexSocketEvent<Execution> obj)
        {
            Console.WriteLine($"{DateTime.UtcNow}:Catched EXEC {JsonConvert.SerializeObject(obj.AsDictionary())}");
            Console.WriteLine();
        }

        private static void OnBitmexOrderUpdate(BitmexSocketEvent<Order> obj)
        {
            Console.WriteLine($"{DateTime.UtcNow}:Catched ORDER {JsonConvert.SerializeObject(obj.AsDictionary())}");
            Console.WriteLine();
        }


    }
}
