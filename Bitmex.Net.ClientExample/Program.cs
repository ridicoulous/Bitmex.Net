using Bitmex.Net.Client;
using Bitmex.Net.Client.Objects.Socket.Repsonses;
using System;
using System.Threading.Tasks;
using Bitmex.Net.Client.Helpers.Extensions;
using Microsoft.Extensions.Configuration;

namespace Bitmex.Net.ClientExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
           .AddJsonFile("appconfig.json", optional: true, reloadOnChange: true);

           var configuration = builder.Build();
            var t = configuration["key"];
            var socket = new BitmexSocketClient(new BitmexSocketClientOptions(configuration["key"], configuration["secret"], bool.Parse(configuration["testnet"])));
            socket.SubscribeToUserExecutions(Exec, "XBTUSD");
            socket.SubscribeToUserOrderUpdates(Exec, "XBTUSD");
            Console.ReadLine();
        }

        private static void Exec(BitmexOrderUpdateEvent obj)
        {
          
        }

        private static void Exec(BitmexExecutionEvent obj)
        {
           
        }

        private static void Ob_OnBestOffersChanged(CryptoExchange.Net.Interfaces.ISymbolOrderBookEntry arg1, CryptoExchange.Net.Interfaces.ISymbolOrderBookEntry arg2)
        {
            Console.WriteLine($"{arg1.Price}  : {arg2.Price}");
        }
       
    }
}
