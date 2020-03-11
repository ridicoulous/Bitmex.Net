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

            var configuration = builder.Build();
            var client = new BitmexClient(new BitmexClientOptions(configuration["key"], configuration["secret"], bool.Parse(configuration["testnet"])));
            //var order = await client.PlaceOrderAsync(new Client.Objects.Requests.PlaceOrderRequest("XBTUSD") { BitmexOrderType = Client.Objects.BitmexOrderType.Limit, Quantity = -1, Price=25000 });
            var p = await client.GetOrdersAsync(new Client.Objects.Requests.BitmexRequestWithFilter().WithSymbolFilter("XBTUSD").WithOnlyActiveOrders());
            Console.WriteLine();
            //   ob = new BitmexSymbolOrderBook("XBTUSD", new BitmexSocketOrderBookOptions("sdsfgsdfg", true));
            // ob.OnBestOffersChanged+= Ob_OnOrderBookUpdate;
            // ob.OnOrderBookUpdate += Ob_OnOrderBookUpdate1;
            //var socket = new BitmexSocketClient(new BitmexSocketClientOptions(configuration["key"], configuration["secret"], bool.Parse(configuration["testnet"])));
            // var socket = new BitmexSocketClient();
            //socket.OnUserPositionsUpdate += Socket_OnUserPositionsUpdate;
            // socket.OnOrderBookL2_25Update += Socket_OnOrderBookL2_25Update1;
            // socket.OnQuotesUpdate += Socket_OnQuotesUpdate1;
            // await ob.StartAsync();

            //  socket.Subscribe(new BitmexSubscribeRequest()/*.Subscribe(BitmexSubscribtions.OrderBookL2_25, "XBTUSD")*/.Subscribe(BitmexSubscribtions.Quote,"XBTUSD"));
            //socket.OnQuotesUpdate += Socket_OnQuotesUpdate;
            //socket.OnTradeUpdate += Socket_OnTradeUpdate;
            //socket.OnOrderBookL2_25Update += Socket_OnOrderBookL2_25Update;
            //var res = SocketSubscribeRequestBuilder.CreateEmptySubscribeRequest()
            //    .Subscribe(BitmexSubscribtions.OrderBookL2_25);

            //  await socket.SubscribeAsync(res);

            // await socket.SubscribeAsync(new BitmexSubscribeRequest().Subscribe(BitmexSubscribtions.Trade));
            //socket.SubscribeToUserExecutions(Exec, "XBTUSD");
            //socket.SubscribeToUserOrderUpdates(Exec, "XBTUSD");
            Console.ReadLine();
        }

        private static void Ob_OnOrderBookUpdate1(System.Collections.Generic.IEnumerable<CryptoExchange.Net.Interfaces.ISymbolOrderBookEntry> arg1, System.Collections.Generic.IEnumerable<CryptoExchange.Net.Interfaces.ISymbolOrderBookEntry> arg2)
        {
            if (arg1 == null || arg2 == null)
            {
                return;
            }
            Console.WriteLine($"[{ob.BestAsk.Price}] - [{ob.BestBid.Price}]");
        }

        private static void Ob_OnOrderBookUpdate(CryptoExchange.Net.Interfaces.ISymbolOrderBookEntry arg1, CryptoExchange.Net.Interfaces.ISymbolOrderBookEntry arg2)
        {
            Console.WriteLine($"Б {ob.BestAsk.Price}$: {ob.BestAsk.Quantity} {ob.LastOrderBookUpdate.ToString("HH:mm:ss.fffff")}");
        }

        private static void Socket_OnQuotesUpdate1(BitmexSocketEvent<Client.Objects.Quote> obj)
        {
            Console.WriteLine($"К {obj.Data[0].AskPrice}$: {obj.Data[0].AskSize} {DateTime.Now.ToString("HH:mm:ss.fffff")}");
        }

        private static void Socket_OnOrderBookL2_25Update1(BitmexSocketEvent<Client.Objects.BitmexOrderBookEntry> obj)
        {
            if (obj.Action == BitmexAction.Insert || obj.Action == BitmexAction.Update && obj.Data.Any(c => c.Side == CryptoExchange.Net.Objects.OrderBookEntryType.Ask))
                Console.WriteLine($"OB: {String.Join(",", obj.Data.Select(c => c.Size))} {DateTime.Now.ToString("HH:mm:ss.fffff")}");
            //throw new NotImplementedException();
        }

        private static void Socket_OnUserPositionsUpdate(BitmexSocketEvent<Client.Objects.Position> obj)
        {
            Console.WriteLine(JsonConvert.SerializeObject(obj.AsDictionary()));
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
