using CryptoExchange.Net.Objects;
using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bitmex.Net.Client.Objects;
using System.Linq;
using Bitmex.Net.Client.Objects.Socket;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace Bitmex.Net.Client
{
    public class BitmexSymbolOrderBook : SymbolOrderBook
    {
        private static BitmexSocketOrderBookOptions defaultOrderBookOptions = new BitmexSocketOrderBookOptions("BitmexOrderBook");
        private readonly BitmexSocketClient _bitmexSocketClient;
        private bool usedNewSocketClient;
        private readonly int InstrumentIndex;
        private readonly decimal InstrumentTickSize;
        private bool IsInititalBookSetted;
        private bool isTestnet;
        /// <summary>
        /// The last used id
        /// </summary>
        protected static long lastId;
        /// <summary>
        /// Lock for id generating
        /// </summary>
        protected static object idLock = new object();
        /// <summary>
        /// Last is used
        /// </summary>
        public static long LastId => lastId;

        /// <summary>
        /// Generate a unique id
        /// </summary>
        /// <returns></returns>
        protected long NextId()
        {
            lock (idLock)
            {
                lastId ++;
                return lastId;
            }
        }
        public BitmexSymbolOrderBook(string symbol, bool isTest = false) : this(symbol, defaultOrderBookOptions)
        {
            isTestnet = isTest;
            _bitmexSocketClient = new BitmexSocketClient(new BitmexSocketClientOptions(isTest));
            InstrumentIndex = _bitmexSocketClient.GetIndexAndTickForInstrument(symbol).Index;
            InstrumentTickSize = _bitmexSocketClient.GetIndexAndTickForInstrument(symbol).TickSize;
            if (symbol == "XBTUSD")
            {
                InstrumentTickSize = 0.01m;
            }
        }
        /// <summary>
        /// AttentioN! For price calculation at order book update you should use level id and and instrument index <see href=""/></see>
        /// example, XBTUSD has 88 index and tick size returned by api =0.5, but to calculate price at orderbookL2 update you should use 0.01. this value is hardcoded
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="options"></param>
        /// <param name="bitmexSocketClient"></param>
        public BitmexSymbolOrderBook(string symbol, BitmexSocketOrderBookOptions options, BitmexSocketClient bitmexSocketClient = null) : base("Bitmex", symbol, options)
        {
            isTestnet = options.IsTestnet;
            usedNewSocketClient = bitmexSocketClient is null;
            _bitmexSocketClient = bitmexSocketClient ?? new BitmexSocketClient(new BitmexSocketClientOptions(options.IsTestnet));
            InstrumentIndex = options.InstrumentIndex ?? _bitmexSocketClient.GetIndexAndTickForInstrument(symbol).Index;
            InstrumentTickSize = options.TickSize.HasValue ? options.TickSize.Value : _bitmexSocketClient.GetIndexAndTickForInstrument(symbol).TickSize;
            if (symbol == "XBTUSD")
            {
                InstrumentTickSize = 0.01m;
            }
        }
        protected override void Dispose(bool disposing)
        {
            // dispose client only created by this instance not shared socket client
            if (!usedNewSocketClient)
                _bitmexSocketClient.Dispose();
            base.Dispose(disposing);
        }

        protected override async Task<CallResult<bool>> DoResyncAsync(CancellationToken ct)
        {
            return await WaitForSetOrderBookAsync(TimeSpan.FromSeconds(10.0), ct).ConfigureAwait(false);
        }

        protected override async Task<CallResult<UpdateSubscription>> DoStartAsync(CancellationToken ct)
        {
            /*If you wish to get real-time order book data, we recommend you use the orderBookL2_25 subscription. orderBook10 pushes the top 10 levels on every tick,
             * but transmits much more data. orderBookL2 pushes the full L2 order book, but the payload can get very large. orderbookL2_25 provides a subset of the full L2 orderbook,
             * but is throttled. In the future, orderBook10 may be throttled, so use orderBookL2 in any latency-sensitive application. 
             * For those curious, the id on an orderBookL2_25 or orderBookL2 entry is a composite of price and symbol, and is always unique for any given price level.
             * It should be used to apply update and delete actions.
             
            Due to decrease delays, subscribe to full orderbook
             */
            var subscriptionResult = await _bitmexSocketClient.SubscribeToOrderBookUpdatesAsync(OnUpdate, Symbol, true).ConfigureAwait(false);
            if (!subscriptionResult)
            {
                return subscriptionResult;
            }
            var setResult = await WaitForSetOrderBookAsync(TimeSpan.FromSeconds(10.0), ct).ConfigureAwait(false);
            return setResult ? subscriptionResult : new CallResult<UpdateSubscription>(setResult.Error);
        }
        public DateTime LastOrderBookMessage;
        public DateTime LastAction;
        private void OnUpdate(DataEvent<BitmexSocketEvent<BitmexOrderBookEntry>> dataEvent)
        {
            var update = dataEvent.Data;
            if (update.Action == BitmexAction.Undefined || update.Data==null)
            {
                return;
            }
            if (!update.Data.Any(x => x.Symbol == Symbol))
            {
                return;
            }
            LastOrderBookMessage = DateTime.UtcNow;
            if (update.Action == Objects.Socket.BitmexAction.Partial)
            {
                Create(update.Data);
                return;
            }

            Update(update.Data);
        }

        /// <summary>
        /// You may receive other messages before the partial comes through. In that case, drop any messages received until you have received the partial.
        /// </summary>
        /// <param name="entries"></param>
        private void Create(List<BitmexOrderBookEntry> entries)
        {
            SetInitialOrderBook(NextId(), entries.Where(e => e.Side == OrderBookEntryType.Bid), entries.Where(e => e.Side == OrderBookEntryType.Ask));
            IsInititalBookSetted = true;
        }
        private void Update(List<BitmexOrderBookEntry> entries)
        {
            try
            {
                if (IsInititalBookSetted)
                {
                    if (entries == null || !entries.Any())
                    {
                        return;
                    }
                    foreach (var e in entries)
                    {                       
                        e.SetPrice(InstrumentIndex, InstrumentTickSize);
                    }
                    UpdateOrderBook(LastId, NextId(), entries.Where(e => e.Side == OrderBookEntryType.Bid), entries.Where(e => e.Side == OrderBookEntryType.Ask));
                    LastAction = DateTime.UtcNow;
                }
                else
                {
                    log.Write(LogLevel.Error, $"Orderbook was not updated cause not initiated");
                    using (var client = new BitmexClient(new BitmexClientOptions(isTestnet)))
                    {
                        log.Write(LogLevel.Debug, $"Setting orderdbook through api");

                        var ob = client.GetOrderBook(Symbol, 0);
                        if (ob)
                        {
                            SetInitialOrderBook(NextId(), ob.Data.Where(x => x.Side == OrderBookEntryType.Bid), ob.Data.Where(x => x.Side == OrderBookEntryType.Ask));
                            IsInititalBookSetted = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Write(LogLevel.Error, $"Orderbook was not updated {ex.ToString()}");
            }
        }
    }
}
