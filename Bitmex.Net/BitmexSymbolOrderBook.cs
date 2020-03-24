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
        public BitmexSymbolOrderBook(string symbol, BitmexSocketOrderBookOptions options) : base(symbol, options)
        {
            _bitmexSocketClient = new BitmexSocketClient(new BitmexSocketClientOptions(options.IsTestnet));
            InstrumentIndex =  _bitmexSocketClient.InstrumentsIndexesAndTicks[symbol].Index;
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

        private void OnUpdate(BitmexSocketEvent<BitmexOrderBookEntry> update)
        {
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
            if (IsInititalBookSetted)
            {
                if(entries==null||!entries.Any())
                {
                    return;
                }
                foreach (var e in entries)
                {
                    e.SetPrice(InstrumentIndex, InstrumentTickSize);
                    // log.Write(CryptoExchange.Net.Logging.LogVerbosity.Warning, $"Price for orderboook level {e.Id} was not setted");
                }

                UpdateOrderBook(DateTime.UtcNow.Ticks, entries.Where(e => e.Side == OrderBookEntryType.Bid), entries.Where(e => e.Side == OrderBookEntryType.Ask));
            }
        }
    }
}
