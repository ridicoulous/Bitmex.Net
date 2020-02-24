using Bitmex.Net.Objects;
using CryptoExchange.Net.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitmex.Net.Converters
{
    public class BitmexOrderTypeConverter : BaseConverter<BitmexOrderType>
    {
        public BitmexOrderTypeConverter() : this(true) { }
        public BitmexOrderTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<BitmexOrderType, string>> Mapping => new List<KeyValuePair<BitmexOrderType, string>>
        {
            new KeyValuePair<BitmexOrderType, string>(BitmexOrderType.Limit, "Limit"),
            new KeyValuePair<BitmexOrderType, string>(BitmexOrderType.LimitIfTouched, "LimitIfTouched"),
            new KeyValuePair<BitmexOrderType, string>(BitmexOrderType.Market, "Market"),
            new KeyValuePair<BitmexOrderType, string>(BitmexOrderType.MarketIfTouched, "MarketIfTouched"),
            new KeyValuePair<BitmexOrderType, string>(BitmexOrderType.Stop, "Stop"),
            new KeyValuePair<BitmexOrderType, string>(BitmexOrderType.StopLimit, "StopLimit"),

        };
    }
}
