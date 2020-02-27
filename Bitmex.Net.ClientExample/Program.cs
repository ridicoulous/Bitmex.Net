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
           
            var ob = new BitmexSymbolOrderBook("XBTUSD", new BitmexSocketOrderBookOptions("BitmexXBTUSD",tickSize:0.01m));
            ob.OnBestOffersChanged += Ob_OnBestOffersChanged;
            ob.Start();           
            Console.ReadLine();
        }

        private static void Ob_OnBestOffersChanged(CryptoExchange.Net.Interfaces.ISymbolOrderBookEntry arg1, CryptoExchange.Net.Interfaces.ISymbolOrderBookEntry arg2)
        {
            Console.WriteLine($"{arg1.Price}  : {arg2.Price}");
        }
       
    }
}
