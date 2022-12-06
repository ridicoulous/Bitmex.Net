﻿using Bitmex.Net.Client;
using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Bitmex.Net.Client.Objects.Socket.Requests;
using Bitmex.Net.Client.Objects.Socket;

using Bitmex.Net.Client.Objects;

using Bitmex.Net.Client.HistoricalData;
using System.Collections.Generic;
using CryptoExchange.Net.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Polly.Extensions.Http;
using Polly;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using CryptoExchange.Net.Sockets;
using Newtonsoft.Json;
using CryptoExchange.Net.Authentication;
using System.Threading;
using CryptoExchange.Net.CommonObjects;
using Bitmex.Net.Client.Objects.Requests;

namespace Bitmex.Net.ClientExample
{

    class Program
    {
        static void onData(DataEvent<BitmexSocketEvent<BitmexOrderBookEntry>> data)
        {
            Console.WriteLine(JsonConvert.SerializeObject(data));
        }
        static async Task Main(string[] args)
        {
            // using CryptoExchange.Net.OrderBook.SymbolOrderBook, it subscribes to socket orderbook under the hood
            var socketBook = new BitmexSymbolOrderBook("XBTUSD", new BitmexSocketOrderBookOptions("bitmex"){LogLevel = LogLevel.Trace});
            socketBook.OnBestOffersChanged += S_OnBestOffersChanged;
            await socketBook.StartAsync();
            Console.ReadLine();

            // using appconfig.json instead of hardcoding the key/secret values
            var builder = new ConfigurationBuilder()
           .AddJsonFile("appconfig.json", optional: true, reloadOnChange: false);

            var configuration = builder.Build();


            var client = new BitmexClient(new BitmexClientOptions(configuration["prod:key"], configuration["prod:secret"])).MarginClient;
            // get last 100 public trades for XBTUSD
            var pubTrades = await client.GetTradesAsync(new BitmexRequestWithFilter(){Symbol = "XBTUSD"});
            // get last 100 your wallet changes
            var walletHist = await client.GetUserWalletHistoryAsync();
            foreach (var r in walletHist.Data)
            {
                Console.WriteLine($"[{r.Timestamp}]: {r.TransactStatus} - {r.WalletBalanceInBtc} BTC ({r.WalletBalance}) satoshi");
            }
            // get wallet with all currencies
            var wallet = await client.GetUserWalletAllCurrenciesAsync();
            // place order via CryptoExchange.Net.Interfaces.CommonClients.IFuturesClient interface implementation
            var ordId = client.PlaceOrderAsync("XBTUSD", CommonOrderSide.Buy, CommonOrderType.Limit, 100, 10000);
            // place order via IBitmexCommonTradeClient interface implementation
            var ord = client.PlaceOrderAsync(new PlaceOrderRequest("XBTUSD")
                { 
                    BitmexOrderType = BitmexOrderType.Limit,
                    Side = BitmexOrderSide.Buy,
                    Price = 10000,
                    Quantity = 100,
                    ClientOrderId = "your_own_uniq_id"    //optional parameter, should be less or equal to 36 chars
                }
            );
            Console.ReadLine();

            // subscribe to testnet.bitmex.com's Order, 1h klines using appconfig.json credentials
            using (var socket = new BitmexSocketClient(new BitmexSocketClientOptions(configuration["testnet:key"], configuration["testnet:secret"], isTestnet: true)
            {
                LogLevel = LogLevel.Debug,
            }))
            {
                var stream = socket.SocketStreams;
                // each subscription method has corresponding BitmexSocketStream event triggered on updates
                // e.g. Order => OnUserOrdersUpdate, TradeBin1h => OnOneHourTradeBinUpdate, Trade => OnTradeUpdate and so on
                stream.OnUserOrdersUpdate += Socket_OnUserOrdersUpdate; //add action that should be do on orders updates
                stream.OnOneHourTradeBinUpdate += (data) => {         //add action that should be do on new candle comes
                    var whichCandle = data.Action == BitmexAction.Partial ? "snapshot" : "new";
                    foreach(var candle in data.Data)
                    Console.WriteLine($"{whichCandle} candle come: open {candle.Open}, high {candle.High}, low {candle.Low}, close: {candle.Close}");
                };
                var res = await stream.SubscribeAsync(new BitmexSubscribeRequest()
                 .AddSubscription(BitmexSubscribtions.Order, "XBTUSD")  // subscribe to orders updates
                 .AddSubscription(BitmexSubscribtions.TradeBin1h, "XBTUSD") // subscribe to candle updates
                );
                Console.ReadLine();
                // you can unsubscribe from any subscriptions
                await socket.UnsubscribeAsync(res.Data);
                Console.ReadLine();
                // or from all ones
                await socket.UnsubscribeAllAsync();
                Console.ReadLine();

            }

            Console.ReadLine();

            // await TestHistoricalDataLoading();

        }

        private static void S_OnTradeUpdate(BitmexSocketEvent<BitmexTrade> obj)
        {
            Console.WriteLine($"trade came: {JsonConvert.SerializeObject(obj)}");
        }

        private static void S_OnBestOffersChanged((CryptoExchange.Net.Interfaces.ISymbolOrderBookEntry BestBid, CryptoExchange.Net.Interfaces.ISymbolOrderBookEntry BestAsk) obj)
        {
            Console.WriteLine($"S_OnBestOffersChanged:{obj.BestAsk.Price}:{obj.BestBid.Price}");
        }

     

        private static void Socket_OnUserOrdersUpdate(BitmexSocketEvent<BitmexOrder> obj)
        {
            foreach (var o in obj.Data)
            {
                Console.WriteLine($"{o.Symbol}:{o.Side}:{o.Id}");
            }
        }

        private static async Task TestHistoricalDataLoading()
        {
            BitmexHistoricalTradesLoader bitmexHistoricalTradesLoader = new BitmexHistoricalTradesLoader();
            var data = await bitmexHistoricalTradesLoader.GetDailyTradesAsync(new DateTime(2022, 11, 24));
            Console.WriteLine($"Loaded {data.Count} trades");
            //var quotes = await bitmexHistoricalTradesLoader.GetDailyQuotesAsync(new DateTime(2020, 6, 20));
        }


    }
}
