using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Bitmex.Net.Client.Objects.Socket
{

    public enum BitmexAction : byte
    {
        Undefined,
        Partial,
        Insert,
        Update,
        Delete
    }
    public enum BitmexWebSocketOperation : byte
    {

        [DataMember(Name = "ping")]
        Ping,
        [DataMember(Name = "authKeyExpires")]
        AuthKeyExpires,
        [DataMember(Name = "subscribe")]
        Subscribe,
        [DataMember(Name = "unsubscribe")]
        Unsubscribe,
        [DataMember(Name = "cancelAllAfter")]
        CancelAllAfter,
        Undefined
    }
    public enum BitmexSubscribtions : byte
    {
        /// <summary>
        /// Site announcements 
        /// </summary>
        //, Announcement;
        Announcements,//"announcement"
        /// <summary>
        /// Trollbox chat  
        /// </summary>
        Chat,//"chat";
        /// <summary>
        /// Statistics of connected users/bots
        /// </summary>
        Connected,//"connected";
        /// <summary>
        /// Updates of swap funding rates. Sent every funding interval (usually 8hrs)
        /// </summary>
        Funding,//"funding";
        /// <summary>
        /// Instrument updates including turnover and bid/ask 
        /// </summary>
        Instrument,//"instrument";
        /// <summary>
        /// Daily Insurance Fund updates
        /// </summary>
        Insurance,//"insurance";
        /// <summary>
        /// Liquidation orders as they're entered into the book
        /// </summary>
        Liquidation,//"liquidation";
        /// <summary>
        /// Top 25 levels of level 2 order book
        /// </summary>
        OrderBookL2_25,//"orderBookL2_25";
        /// <summary>
        /// Full level 2 order book
        /// </summary>
        OrderBookL2,//"orderBookL2";
        /// <summary>
        ///  Top 10 levels using traditional full book push
        /// </summary>
        OrderBook10,//"orderBook10";
        /// <summary>
        /// System-wide notifications (used for short-lived messages)
        /// </summary>
        PublicNotifications,//"publicNotifications";
        /// <summary>
        /// Top level of the book
        /// </summary>
        Quote,//"quote";
        /// <summary>
        /// 1-minute quote bins
        /// </summary>
        QuoteBin1m,//"quoteBin1m";
        /// <summary>
        /// 5-minute quote bins
        /// </summary>
        QuoteBin5m,//"quoteBin5m";
        /// <summary>
        /// 1-hour quote bins
        /// </summary>
        QuoteBin1h,//"quoteBin1h";
        /// <summary>
        /// 1-day quote bins
        /// </summary>
        QuoteBin1d,//"quoteBin1d";
        /// <summary>
        /// Settlements
        /// </summary>
        Settlement,//"settlement";
        /// <summary>
        /// Live trades
        /// </summary>
        Trade,//"trade";
        /// <summary>
        /// 1-minute trade bins
        /// </summary>
        TradeBin1m,//"tradeBin1m";
        /// <summary>
        /// 5-minute trade bins
        /// </summary>
        TradeBin5m,//"tradeBin5m";
        /// <summary>
        ///  1-hour trade bins
        /// </summary>
        TradeBin1h,//"tradeBin1h";
        /// <summary>
        /// 1-day trade bins
        /// </summary>
        TradeBin1d,//"tradeBin1d";
        /// <summary>
        /// Affiliate status, such as total referred users & payout
        /// </summary>
        Affiliate,//"affiliate";
        /// <summary>
        /// Individual executions; can be multiple per order
        /// </summary>
        Execution,//"execution";
        /// <summary>
        /// Live updates on your orders
        /// </summary>
        Order,//"order";
        /// <summary>
        /// Updates on your current account balance and margin requirements
        /// </summary>
        Margin,//"margin";
        /// <summary>
        /// Updates on your positions
        /// </summary>
        Position,//"position";
        /// <summary>
        /// Deposit/Withdrawal updates
        /// </summary>
        Transact,//"transact";

        ///<summary>
        /// Bitcoin address balance data, including total deposits & withdrawals
        ///</summary>
        Wallet,//"wallet";

    }
}
