using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitmex.Net.Client
{    
    public class BitmexSocketClientOptions : SocketClientOptions
    {
        public bool IsTestnet;
        public readonly bool LoadInstruments;
        public bool SendPingManually =false;

        public BitmexSocketClientOptions():base("wss://www.bitmex.com/realtime")
        {
            IsTestnet = false;
            LoadInstruments = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isTestnet"></param>
        /// <param name="useMultiplexing"></param>
        /// <param name="loadInstrumentIndexes">If you will subscribe to orderbook, set it to true, cause instrument index and tick size will be used for price calculation</param>
        public BitmexSocketClientOptions(bool isTestnet = false, bool useMultiplexing = false, bool loadInstrumentIndexes=true) : base(isTestnet ? "wss://testnet.bitmex.com/realtime":"wss://www.bitmex.com/realtime" )
        {
            LoadInstruments = loadInstrumentIndexes;
            IsTestnet = isTestnet;
            if (useMultiplexing)
            {
                throw new NotImplementedException("Multiplex client is not implemented yet");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="secret"></param>
        /// <param name="isTestnet"></param>
        /// <param name=""></param>
        /// <param name="loadInstrumentIndexes">If you will subscribe to orderbook, set it to true, cause instrument index and tick size will be used for price calculation</param>
        /// <param name="useMultiplexing"></param>
        public BitmexSocketClientOptions(string key, string secret, bool isTestnet = false, bool loadInstrumentIndexes = true, bool useMultiplexing = false) : base(isTestnet ? "wss://testnet.bitmex.com/realtime" : "wss://www.bitmex.com/realtime")
        {
            LoadInstruments = loadInstrumentIndexes;
            if (useMultiplexing)
            {
                throw new NotImplementedException("Multiplex client is not implemented yet");
            }
            IsTestnet = isTestnet;
            key.ValidateNotNull(nameof(key));
            secret.ValidateNotNull(nameof(secret));
            ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials(key, secret);
        }
        public BitmexSocketClientOptions Copy()
        {
            var copy = Copy<BitmexSocketClientOptions>();

            return copy;
        }
    }
}
