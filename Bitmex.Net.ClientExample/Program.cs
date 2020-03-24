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
        static async Task Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            var builder = new ConfigurationBuilder()
           .AddJsonFile("appconfig.json", optional: true, reloadOnChange: true);
            ob = new BitmexSymbolOrderBook("XBTUSD", new BitmexSocketOrderBookOptions("XBT"));
            ob.OnBestOffersChanged += Ob_OnBestOffersChanged;


            await ob.StartAsync();
            //while(true)
            //{
            //    ob.Ping();
            //    await Task.Delay(5000);
            //}
            var configuration = builder.Build();



            var socket = new BitmexSocketClient(new BitmexSocketClientOptions(configuration["key"], configuration["secret"], bool.Parse(configuration["testnet"])));

            socket.OnUserOrdersUpdate += OnBitmexOrderUpdate;
            socket.OnUserExecutionsUpdate += OnExecution;
            socket.OnUserPositionsUpdate += BitmexSocketClient_OnUserPositionsUpdate;
            socket.OnOrderBookL2_25Update += Socket_OnOrderBookL2_25Update;
            socket.OnTradeUpdate += Socket_OnTradeUpdate;
            socket.Subscribe(new BitmexSubscribeRequest()
                .AddSubscription(BitmexSubscribtions.Order, "XBTUSD")
                .AddSubscription(BitmexSubscribtions.Execution, "XBTUSD")
                .AddSubscription(BitmexSubscribtions.Position, "XBTUSD")
                .AddSubscription(BitmexSubscribtions.Trade, "XBTUSD")
                .AddSubscription(BitmexSubscribtions.OrderBookL2_25, "XBTUSD"));
            // var client = new BitmexClient(new BitmexClientOptions(configuration["key"], configuration["secret"], bool.Parse(configuration["testnet"])));
            //  var posit = client.GetExecutions(new BitmexRequestWithFilter().WithSymbolFilter("XBTUSD").WithOldestFirst().WithResultsCount(19));
            //string id = posit.Data.FirstOrDefault().OrderID;
            //var order = client.GetOrders(new BitmexRequestWithFilter().WithSymbolFilter("XBTUSD").WithOldestFirst().WithResultsCount(1));
            //var ttt = order.Data.FirstOrDefault(c => c.Id == "c450d977-500d-1faa-2f1f-527a589c99ab");
            //var activeOrders = await client.GetOrdersAsync(new Client.Objects.Requests.BitmexRequestWithFilter().WithSymbolFilter("XBTUSD").WithOnlyActiveOrders());

            //var story = await client.GetStatsHistoryUsdAsync();

            Console.ReadLine();
        }

        private static void Socket_OnTradeUpdate(BitmexSocketEvent<Trade> obj)
        {
            Console.WriteLine("trade");
        }

        private static void Socket_OnOrderBookL2_25Update(BitmexSocketEvent<BitmexOrderBookEntry> obj)
        {
            Console.WriteLine("ob");
        }

        private static void BitmexSocketClient_OnUserPositionsUpdate(BitmexSocketEvent<Position> obj)
        {
            Console.WriteLine($"Catched POSITION {JsonConvert.SerializeObject(obj.AsDictionary())}");
            Console.WriteLine();
        }

        private static void OnExecution(BitmexSocketEvent<Execution> obj)
        {
            Console.WriteLine($"Catched EXEC {JsonConvert.SerializeObject(obj.AsDictionary())}");
            Console.WriteLine();
        }

        private static void OnBitmexOrderUpdate(BitmexSocketEvent<Order> obj)
        {
            Console.WriteLine($"Catched ORDER {JsonConvert.SerializeObject(obj.AsDictionary())}");
            Console.WriteLine();
        }

        private static void Ob_OnBestOffersChanged(CryptoExchange.Net.Interfaces.ISymbolOrderBookEntry arg1, CryptoExchange.Net.Interfaces.ISymbolOrderBookEntry arg2)
        {
            Console.WriteLine($"{DateTime.UtcNow}: {arg1.Price}:{arg2.Price}");
        }
    }
}
