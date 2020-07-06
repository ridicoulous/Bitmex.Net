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

namespace Bitmex.Net.ClientExample
{
    class Program
    {       
        static async Task Main(string[] args)
        {
            BitmexHistoricalTradesLoader bitmexHistoricalTradesLoader = new BitmexHistoricalTradesLoader();
            var data = await bitmexHistoricalTradesLoader.GetDailyTradesAsync(new DateTime(2020, 5, 20));
            var data2 = await bitmexHistoricalTradesLoader.GetTradesByPeriodAsync(new DateTime(2020, 5, 20), new DateTime(2020, 5, 21));

            var quotes = await bitmexHistoricalTradesLoader.GetDailyQuotesAsync(new DateTime(2020, 6, 20));
            
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            var builder = new ConfigurationBuilder()
           .AddJsonFile("appconfig.json", optional: true, reloadOnChange: true);


            var configuration = builder.Build();
            var c = new BitmexClient(new BitmexClientOptions(true) 
            {
                ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials(configuration["testnet:key"], configuration["testnet:secret"]),              
            });
            
            var o = await c.PlaceOrderAsync(new PlaceOrderRequest() { BitmexOrderType = BitmexOrderType.Limit, Price=42000,Side=BitmexOrderSide.Sell,Quantity=12, Symbol="XBTUSD" } );
            await Task.Delay(1000);
            var o2 = await c.PlaceOrderAsync(new PlaceOrderRequest() { BitmexOrderType = BitmexOrderType.Limit, Price=43000,Side=BitmexOrderSide.Sell,Quantity=12, Symbol="XBTUSD" } );

            var cancel = await c.CancelOrderAsync(new CancelOrderRequest(new string[] { o.Data.Id, o2.Data.Id }));


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
