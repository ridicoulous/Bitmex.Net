using    Bitmex.Net.Client.Objects;
using CryptoExchange.Net.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace  Bitmex.Net.Client.Converters
{
    public class BitmexTickDirectionConverter : BaseConverter<BitmexTickDirection>
    {
        public BitmexTickDirectionConverter() : this(true) { }
        public BitmexTickDirectionConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<BitmexTickDirection, string>> Mapping => new List<KeyValuePair<BitmexTickDirection, string>>
        {
            new KeyValuePair<BitmexTickDirection, string>(BitmexTickDirection.MinusTick, "MinusTick"),
            new KeyValuePair<BitmexTickDirection, string>(BitmexTickDirection.PlusTick, "PlusTick"),
            new KeyValuePair<BitmexTickDirection, string>(BitmexTickDirection.Undefined, "Undefined"),
            new KeyValuePair<BitmexTickDirection, string>(BitmexTickDirection.ZeroMinusTick, "ZeroMinusTick"),
            new KeyValuePair<BitmexTickDirection, string>(BitmexTickDirection.ZeroPlusTick, "ZeroPlusTick"),
        };
    }
}
