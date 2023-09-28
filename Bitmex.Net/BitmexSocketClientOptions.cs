using CryptoExchange.Net;
using CryptoExchange.Net.Objects.Options;

namespace Bitmex.Net.Client
{
    public class BitmexSocketClientOptions : SocketApiOptions
    {
        private const string SocketEndpoint = "wss://ws.bitmex.com/realtime";
        private const string TestNetSocketEndpoint = "wss://ws.testnet.bitmex.com/realtime";
        public bool SendPingManually = false;

        public BitmexSocketClientOptions() : this(false)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="secret"></param>
        /// <param name="isTestnet"></param>
        public BitmexSocketClientOptions(string key, string secret, bool isTestnet = false) : this(isTestnet)
        {
            key.ValidateNotNull(nameof(key));
            secret.ValidateNotNull(nameof(secret));
            ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials(key, secret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isTestnet"></param>
        public BitmexSocketClientOptions(bool isTestnet, SocketExchangeOptions commonStreamsOptions = null) : base()
        {
            CommonStreamsOptions = commonStreamsOptions ?? new();
            IsTestnet = isTestnet;
        }

        /// <summary>
        /// Default options
        /// </summary>
        public static BitmexSocketClientOptions Default { get; set; } = new BitmexSocketClientOptions();
        public SocketExchangeOptions CommonStreamsOptions { get; private set; }
        public bool IsTestnet { get; private set; }
        internal virtual string BaseAddress => IsTestnet ? TestNetSocketEndpoint : SocketEndpoint;

        public BitmexSocketClientOptions Copy() => Copy<BitmexSocketClientOptions>();
        public new BitmexSocketClientOptions Copy<T>()
        where T : BitmexSocketClientOptions, new()
        {
            var newOpt = base.Copy<T>();
            newOpt.IsTestnet = IsTestnet;
            newOpt.SendPingManually = SendPingManually;
            newOpt.CommonStreamsOptions = CommonStreamsOptions;
            return newOpt;
        }
    }
    internal class BitmexNonTradeSocketClientOptions :BitmexSocketClientOptions
    {
        internal override string BaseAddress => $"{base.BaseAddress}Platform";
    }
}
