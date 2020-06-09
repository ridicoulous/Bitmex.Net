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
using System.Diagnostics;
using System.Reactive.Linq;
using CryptoExchange.Net.Interfaces;


namespace Bitmex.Net.ClientExample
{
    class Quote
    {
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
    }
    class Program
    {
        static BitmexSymbolOrderBook ob;       
        static Stopwatch sw = new Stopwatch();
        static EventHandler<Quote> OnQuote;
        static async Task MoveOrdersToBestBidOrAsk(Quote quote, CancellationToken token=default)
        {
            //await Task.Delay(100, token);
            if (token.IsCancellationRequested)
            {
                Console.WriteLine($"Token was canceled");
                return;
            }           
            Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss.fff}]: {quote.Bid}:{quote.Ask} [{sw.ElapsedMilliseconds}]");
            sw.Restart();
        }
        static async Task Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            var builder = new ConfigurationBuilder()
           .AddJsonFile("appconfig.json", optional: true, reloadOnChange: true);


            var configuration = builder.Build();
            var c = new BitmexClient(new BitmexClientOptions(true) 
            {
                ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials(configuration["testnet:key"], configuration["testnet:secret"]),              
            });
            decimal init = 12;
            var o = await c.PlaceOrderAsync(new PlaceOrderRequest() { BitmexOrderType = BitmexOrderType.Limit, Price=42000,Side=BitmexOrderSide.Sell,Quantity=12, Symbol="XBTUSD" } );
            await Task.Delay(1000);
            var o2 = await c.PlaceOrderAsync(new PlaceOrderRequest() { BitmexOrderType = BitmexOrderType.Limit, Price=43000,Side=BitmexOrderSide.Sell,Quantity=12, Symbol="XBTUSD" } );

            var cancel = await c.CancelOrderAsync(new CancelOrderRequest(new string[] { o.Data.Id, o2.Data.Id }));

            var b = c.GetUserWalletHistory(count: 5);
            // var res = b.Data.Where(c => c.TransactType == "Total").Sum(c => c.Amount) - b.Data.Where(c => c.TransactType == "RealisedPNL").Sum(c => c.Amount);
            //var socket = new BitmexSocketClient(new BitmexSocketClientOptions(false) { LogVerbosity = CryptoExchange.Net.Logging.LogVerbosity.Debug });

            var socket = new BitmexSocketClient(new BitmexSocketClientOptions(configuration["testnet:key"], configuration["testnet:secret"], isTestnet: true)
            {
                LogVerbosity = CryptoExchange.Net.Logging.LogVerbosity.Debug,
                SocketNoDataTimeout = TimeSpan.FromSeconds(45)
            });

            socket.OnUserWalletUpdate += Socket_OnUserWalletUpdate;

            //socket.OnUserExecutionsUpdate += OnExecution;
             socket.OnUserPositionsUpdate += BitmexSocketClient_OnUserPositionsUpdate;
            socket.OnOrderBookL2_25Update += Socket_OnOrderBookL2_25Update;
            //socket.OnTradeUpdate += Socket_OnTradeUpdate;
            //  socket.OnSocketClose += Socket_OnSocketClose;
            // socket.OnSocketException += Socket_OnSocketException;
            //socket.OnChatMessageUpdate += Socket_OnChatMessageUpdate;
             socket.Subscribe(new BitmexSubscribeRequest()
                .AddSubscription(BitmexSubscribtions.Order, "XBTUSD")
                .AddSubscription(BitmexSubscribtions.Execution, "XBTUSD")
               .AddSubscription(BitmexSubscribtions.Wallet));
              //  .AddSubscription(BitmexSubscribtions.Trade, "XBTUSD")
                //.AddSubscription(BitmexSubscribtions.Chat)
            //    .AddSubscription(BitmexSubscribtions.OrderBookL2_25, "XBTUSD"));
            //socket.Subscribe(new BitmexSubscribeRequest()
            //   .AddSubscription(BitmexSubscribtions.Trade, "XBTUSD"));
            socket.Subscribe(new BitmexSubscribeRequest()
                .AddSubscription(BitmexSubscribtions.OrderBookL2_25, "XBTUSD"));
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
            Console.WriteLine($"                                         +=> {arg1.Price}:{arg2.Price} [{DateTime.UtcNow:HH:mm:ss.fff}]:");
         
            OnQuote?.Invoke(ob, new Quote() { Ask = arg2.Price, Bid = arg1.Price });
        }
    }
}
