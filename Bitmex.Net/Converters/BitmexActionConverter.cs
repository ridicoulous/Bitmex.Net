using Bitmex.Net.Client.Objects.Socket;
using CryptoExchange.Net.Converters;
using System.Collections.Generic;

namespace  Bitmex.Net.Client.Converters
{
    public class BitmexActionConverter : BaseConverter<BitmexAction>
    {
        public BitmexActionConverter() : this(true) { }
        public BitmexActionConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<BitmexAction, string>> Mapping => new List<KeyValuePair<BitmexAction, string>>
        {
            new KeyValuePair<BitmexAction, string>(BitmexAction.Undefined, ""),
            new KeyValuePair<BitmexAction, string>(BitmexAction.Insert, "insert"),
            new KeyValuePair<BitmexAction, string>(BitmexAction.Partial, "partial"),
            new KeyValuePair<BitmexAction, string>(BitmexAction.Update, "update"),
            new KeyValuePair<BitmexAction, string>(BitmexAction.Delete, "delete"),
        };
    }
}
