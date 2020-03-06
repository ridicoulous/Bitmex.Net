using    Bitmex.Net.Client.Objects;
using Bitmex.Net.Client.Objects.Socket;
using CryptoExchange.Net.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace  Bitmex.Net.Client.Converters
{
    public class BitmexWebSocketOperationConverter : BaseConverter<BitmexWebSocketOperation>
    {
        public BitmexWebSocketOperationConverter() : this(true) { }
        public BitmexWebSocketOperationConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<BitmexWebSocketOperation, string>> Mapping => new List<KeyValuePair<BitmexWebSocketOperation, string>>
        {
            new KeyValuePair<BitmexWebSocketOperation, string>(BitmexWebSocketOperation.AuthKeyExpires, "authKeyExpires"),
            new KeyValuePair<BitmexWebSocketOperation, string>(BitmexWebSocketOperation.CancelAllAfter, "cancelAllAfter"),
            new KeyValuePair<BitmexWebSocketOperation, string>(BitmexWebSocketOperation.Subscribe, "subscribe"),
            new KeyValuePair<BitmexWebSocketOperation, string>(BitmexWebSocketOperation.Unsubscribe, "unsubscribe"),
            new KeyValuePair<BitmexWebSocketOperation, string>(BitmexWebSocketOperation.Ping, "ping"),
            new KeyValuePair<BitmexWebSocketOperation, string>(BitmexWebSocketOperation.Undefined, ""),

        };
    }
}
