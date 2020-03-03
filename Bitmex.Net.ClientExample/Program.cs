using Bitmex.Net.Client;
using Bitmex.Net.Client.Objects.Socket.Repsonses;
using System;
using System.Threading.Tasks;
using Bitmex.Net.Client.Helpers.Extensions;
namespace Bitmex.Net.ClientExample
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var socket = new BitmexSocketClient(new BitmexSocketClientOptions("W4kfhlp9OhwtHquxV0Zsd5WM", "TDvu6R2hfp3Si8gUrsiNuqYZ_llKW5p2CGFUyuJfOLEjM0it",true));
            socket.SubscribeToUserExecutions(Exec, "XBTUSD");
            Console.ReadLine();
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
