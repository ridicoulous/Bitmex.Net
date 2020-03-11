using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitmex.Net.Client
{
    public class BitmexSocketOrderBookOptions : OrderBookOptions
    {
        /// <summary>
        /// "orderBookL2_25" -  Top 25 levels of level 2 order book;
        /// "orderBookL2" - Full level 2 order book; 
        /// </summary>
        public readonly bool IsFull;

        public readonly bool IsTestnet;

        /// <summary>
        /// Use it carefully. 
        /// </summary>
        public readonly decimal? TickSize;

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
        public BitmexSocketOrderBookOptions(string name, bool isTest = false, decimal? tickSize = null) : base(name, false)
        {
            IsTestnet = isTest;          
            TickSize = tickSize;
        }
    }
}
