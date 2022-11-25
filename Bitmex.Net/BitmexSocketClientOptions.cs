using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitmex.Net.Client
{    
    public class BitmexSocketClientOptions : ClientOptions
    {
        private const string SocketEndpoint = "wss://ws.bitmex.com/realtime";
        private const string TestNetSocketEndpoint = "wss://ws.testnet.bitmex.com/realtime";
        public readonly bool LoadInstruments;
        public bool SendPingManually = false;

        
        public BitmexSocketClientOptions() : this(false, false, true)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isTestnet"></param>
        /// <param name="useMultiplexing"></param>
        /// <param name="loadInstrumentIndexes">If you will subscribe to orderbook, set it to true, cause instrument index and tick size will be used for price calculation</param>
        public BitmexSocketClientOptions(bool isTestnet, bool useMultiplexing = false, bool loadInstrumentIndexes=true) : base()
        {
            CommonStreamsOptions = new SocketApiClientOptions(isTestnet ? TestNetSocketEndpoint : SocketEndpoint);
            LoadInstruments = loadInstrumentIndexes;
            IsTestnet = isTestnet;
            if (useMultiplexing)
            {
                throw new NotImplementedException("Multiplex client is not implemented yet");
            }
        }

        private BitmexSocketClientOptions(BitmexSocketClientOptions baseOn) : base(baseOn)
        {
            IsTestnet = baseOn.IsTestnet;
            LoadInstruments = baseOn.LoadInstruments;
            SendPingManually = baseOn.SendPingManually;
            CommonStreamsOptions = baseOn.CommonStreamsOptions;
        }

        /// <summary>
        /// Default options
        /// </summary>
        public static BitmexSocketClientOptions Default { get; set; } = new BitmexSocketClientOptions()
        {
        };
        internal SocketApiClientOptions CommonStreamsOptions { get; set; }
        public bool IsTestnet { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="secret"></param>
        /// <param name="isTestnet"></param>
        /// <param name=""></param>
        /// <param name="loadInstrumentIndexes">If you will subscribe to orderbook, set it to true, cause instrument index and tick size will be used for price calculation</param>
        /// <param name="useMultiplexing"></param>
        public BitmexSocketClientOptions(string key, string secret, bool isTestnet = false, bool loadInstrumentIndexes = true, bool useMultiplexing = false) : base()
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
            return new BitmexSocketClientOptions(this);
        }

    }
}
