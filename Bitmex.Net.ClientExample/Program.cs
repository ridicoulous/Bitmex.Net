using Bitmex.Net.Client;
using Bitmex.Net.Client.Objects.Socket.Repsonses;
using System;
using System.Threading.Tasks;
using Bitmex.Net.Client.Helpers.Extensions;
using Microsoft.Extensions.Configuration;
using Bitmex.Net.Client.Objects.Socket.Requests;
using Bitmex.Net.Client.Objects.Socket;
using System.Threading;
using System.Globalization;
using Newtonsoft.Json;
using System.Linq;
using Bitmex.Net.Client.Objects;
using Bitmex.Net.Client.Objects.Requests;

namespace Bitmex.Net.ClientExample
{
    class Program
    {
        static BitmexSymbolOrderBook ob;
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            var builder = new ConfigurationBuilder()
           .AddJsonFile("appconfig.json", optional: true, reloadOnChange: true);
            //ob = new BitmexSymbolOrderBook("XBTUSD", new BitmexSocketOrderBookOptions("XBT") { LogVerbosity = CryptoExchange.Net.Logging.LogVerbosity.Info });
            //ob.OnBestOffersChanged += Ob_OnBestOffersChanged;
            var t = new PlaceOrderRequest() { Price = 8050.500000000000000m, Quantity = 432.0000m };
            var r = t.AsDictionary();

            var configuration = builder.Build();
            //var c = new BitmexClient(new BitmexClientOptions(true) { ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials(configuration["testnet:key"], configuration["testnet:secret"]) });
            //    var b = c.GetUserWalletHistory(count:5);
           // var res = b.Data.Where(c => c.TransactType == "Total").Sum(c => c.Amount) - b.Data.Where(c => c.TransactType == "RealisedPNL").Sum(c => c.Amount);
            //var socket = new BitmexSocketClient(new BitmexSocketClientOptions(false) { LogVerbosity = CryptoExchange.Net.Logging.LogVerbosity.Debug });

            var socket = new BitmexSocketClient(new BitmexSocketClientOptions(configuration["key"], configuration["secret"], isTestnet: true)
            {
                LogVerbosity = CryptoExchange.Net.Logging.LogVerbosity.Debug,
                //SocketNoDataTimeout = TimeSpan.FromSeconds(45)
            });

            socket.OnUserWalletUpdate += Socket_OnUserWalletUpdate;

            //socket.OnUserExecutionsUpdate += OnExecution;
            // socket.OnUserPositionsUpdate += BitmexSocketClient_OnUserPositionsUpdate;
            //socket.OnOrderBookL2_25Update += Socket_OnOrderBookL2_25Update;
            //socket.OnTradeUpdate += Socket_OnTradeUpdate;
            //  socket.OnSocketClose += Socket_OnSocketClose;
            // socket.OnSocketException += Socket_OnSocketException;
            //socket.OnChatMessageUpdate += Socket_OnChatMessageUpdate;
            socket.Subscribe(new BitmexSubscribeRequest()
               //    .AddSubscription(BitmexSubscribtions.Order, "XBTUSD")
               //    .AddSubscription(BitmexSubscribtions.Execution, "XBTUSD")
               .AddSubscription(BitmexSubscribtions.Wallet));
            //    .AddSubscription(BitmexSubscribtions.Trade, "XBTUSD")
            //    .AddSubscription(BitmexSubscribtions.Chat)
            //    .AddSubscription(BitmexSubscribtions.OrderBookL2_25, "XBTUSD"));
            //socket.Subscribe(new BitmexSubscribeRequest()
            //   .AddSubscription(BitmexSubscribtions.Trade, "XBTUSD"));
            //socket.Subscribe(new BitmexSubscribeRequest()
            //    .AddSubscription(BitmexSubscribtions.OrderBookL2_25, "XBTUSD"));
            //      var client = new BitmexClient(new BitmexClientOptions(configuration["key"], configuration["secret"], bool.Parse(configuration["testnet"])));

            //var up = new UpdateOrderRequest() { OrigClOrdId = "cc75b825-acfb-447b-b497-2a41c65b30b5", OrderQty=101};

            //var dic = up.AsDictionary();
            //var update = await client.UpdateOrderAsync(up);

            //            Console.WriteLine(update.Data.Id);
            // var dsf = await client.CancelOrderAsync(new CancelOrderRequest() { ClientOrderId = o.Data.ClOrdID });
            //  var posit = client.GetExecutions(new BitmexRequestWithFilter().WithSymbolFilter("XBTUSD").WithOldestFirst().WithResultsCount(19));
            //string id = posit.Data.FirstOrDefault().OrderID;
            //   var order = client.GetOrders(new BitmexRequestWithFilter().WithSymbolFilter("XBTUSD").WithNewestFirst().WithResultsCount(1));
            //var ttt = order.Data.FirstOrDefault(c => c.Id == "c450d977-500d-1faa-2f1f-527a589c99ab");
            //var activeOrders = await client.GetOrdersAsync(new Client.Objects.Requests.BitmexRequestWithFilter().WithSymbolFilter("XBTUSD").WithOnlyActiveOrders());

            //var story = await client.GetStatsHistoryUsdAsync();

            Console.ReadLine();
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
            Console.WriteLine($"{DateTime.UtcNow}:ob");
            // throw new Exception();
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

        private static void Ob_OnBestOffersChanged(CryptoExchange.Net.Interfaces.ISymbolOrderBookEntry arg1, CryptoExchange.Net.Interfaces.ISymbolOrderBookEntry arg2)
        {
            Console.WriteLine($"{DateTime.UtcNow}: {arg1.Price}:{arg2.Price}");
        }
    }
}
