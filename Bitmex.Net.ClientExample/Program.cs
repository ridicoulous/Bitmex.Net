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
            //ob = new BitmexSymbolOrderBook("XBTUSD", new BitmexSocketOrderBookOptions("XBT"));
            //ob.OnBestOffersChanged += Ob_OnBestOffersChanged;
            //await ob.StartAsync();
            var configuration = builder.Build();
            var socket = new BitmexSocketClient(new BitmexSocketClientOptions(configuration["key"], configuration["secret"], bool.Parse(configuration["testnet"])));
            socket.OnUserOrdersUpdate += OnBitmexOrderUpdate;
            socket.OnUserExecutionsUpdate += OnExecution;
            socket.OnUserPositionsUpdate += BitmexSocketClient_OnUserPositionsUpdate;
            socket.Subscribe(new BitmexSubscribeRequest()
                .Subscribe(BitmexSubscribtions.Order,"XBTUSD")
                .Subscribe(BitmexSubscribtions.Execution, "XBTUSD")
                .Subscribe(BitmexSubscribtions.Position, "XBTUSD"));
            //var client = new BitmexClient(new BitmexClientOptions(configuration["key"], configuration["secret"], bool.Parse(configuration["testnet"])));

            //var activeOrders = await client.GetOrdersAsync(new Client.Objects.Requests.BitmexRequestWithFilter().WithSymbolFilter("XBTUSD").WithOnlyActiveOrders());

            //var story = await client.GetStatsHistoryUsdAsync();

            Console.ReadLine();
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
