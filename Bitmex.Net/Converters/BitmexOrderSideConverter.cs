using Bitmex.Net.Client.Objects;
using CryptoExchange.Net.Converters;
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
}
