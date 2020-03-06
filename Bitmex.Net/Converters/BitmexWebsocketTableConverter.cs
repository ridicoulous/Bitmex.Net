using Bitmex.Net.Client.Objects;
using Bitmex.Net.Client.Objects.Socket;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitmex.Net.Client.Converters
{
    public class BitmexWebsocketTableConverter : BaseConverter<BitmexSubscribtions>
    {
        public BitmexWebsocketTableConverter() : this(true) { }
        public BitmexWebsocketTableConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<BitmexSubscribtions, string>> Mapping => new List<KeyValuePair<BitmexSubscribtions, string>>
        {
            new KeyValuePair<BitmexSubscribtions, string>(BitmexSubscribtions.Announcements, "announcement"),
            new KeyValuePair<BitmexSubscribtions, string>(BitmexSubscribtions.Chat, "chat"),
            new KeyValuePair<BitmexSubscribtions, string>(BitmexSubscribtions.Connected, "connected"),
            new KeyValuePair<BitmexSubscribtions, string>(BitmexSubscribtions.Funding, "funding"),
            new KeyValuePair<BitmexSubscribtions, string>(BitmexSubscribtions.Instrument, "instrument"),
            new KeyValuePair<BitmexSubscribtions, string>(BitmexSubscribtions.Insurance, "insurance"),
            new KeyValuePair<BitmexSubscribtions, string>(BitmexSubscribtions.Liquidation, "liquidation"),
            new KeyValuePair<BitmexSubscribtions, string>(BitmexSubscribtions.OrderBookL2_25, "orderBookL2_25"),
            new KeyValuePair<BitmexSubscribtions, string>(BitmexSubscribtions.OrderBookL2, "orderBookL2"),
            new KeyValuePair<BitmexSubscribtions, string>(BitmexSubscribtions.OrderBook10, "orderBook10"),
            new KeyValuePair<BitmexSubscribtions, string>(BitmexSubscribtions.PublicNotifications, "publicNotifications"),
            new KeyValuePair<BitmexSubscribtions, string>(BitmexSubscribtions.Quote, "quote"),
            new KeyValuePair<BitmexSubscribtions, string>(BitmexSubscribtions.QuoteBin1m, "quoteBin1m"),
            new KeyValuePair<BitmexSubscribtions, string>(BitmexSubscribtions.QuoteBin5m, "quoteBin5m"),
            new KeyValuePair<BitmexSubscribtions, string>(BitmexSubscribtions.QuoteBin1h, "quoteBin1h"),
            new KeyValuePair<BitmexSubscribtions, string>(BitmexSubscribtions.QuoteBin1d, "quoteBin1d"),
            new KeyValuePair<BitmexSubscribtions, string>(BitmexSubscribtions.Settlement, "settlement"),
            new KeyValuePair<BitmexSubscribtions, string>(BitmexSubscribtions.Trade, "trade"),
            new KeyValuePair<BitmexSubscribtions, string>(BitmexSubscribtions.TradeBin1m, "tradeBin1m"),
            new KeyValuePair<BitmexSubscribtions, string>(BitmexSubscribtions.TradeBin5m, "tradeBin5m"),
            new KeyValuePair<BitmexSubscribtions, string>(BitmexSubscribtions.TradeBin1h, "tradeBin1h"),
            new KeyValuePair<BitmexSubscribtions, string>(BitmexSubscribtions.TradeBin1d, "tradeBin1d"),
            new KeyValuePair<BitmexSubscribtions, string>(BitmexSubscribtions.Affiliate, "affiliate"),
            new KeyValuePair<BitmexSubscribtions, string>(BitmexSubscribtions.Execution, "execution"),
            new KeyValuePair<BitmexSubscribtions, string>(BitmexSubscribtions.Order, "order"),
            new KeyValuePair<BitmexSubscribtions, string>(BitmexSubscribtions.Margin, "margin"),
            new KeyValuePair<BitmexSubscribtions, string>(BitmexSubscribtions.Position, "position"),
            new KeyValuePair<BitmexSubscribtions, string>(BitmexSubscribtions.Transact, "transact"),
            new KeyValuePair<BitmexSubscribtions, string>(BitmexSubscribtions.Wallet, "wallet")   
        };
    }
   
}
