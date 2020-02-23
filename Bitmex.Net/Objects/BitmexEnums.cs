using System;
using System.Collections.Generic;
using System.Text;

namespace Bitmex.Net.Objects
{
    public enum BitmexTimeInForce:byte
    {
        Day,
        GoodTillCancel,
        ImmediateOrCancel,
        FillOrKill
    }
    public enum BitmexOrderSide:byte
    {
        Buy,
        Sell
    }
    public enum BitmexOrderType:byte
    {
        /// <summary>
        /// The default order type. Specify an orderQty and price.
        /// </summary>
        Limit,
        /// <summary>
        /// A traditional Market order. A Market order will execute until filled or your bankruptcy price is reached, at which point it will cancel.
        /// </summary>
        Market,
        /// <summary>
        /// A Stop Market order. Specify an orderQty and stopPx. When the stopPx is reached, the order will be entered into the book.
        ///On sell orders, the order will trigger if the triggering price is lower than the stopPx. On buys, higher.
        ///Note: Stop orders do not consume margin until triggered. Be sure that the required margin is available in your account so that it may trigger fully.
        ///Close Stops don't require an orderQty. See Execution Instructions below.
        /// </summary>
        Stop,
        /// <summary>
        /// Like a Stop Market, but enters a Limit order instead of a Market order. Specify an orderQty, stopPx, and price.
        /// </summary>
        StopLimit,
        /// <summary>
        /// Similar to a Stop, but triggers are done in the opposite direction. Useful for Take Profit orders.
        /// </summary>
        MarketIfTouched,
        /// <summary>
        ///  as above MarketIfTouched. use for Take Profit Limit orders.
        /// </summary>
        LimitIfTouched
    }
    public enum BitmexExecutionIntructions:byte
    {
        /// <summary>
        /// Also known as a Post-Only order. If this order would have executed on placement, it will cancel instead.
        /// </summary>
        ParticipateDoNotInitiate,
        /// <summary>
        /// Used by stop and if-touched orders to determine the triggering price. Use only one. By default, 'MarkPrice' is used. Also used for Pegged orders to define the value of 'LastPeg'.
        /// </summary>
        MarkPrice,
        /// <summary>
        /// Used by stop and if-touched orders to determine the triggering price. Use only one. By default, 'MarkPrice' is used. Also used for Pegged orders to define the value of 'LastPeg'.
        /// </summary>
        LastPrice,
        /// <summary>
        /// Used by stop and if-touched orders to determine the triggering price. Use only one. By default, 'MarkPrice' is used. Also used for Pegged orders to define the value of 'LastPeg'.
        /// </summary>
        IndexPrice,
        /// <summary>
        /// A 'ReduceOnly' order can only reduce your position, not increase it. If you have a 'ReduceOnly' limit order that rests in the order book while the position is reduced by other orders, then its order quantity will be amended down or canceled. If there are multiple 'ReduceOnly' orders the least aggressive will be amended first.
        /// </summary>
        ReduceOnly,
        /// <summary>
        /// 'Close' implies 'ReduceOnly'. A 'Close' order will cancel other active limit orders with the same side and symbol if the open quantity exceeds the current position. This is useful for stops: by canceling these orders, a 'Close' Stop is ensured to have the margin required to execute, and can only execute up to the full size of your position. If orderQty is not specified, a 'Close' order has an orderQty equal to your current position's size.
        /// Note that a Close order without an orderQty requires a side, so that BitMEX knows if it should trigger above or below the stopPx.
        /// </summary>
        Close
    }

    public enum BitmexPegPriceType:byte
    {
        LastPeg,
        MidPricePeg,
        MarketPeg,
        PrimaryPeg,
        TrailingStopPeg
    }
}
