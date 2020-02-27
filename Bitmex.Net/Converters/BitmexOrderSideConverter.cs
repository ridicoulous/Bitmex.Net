using Bitmex.Net.Client.Objects;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitmex.Net.Client.Converters
{
    public class BitmexOrderSideConverter : BaseConverter<BitmexOrderSide>
    {
        public BitmexOrderSideConverter() : this(true) { }
        public BitmexOrderSideConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<BitmexOrderSide, string>> Mapping => new List<KeyValuePair<BitmexOrderSide, string>>
        {
            new KeyValuePair<BitmexOrderSide, string>(BitmexOrderSide.Buy, "Buy"),
            new KeyValuePair<BitmexOrderSide, string>(BitmexOrderSide.Sell, "Sell"),

        };
    }
    public class BitmexOrderSideToOrderBookEntryTypeConverter : BaseConverter<OrderBookEntryType>
    {
        public BitmexOrderSideToOrderBookEntryTypeConverter() : this(true) { }
        public BitmexOrderSideToOrderBookEntryTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OrderBookEntryType, string>> Mapping => new List<KeyValuePair<OrderBookEntryType, string>>
        {
            new KeyValuePair<OrderBookEntryType, string>(OrderBookEntryType.Bid, "Buy"),
            new KeyValuePair<OrderBookEntryType, string>(OrderBookEntryType.Ask, "Sell"),

        };
    }
}
