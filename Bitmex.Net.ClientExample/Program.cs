using Bitmex.Net.Client;
using Bitmex.Net.Client.Objects.Socket.Repsonses;
using System;
using System.Threading.Tasks;

namespace Bitmex.Net.ClientExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var socket = new BitmexSocketClient(new BitmexSocketClientOptions(true) { ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("W4kfhlp9OhwtHquxV0Zsd5WM", "TDvu6R2hfp3Si8gUrsiNuqYZ_llKW5p2CGFUyuJfOLEjM0it") });
            await Task.Delay(2000);
            await socket.SubscribeToTradesAsync(OnData, "XBTUSD");
            Console.ReadLine();
        }

        private static void OnData(BitmexTradeEvent obj)
        {
            Console.WriteLine(obj.Data[0].Size);    
        }
    }
}
