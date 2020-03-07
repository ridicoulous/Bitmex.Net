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

namespace Bitmex.Net.ClientExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            var builder = new ConfigurationBuilder()
           .AddJsonFile("appconfig.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();
            var ddd = configuration["key"];
            var client = new BitmexClient(new BitmexClientOptions(configuration["key"], configuration["secret"], bool.Parse(configuration["testnet"])));

            var ns = client.PlaceOrder(new Client.Objects.Requests.PlaceOrderRequest("XBTUSD")
            {
                Side = Client.Objects.BitmexOrderSide.Buy,
                Quantity = 1,
                BitmexOrderType = Client.Objects.BitmexOrderType.Limit,
                Price = 9090
            });
            //var t = configuration["key"];
            //var socket = new BitmexSocketClient(new BitmexSocketClientOptions(configuration["key"], configuration["secret"], bool.Parse(configuration["testnet"])));
            //var socket = new BitmexSocketClient();
            //socket.OnQuotesUpdate += Socket_OnQuotesUpdate;
            //socket.OnTradeUpdate += Socket_OnTradeUpdate;
            //socket.OnOrderBookL2_25Update += Socket_OnOrderBookL2_25Update;
            //var res = SocketSubscribeRequestBuilder.CreateEmptySubscribeRequest()
            //    .Subscribe(BitmexSubscribtions.OrderBookL2_25);
            Console.WriteLine();
            var positions2 = client.GetPositions();

            //  await socket.SubscribeAsync(res);

            // await socket.SubscribeAsync(new BitmexSubscribeRequest().Subscribe(BitmexSubscribtions.Trade));
            //socket.SubscribeToUserExecutions(Exec, "XBTUSD");
            //socket.SubscribeToUserOrderUpdates(Exec, "XBTUSD");
            Console.ReadLine();
        }

        private static void Socket_OnOrderBookL2_25Update(BitmexSocketEvent<Client.Objects.BitmexOrderBookEntry> obj)
        {
            Console.WriteLine($"[OrderBook:]{obj.Data[0].Symbol} : {obj.Data[0].Price}");
        }

        private static void Socket_OnTradeUpdate(BitmexSocketEvent<Client.Objects.Trade> obj)
        {
            Console.WriteLine($"[TRADE:]{obj.Data[0].Timestamp} {obj.Data[0].Symbol}");
        }

        private static void Socket_OnQuotesUpdate(BitmexSocketEvent<Client.Objects.Quote> obj)
        {
            Console.WriteLine($"[QUOTE:]{obj.Data[0].Timestamp} {obj.Data[0].Symbol}");
        }

        private static void Ob_OnBestOffersChanged(CryptoExchange.Net.Interfaces.ISymbolOrderBookEntry arg1, CryptoExchange.Net.Interfaces.ISymbolOrderBookEntry arg2)
        {
            Console.WriteLine($"{arg1.Price}  : {arg2.Price}");
        }

    }
}
