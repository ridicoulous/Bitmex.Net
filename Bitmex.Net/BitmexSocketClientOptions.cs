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
        /// <param name="loadInstrumentIndexes">now is obsolete and not used anymore, left for compatibility</param>
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
        /// <param name="loadInstrumentIndexes">now is obsolete and not used anymore, left for compatibility</param>
        public BitmexSocketClientOptions(bool isTestnet, bool loadInstrumentIndexes=true, SocketApiClientOptions commonStreamsOptions = null) : base()
        {
            CommonStreamsOptions = commonStreamsOptions ?? new SocketApiClientOptions(isTestnet ? TestNetSocketEndpoint : SocketEndpoint);
            IsTestnet = isTestnet;
        }

        private BitmexSocketClientOptions(BitmexSocketClientOptions baseOn) : base(baseOn)
        {
            IsTestnet = baseOn.IsTestnet;
            SendPingManually = baseOn.SendPingManually;
            CommonStreamsOptions = baseOn.CommonStreamsOptions;
        }

        /// <summary>
        /// Default options
        /// </summary>
        public static BitmexSocketClientOptions Default { get; set; } = new BitmexSocketClientOptions()
        {
        };
        public SocketApiClientOptions CommonStreamsOptions { get; private set; }
        public bool IsTestnet { get; private set; }


        public BitmexSocketClientOptions Copy()
        {
            return new BitmexSocketClientOptions(this);
        }

        internal BitmexSocketClientOptions CopyWithNonTradeSocketEndpoint()
        {
            return new BitmexSocketClientOptions(this)
            {
                CommonStreamsOptions = new(this.CommonStreamsOptions, new($"{this.CommonStreamsOptions.BaseAddress}Platform"))
            };
        }

    }
}
