using    Bitmex.Net.Client.Objects;
using CryptoExchange.Net.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace  Bitmex.Net.Client.Converters
{
    public class BitmexOrderStatusConverter : BaseConverter<BitmexOrderStatus>
    {
        public BitmexOrderStatusConverter() : this(true) { }
        public BitmexOrderStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<BitmexOrderStatus, string>> Mapping => new List<KeyValuePair<BitmexOrderStatus, string>>
        {
            new KeyValuePair<BitmexOrderStatus, string>(BitmexOrderStatus.New, "New"),
            new KeyValuePair<BitmexOrderStatus, string>(BitmexOrderStatus.PartiallyFilled, "PartiallyFilled"),
            new KeyValuePair<BitmexOrderStatus, string>(BitmexOrderStatus.Filled, "Filled"),
            new KeyValuePair<BitmexOrderStatus, string>(BitmexOrderStatus.Canceled, "Canceled"),
            new KeyValuePair<BitmexOrderStatus, string>(BitmexOrderStatus.Rejected, "Rejected"),
            new KeyValuePair<BitmexOrderStatus, string>(BitmexOrderStatus.Undefined, "")
        };
    }
}
