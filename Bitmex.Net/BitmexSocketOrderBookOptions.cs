using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitmex.Net.Client
{
    /// <summary>
    /// 
    /// </summary>
    public class BitmexSocketOrderBookOptions : OrderBookOptions
    {
        /// <summary>
        /// "orderBookL2_25" -  Top 25 levels of level 2 order book;
        /// "orderBookL2" - Full level 2 order book; 
        /// </summary>
        public readonly bool IsFull;

        public readonly bool IsTestnet;
        /// <summary>
        /// This value is used for price calculation by orderbook entry id. Set  it carefull
        /// </summary>
        public readonly decimal? TickSize;
        /// <summary>
        /// This value is used for price calculation by orderbook entry id. Set it carefully. 
        /// </summary>
        public readonly int? InstrumentIndex;

        /// <summary>
        /// 
        /// </summary>
        ///
        /// <param name="isFull">
        /// if false, take first 25 levels
        /// "if true, will return the full level 2 order book;
        /// </param>
        /// <param name="tickSize">
        /// Used for price calculation. <see href="https://www.bitmex.com/app/restAPI#OrderBookL2">Bitmex docs</see>
        /// WARNING: This is a compatibility change as the tick sizes of live instruments changed in-flight. If you are listing
        /// these instruments, you must use their original tick as part of your calculations. If not, this can be ignored,
        /// and you can use `instrument.tickSize` directly. For example, XBTUSD has 88
        /// </param>
        /// <param name="instrumentIndex">Used for price calculation. <see href="https://www.bitmex.com/app/restAPI#OrderBookL2">Bitmex docs</see></param>
        public BitmexSocketOrderBookOptions(string name, bool isTest = false, bool isFull=false, int? instrumentIndex=null, decimal? tickSize = null) : base(name, false)
        {
            IsTestnet = isTest;          
            TickSize = tickSize;
            InstrumentIndex = instrumentIndex;
            IsFull = isFull;
        }
    }
}
