using Bitmex.Net.Client.Objects.Socket.Repsonses;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bitmex.Net.Client.Helpers.Extensions;
using Bitmex.Net.Client.Objects;
using System.Linq;
using Bitmex.Net.Client.Objects.Requests;
using Bitmex.Net.Client.Objects.Socket;

namespace Bitmex.Net.Client
{
    public class BitmexSymbolOrderBook : SymbolOrderBook
    {
        private readonly BitmexSocketClient _bitmexSocketClient;
        private readonly int InstrumentIndex;
        private readonly decimal InstrumentTickSize;
        private bool IsInititalBookSetted;
        private bool isTestnet;
        public BitmexSymbolOrderBook(string symbol, BitmexSocketOrderBookOptions options, BitmexSocketClient? bitmexSocketClient = null) : base(symbol, options)
        {
            isTestnet = options.IsTestnet;
            _bitmexSocketClient = bitmexSocketClient ?? new BitmexSocketClient(new BitmexSocketClientOptions(options.IsTestnet));
            InstrumentIndex = options.InstrumentIndex ?? _bitmexSocketClient.InstrumentsIndexesAndTicks[symbol].Index;
            InstrumentTickSize = options.TickSize.HasValue ? options.TickSize.Value : _bitmexSocketClient.InstrumentsIndexesAndTicks[symbol].TickSize;
        }
        public void Ping()
        {
            _bitmexSocketClient.Ping();
        }
        public override void Dispose()
        {
            processBuffer.Clear();
            asks.Clear();
            bids.Clear();
            _bitmexSocketClient.Dispose();
        }

        protected override async Task<CallResult<bool>> DoResync()
        {
            return await WaitForSetOrderBook(10000).ConfigureAwait(false);
        }

        protected override async Task<CallResult<UpdateSubscription>> DoStart()
        {
            var subscriptionResult = await _bitmexSocketClient.SubscribeToOrderBookUpdatesAsync(OnUpdate, Symbol).ConfigureAwait(false);
            if (!subscriptionResult)
            {
                return subscriptionResult;
            }
            var setResult = await WaitForSetOrderBook(10000).ConfigureAwait(false);
            return setResult ? subscriptionResult : new CallResult<UpdateSubscription>(null, setResult.Error);
        }
        public DateTime LastOrderBookMessage;
        public DateTime LastAction;
        private void OnUpdate(BitmexSocketEvent<BitmexOrderBookEntry> update)
        {
            if (update.Data[0].Symbol != Symbol)
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
            SetInitialOrderBook(DateTime.UtcNow.Ticks, entries.Where(e => e.Side == OrderBookEntryType.Bid), entries.Where(e => e.Side == OrderBookEntryType.Ask));
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
                    UpdateOrderBook(DateTime.UtcNow.Ticks, entries.Where(e => e.Side == OrderBookEntryType.Bid), entries.Where(e => e.Side == OrderBookEntryType.Ask));
                    LastAction = DateTime.UtcNow;
                }
                else
                {
                    log.Write(CryptoExchange.Net.Logging.LogVerbosity.Error, $"Orderbook was not updated cause not initiated");
                    using (var client = new BitmexClient(new BitmexClientOptions(isTestnet)))
                    {
                        log.Write(CryptoExchange.Net.Logging.LogVerbosity.Debug, $"Setting orderdbook through api");

                        var ob = client.GetOrderBook(Symbol);
                        if (ob)
                        {
                            SetInitialOrderBook(DateTime.UtcNow.Ticks, ob.Data.Where(x => x.Side == OrderBookEntryType.Bid), ob.Data.Where(x => x.Side == OrderBookEntryType.Ask));
                            IsInititalBookSetted = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Write(CryptoExchange.Net.Logging.LogVerbosity.Error, $"Orderbook was not updated {ex.ToString()}");
            }
        }
    }
}
