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

namespace Bitmex.Net.Client
{
    public class BitmexSymbolOrderBook : SymbolOrderBook
    {
        private readonly BitmexClient _bitmexClient;
        private readonly BitmexSocketClient _bitmexSocketClient;
        private readonly int InstrumentIndex;
        private readonly decimal InstrumentTickSize;
        private readonly string OrderBookResultType;
        private bool IsInititalBookSetted;
        private bool IsFull;
        public BitmexSymbolOrderBook(string symbol, BitmexSocketOrderBookOptions options) : base(symbol, options)
        {
            IsFull = options.IsFull;

            _bitmexSocketClient = new BitmexSocketClient();
            _bitmexClient = new BitmexClient();
            var allInstruments = _bitmexClient.GetInstruments(new BitmexRequestWithFilter()
                .AddColumnsToGetInRequest(new string[] { "tickSize", "symbol" })
                .WithResultsCount(500)
                .WithStartingFrom(0));
            if (allInstruments)
            {
                allInstruments.Data.Reverse();
                InstrumentIndex = allInstruments.Data.FindIndex(c => c.Symbol == symbol);
                if (InstrumentIndex <= 0)
                {
                    throw new ArgumentException($"Can not find {symbol} index in instruments");
                }
                else
                {
                    InstrumentTickSize = options.TickSize ?? allInstruments.Data[InstrumentIndex].TickSize ?? 0.01m;
                }
            }
            else
            {
                throw new ArgumentException($"Can not load instruments from Bitmex: {allInstruments.Error.Message}");
            }
        }

        public override void Dispose()
        {
            processBuffer.Clear();
            asks.Clear();
            bids.Clear();
            _bitmexClient.Dispose();
            _bitmexSocketClient.Dispose();
        }

        protected override async Task<CallResult<bool>> DoResync()
        {
            return await WaitForSetOrderBook(10000).ConfigureAwait(false);
        }

        protected override async Task<CallResult<UpdateSubscription>> DoStart()
        {
            var subscriptionResult = await _bitmexSocketClient.SubscribeToOrderBookUpdatesAsync(OnUpdate, Symbol, IsFull?"orderBookL2":"orderBookL2_25").ConfigureAwait(false);
            if (!subscriptionResult)
            {
                return subscriptionResult;
            }
            var setResult = await WaitForSetOrderBook(10000).ConfigureAwait(false);
            return setResult ? subscriptionResult : new CallResult<UpdateSubscription>(null, setResult.Error);
        }

        private void OnUpdate(BitmexOrderBookUpdateEvent update)
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
                foreach (var e in entries)
                {
                    e.SetPrice(InstrumentIndex, InstrumentTickSize);
                }
                UpdateOrderBook(DateTime.UtcNow.Ticks, entries.Where(e => e.Side == OrderBookEntryType.Bid), entries.Where(e => e.Side == OrderBookEntryType.Ask));
            }
        }
    }
}
