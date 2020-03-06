using System;
using System.Collections.Generic;
using System.Text;

namespace Bitmex.Net.Client.Objects.Socket.Repsonses
{
    public  class ResponseTableToDataTypeMapping
    {
        public Dictionary<string,BitmexSubscribtions> Mappings = new Dictionary<string, BitmexSubscribtions>() { };

        public ResponseTableToDataTypeMapping()
        {
            Mappings.Add("announcement", BitmexSubscribtions.Announcements);
            Mappings.Add("chat", BitmexSubscribtions.Chat);
            Mappings.Add("connected", BitmexSubscribtions.Connected);
            Mappings.Add("funding", BitmexSubscribtions.Funding);
            Mappings.Add("instrument", BitmexSubscribtions.Instrument);
            Mappings.Add("insurance", BitmexSubscribtions.Insurance);
            Mappings.Add("liquidation", BitmexSubscribtions.Liquidation);
            Mappings.Add("orderBookL2_25", BitmexSubscribtions.OrderBookL2_25);
            Mappings.Add("orderBookL2", BitmexSubscribtions.OrderBookL2);
            Mappings.Add("orderBook10", BitmexSubscribtions.OrderBook10);
            Mappings.Add("publicNotifications", BitmexSubscribtions.PublicNotifications);
            Mappings.Add("quote", BitmexSubscribtions.Quote);
            Mappings.Add("quoteBin1m", BitmexSubscribtions.QuoteBin1m);
            Mappings.Add("quoteBin5m", BitmexSubscribtions.QuoteBin5m);
            Mappings.Add("quoteBin1h", BitmexSubscribtions.QuoteBin1h);
            Mappings.Add("quoteBin1d", BitmexSubscribtions.QuoteBin1d);
            Mappings.Add("settlement", BitmexSubscribtions.Settlement);
            Mappings.Add("trade", BitmexSubscribtions.Trade);
            Mappings.Add("tradeBin1m", BitmexSubscribtions.TradeBin1m);
            Mappings.Add("tradeBin5m", BitmexSubscribtions.TradeBin5m);
            Mappings.Add("tradeBin1h", BitmexSubscribtions.TradeBin1h);
            Mappings.Add("tradeBin1d", BitmexSubscribtions.TradeBin1d);
            Mappings.Add("affiliate", BitmexSubscribtions.Affiliate);
            Mappings.Add("execution", BitmexSubscribtions.Execution);
            Mappings.Add("order", BitmexSubscribtions.Order);
            Mappings.Add("margin", BitmexSubscribtions.Margin);
            Mappings.Add("position", BitmexSubscribtions.Position);
            Mappings.Add("transact", BitmexSubscribtions.Transact);
            Mappings.Add("wallet", BitmexSubscribtions.Wallet);
        }
 
    }
}
