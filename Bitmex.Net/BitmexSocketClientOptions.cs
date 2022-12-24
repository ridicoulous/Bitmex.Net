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

        
        public BitmexSocketClientOptions() : this(false, true)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="secret"></param>
        /// <param name="isTestnet"></param>
        /// <param name="loadInstrumentIndexes">If you will subscribe to orderbook, set it to true, cause instrument index and tick size will be used for price calculation</param>
        public BitmexSocketClientOptions(string key, string secret, bool isTestnet = false, bool loadInstrumentIndexes = true) : this(isTestnet, loadInstrumentIndexes)
        {
            key.ValidateNotNull(nameof(key));
            secret.ValidateNotNull(nameof(secret));
            ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials(key, secret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isTestnet"></param>
        /// <param name="loadInstrumentIndexes">If you will subscribe to orderbook, set it to true, cause instrument index and tick size will be used for price calculation</param>
        public BitmexSocketClientOptions(bool isTestnet, bool loadInstrumentIndexes=true, SocketApiClientOptions commonStreamsOptions = null) : base()
        {
            CommonStreamsOptions = commonStreamsOptions ?? new SocketApiClientOptions(isTestnet ? TestNetSocketEndpoint : SocketEndpoint);
            LoadInstruments = loadInstrumentIndexes;
            IsTestnet = isTestnet;
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
        public SocketApiClientOptions CommonStreamsOptions { get;}
        public bool IsTestnet { get; private set; }


        public BitmexSocketClientOptions Copy()
        {
            return new BitmexSocketClientOptions(this);
        }

    }
}
