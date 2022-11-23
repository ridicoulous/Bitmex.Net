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
        public readonly bool IsTestnet;
        /// <summary>
        /// This value is used for price calculation by orderbook entry id. Set  it carefully
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
        /// and you can use `instrument.tickSize` directly. For example, XBTUSD has 88 index and tick size returned by api =0.5, but to calculate price at orderbookL2 update you should use 0.01. this value is hardcoded
        /// </param>
        /// <param name="instrumentIndex">Used for price calculation. <see href="https://www.bitmex.com/app/restAPI#OrderBookL2">Bitmex docs</see></param>
        public BitmexSocketOrderBookOptions(string name, bool isTest = false,  int? instrumentIndex=null, decimal? tickSize = null) : base()
        {
            IsTestnet = isTest;          
            TickSize = tickSize;
            InstrumentIndex = instrumentIndex;    
        }
    }
}
