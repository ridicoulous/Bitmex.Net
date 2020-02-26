using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitmex.Net.Client
{
    public class BitmexSocketClientOptions : SocketClientOptions
    {
        public BitmexSocketClientOptions():base("wss://www.bitmex.com/realtime")
        {

        }
        public BitmexSocketClientOptions(bool isTestnet = false, bool useMultiplexing = false) : base(isTestnet ? "wss://testnet.bitmex.com/realtime":"wss://www.bitmex.com/realtime" )
        {
            if (useMultiplexing)
            {
                throw new NotImplementedException("Multiplex client is not implemented yet");
            }
        }
        public BitmexSocketClientOptions Copy()
        {
            var copy = Copy<BitmexSocketClientOptions>();

            return copy;
        }
    }
}
