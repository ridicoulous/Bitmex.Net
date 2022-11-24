using Bitmex.Net.Client.Helpers.Extensions;
using Bitmex.Net.Client.Interfaces;
using Bitmex.Net.Client.Objects;
using Bitmex.Net.Client.Objects.Socket;
using Bitmex.Net.Client.Objects.Socket.Repsonses;
using Bitmex.Net.Client.Objects.Socket.Requests;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bitmex.Net.Client
{
    public class BitmexSocketStream : SocketApiClient, IBitmexSocketStream
    {
        private static readonly Dictionary<string, BitmexInstrumentIndexWithTick> instrumentsIndexesAndTicks = new Dictionary<string, BitmexInstrumentIndexWithTick>();
        private static readonly SemaphoreSlim instumentGetWaiter = new(1,1);
        private static bool areInstrumentsLoaded;
        protected Log log;
        protected BitmexSocketClient socketClient;
        private readonly bool isTestnet;
        private bool shouldUseIndexesAndTicksFromBitmex = false;

        public BitmexSocketStream(Log log, BitmexSocketClient bitmexSocketClient, BitmexSocketClientOptions options) : base(options, options.CommonStreamsOptions)
        {
            isTestnet = options.IsTestnet;
            this.log = log;
            this.socketClient = bitmexSocketClient;        

            if (options.LoadInstruments)
            {
                shouldUseIndexesAndTicksFromBitmex = true;
                Task.Run( async () => await GetInstrumentsTickerAndIndices());
            }
        }

        #region events
        public event Action<BitmexSocketEvent<Announcement>> OnAnnouncementUpdate;
        public event Action<BitmexSocketEvent<Chat>> OnChatMessageUpdate;
        public event Action<BitmexSocketEvent<ConnectedUsers>> OnChatConnectionUpdate;
        public event Action<BitmexSocketEvent<Funding>> OnFundingUpdate;
        public event Action<BitmexSocketEvent<Instrument>> OnInstrimentUpdate;
        public event Action<BitmexSocketEvent<Insurance>> OnInsuranceUpdate;
        public event Action<BitmexSocketEvent<Liquidation>> OnLiquidationUpdate;
        public event Action<BitmexSocketEvent<BitmexOrderBookEntry>> OnOrderBookL2_25Update;
        public event Action<BitmexSocketEvent<BitmexOrderBookEntry>> OnorderBookL2Update;
        public event Action<BitmexSocketEvent<BitmexOrderBookL10>> OnOrderBook10Update;
        public event Action<BitmexSocketEvent<GlobalNotification>> OnGlobalNotificationUpdate;
        public event Action<BitmexSocketEvent<Quote>> OnQuotesUpdate;
        public event Action<BitmexSocketEvent<Quote>> OnOneMinuteQuoteBinUpdate;
        public event Action<BitmexSocketEvent<Quote>> OnFiveMinuteQuoteBinUpdate;
        public event Action<BitmexSocketEvent<Quote>> OnOneHourQuoteBinUpdate;
        public event Action<BitmexSocketEvent<Quote>> OnDailyQuoteBinUpdate;
        public event Action<BitmexSocketEvent<Settlement>> OnSettlementUpdate;
        public event Action<BitmexSocketEvent<BitmexTrade>> OnTradeUpdate;
        public event Action<BitmexSocketEvent<TradeBin>> OnOneMinuteTradeBinUpdate;
        public event Action<BitmexSocketEvent<TradeBin>> OnFiveMinuteTradeBinUpdate;
        public event Action<BitmexSocketEvent<TradeBin>> OnOneHourTradeBinUpdate;
        public event Action<BitmexSocketEvent<TradeBin>> OnDailyTradeBinUpdate;
        public event Action<BitmexSocketEvent<Affiliate>> OnUserAffiliatesUpdate;
        public event Action<BitmexSocketEvent<Execution>> OnUserExecutionsUpdate;
        public event Action<BitmexSocketEvent<BitmexOrder>> OnUserOrdersUpdate;
        public event Action<BitmexSocketEvent<Margin>> OnUserMarginUpdate;
        public event Action<BitmexSocketEvent<BitmexPosition>> OnUserPositionsUpdate;
        public event Action<BitmexSocketEvent<Transaction>> OnUserTransactionsUpdate;
        public event Action<BitmexSocketEvent<Wallet>> OnUserWalletUpdate;
        
        #endregion

        // protected override IWebsocket CreateSocket(string address)
        // {
        //     Dictionary<string, string> emptyCoockies = new Dictionary<string, string>();
        //     Dictionary<string, string> headers = new Dictionary<string, string>();
        //     //if (authProvider != null)
        //     //{
        //     //    headers = this.authProvider.AddAuthenticationToHeaders("bitmex.com/realtime", HttpMethod.Get, null, true, PostParameters.InUri, ArrayParametersSerialization.MultipleValues);
        //     //}
        //     headers.Add("Accept-Encoding", "gzip, deflate, br");
        //     headers.Add("Cache-Control", "no-cache");
        //     headers.Add("Connection", "Upgrade");
        //     headers.Add("Host", $"{(isTestnet ? "testnet" : "www")}.bitmex.com");
        //     headers.Add("Origin", $"https://{(isTestnet ? "testnet" : "www")}.bitmex.com");
        //     headers.Add("Sec-WebSocket-Extensions", "");
        //     headers.Add("Sec-WebSocket-Version", "13");
        //     headers.Add("Upgrade", "websocket");
        //     headers.Add("User-Agent", "https://github.com/ridicoulous/Bitmex.Net/");

        //     var s = SocketFactory.CreateWebsocket(this.log, address, emptyCoockies, headers);
        //     s.Origin = $"https://{(isTestnet ? "testnet" : "www")}.bitmex.com";
        //     s.OnClose += S_OnClose;
        //     s.OnError += S_OnError;
        //     s.OnOpen += S_OnOpen;
        //     return s;
        // }
   


        // public override async Task UnsubscribeAllAsync()
        // {
        //     foreach (var c in this._subscriptions)
        //     {
        //         await c.CloseAsync().ConfigureAwait(false);
        //     }
        //     await base.UnsubscribeAllAsync();
        // }

        

        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(Action<DataEvent<BitmexSocketEvent<BitmexOrderBookEntry>>> onData, string symbol = "", bool full = false, CancellationToken ct = default)
        {
            BitmexSubscribtions orderbookType = full ? BitmexSubscribtions.OrderBookL2 : BitmexSubscribtions.OrderBookL2_25;
            return await SubscribeAsync(new BitmexSubscribeRequest().AddSubscription(orderbookType, symbol), onData, ct);
        }

        ResponseTableToDataTypeMapping Map = new ResponseTableToDataTypeMapping();

        public async Task<CallResult<UpdateSubscription>> SubscribeAsync(BitmexSubscribeRequest bitmexSubscribeRequest, CancellationToken ct = default)
        {

            var handler = new Action<DataEvent<string>>(dataEvent =>
            {
                var data = dataEvent.Data;
                var token = JToken.Parse(data);
                var table = (string)token["table"];

                if (String.IsNullOrEmpty(table) || !Map.Mappings.ContainsKey(table))
                {
                    var subscriptionResponse = (string)token["subscribe"];
                    if (!String.IsNullOrEmpty(subscriptionResponse))
                    {
                        var response = Deserialize<BitmexSubscriptionResponse>(data);
                        if (response)
                            socketClient.SendedSubscriptions.TryAdd(response.Data.Subscribe, response.Data.Request);
                        return;
                    }
                    else if (!String.IsNullOrEmpty((string)token["unsubscribe"]))
                    {
                        var response = Deserialize<BitmexSubscriptionResponse>(data);
                        if (response)
                            OnUnsubscribe(response.Data);
                        return;
                    }
                    else
                    {
                        log.Write(LogLevel.Warning, $"Unknown table [{table}] update catched at data {data}");
                        return;
                    }
                }
                BitmexSubscribtions updatedTable = Map.Mappings[table];

                switch (updatedTable)
                {
                    case BitmexSubscribtions.Announcements:
                        {
                            var result = Deserialize<BitmexSocketEvent<Announcement>>(token);
                            if (result.Success)
                                OnAnnouncementUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Chat:
                        {
                            var result = Deserialize<BitmexSocketEvent<Chat>>(token);
                            if (result.Success)
                                OnChatMessageUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Connected:
                        {
                            var result = Deserialize<BitmexSocketEvent<ConnectedUsers>>(token);
                            if (result.Success)
                                OnChatConnectionUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Funding:
                        {
                            var result = Deserialize<BitmexSocketEvent<Funding>>(token);
                            if (result.Success)
                                OnFundingUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Instrument:
                        {
                            var result = Deserialize<BitmexSocketEvent<Instrument>>(token);
                            if (result.Success)
                                OnInstrimentUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Insurance:
                        {
                            var result = Deserialize<BitmexSocketEvent<Insurance>>(token);
                            if (result.Success)
                                OnInsuranceUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Liquidation:
                        {
                            var result = Deserialize<BitmexSocketEvent<Liquidation>>(token);
                            if (result.Success)
                                OnLiquidationUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.OrderBookL2_25:
                        {
                            var result = Deserialize<BitmexSocketEvent<BitmexOrderBookEntry>>(token);
                            if (result.Success)
                            {
                                if (shouldUseIndexesAndTicksFromBitmex)
                                {
                                    foreach (var level in result.Data.Data)
                                    {
                                        var symbolTickInfo = GetIndexAndTickForInstrument(level.Symbol);
                                        level.SetPrice(symbolTickInfo.Index, symbolTickInfo.TickSize);
                                    }
                                }
                                OnOrderBookL2_25Update?.Invoke(result.Data);
                            }
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.OrderBookL2:
                        {
                            var result = Deserialize<BitmexSocketEvent<BitmexOrderBookEntry>>(token);
                            if (result.Success)
                            {
                                if (shouldUseIndexesAndTicksFromBitmex)
                                {
                                    foreach (var level in result.Data.Data)
                                    {
                                        var symbolTickInfo = GetIndexAndTickForInstrument(level.Symbol);
                                        level.SetPrice(symbolTickInfo.Index, symbolTickInfo.TickSize);
                                    }
                                }
                                OnorderBookL2Update?.Invoke(result.Data);
                            }
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.OrderBook10:
                        {
                            var result = Deserialize<BitmexSocketEvent<BitmexOrderBookL10>>(token);
                            if (result.Success)
                                OnOrderBook10Update?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.PublicNotifications:
                        {
                            var result = Deserialize<BitmexSocketEvent<GlobalNotification>>(token);
                            if (result.Success)
                                OnGlobalNotificationUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Quote:
                        {
                            var result = Deserialize<BitmexSocketEvent<Quote>>(token);
                            if (result.Success)
                                OnQuotesUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.QuoteBin1m:
                        {
                            var result = Deserialize<BitmexSocketEvent<Quote>>(token);
                            if (result.Success)
                                OnOneMinuteQuoteBinUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.QuoteBin5m:
                        {
                            var result = Deserialize<BitmexSocketEvent<Quote>>(token);
                            if (result.Success)
                                OnFiveMinuteQuoteBinUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.QuoteBin1h:
                        {
                            var result = Deserialize<BitmexSocketEvent<Quote>>(token);
                            if (result.Success)
                                OnOneHourQuoteBinUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.QuoteBin1d:
                        {
                            var result = Deserialize<BitmexSocketEvent<Quote>>(token);
                            if (result.Success)
                                OnDailyQuoteBinUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Settlement:
                        {
                            var result = Deserialize<BitmexSocketEvent<Settlement>>(token);
                            if (result.Success)
                                OnSettlementUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Trade:
                        {
                            var result = Deserialize<BitmexSocketEvent<BitmexTrade>>(token);
                            if (result.Success)
                                OnTradeUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.TradeBin1m:
                        {
                            var result = Deserialize<BitmexSocketEvent<TradeBin>>(token);
                            if (result.Success)
                                OnOneMinuteTradeBinUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.TradeBin5m:
                        {
                            var result = Deserialize<BitmexSocketEvent<TradeBin>>(token);
                            if (result.Success)
                                OnFiveMinuteTradeBinUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.TradeBin1h:
                        {
                            var result = Deserialize<BitmexSocketEvent<TradeBin>>(token);
                            if (result.Success)
                                OnOneHourTradeBinUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.TradeBin1d:
                        {
                            var result = Deserialize<BitmexSocketEvent<TradeBin>>(token);
                            if (result.Success)
                                OnDailyTradeBinUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Affiliate:
                        {
                            var result = Deserialize<BitmexSocketEvent<Affiliate>>(token);
                            if (result.Success)
                                OnUserAffiliatesUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Execution:
                        {
                            var result = Deserialize<BitmexSocketEvent<Execution>>(token);
                            if (result.Success)
                                OnUserExecutionsUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Order:
                        {
                            var result = Deserialize<BitmexSocketEvent<BitmexOrder>>(token);
                            if (result.Success)
                                OnUserOrdersUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Margin:
                        {
                            var result = Deserialize<BitmexSocketEvent<Margin>>(token);
                            if (result.Success)
                                OnUserMarginUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Position:
                        {
                            var result = Deserialize<BitmexSocketEvent<BitmexPosition>>(token);
                            if (result.Success)
                                OnUserPositionsUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Transact:
                        {
                            var result = Deserialize<BitmexSocketEvent<Transaction>>(token);
                            if (result.Success)
                                OnUserTransactionsUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Wallet:
                        {
                            var result = Deserialize<BitmexSocketEvent<Wallet>>(token);
                            if (result.Success)
                                OnUserWalletUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogLevel.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    default:
                        {
                            log.Write(LogLevel.Warning, $"Catched inknown table update: {data}");
                            break;
                        }
                }
            });
            return await SubscribeAsync(bitmexSubscribeRequest, handler, ct);
        }

        private async Task<CallResult<UpdateSubscription>> SubscribeAsync<T>(BitmexSubscribeRequest request, Action<DataEvent<T>> onData, CancellationToken ct)
        {
            return await socketClient.SubscribeInternal(this, BaseAddress, request, AuthenticationProvider is not null, onData, ct);
        }

        private void OnUnsubscribe(BitmexSubscriptionResponse response)
        {
            log.Write(LogLevel.Debug, $"Unsub: {JsonConvert.SerializeObject(response)}");
            if (!String.IsNullOrEmpty(response.Unsubscribe))
            {
                // _sendedSubscriptions.TryRemove(response.Unsubscribe, out _);
                log.Write(LogLevel.Warning, $"{response.Unsubscribe} topic from {JsonConvert.SerializeObject(response.Request)} subscription was unsubscribed");
            }
        }

        public BitmexInstrumentIndexWithTick GetIndexAndTickForInstrument(string instrument)
        {
            if (!shouldUseIndexesAndTicksFromBitmex)
            {
                return null;
            }
            if (!areInstrumentsLoaded)
            {
                GetInstrumentsTickerAndIndices().GetAwaiter().GetResult();
            }
            return instrumentsIndexesAndTicks[instrument];
        }

        private async Task GetInstrumentsTickerAndIndices()
        {
            await instumentGetWaiter.WaitAsync();
            if (areInstrumentsLoaded)
            {
                instumentGetWaiter.Release();
                return;
            }
            using (var bitmexClient = new BitmexClient(new BitmexClientOptions(isTest: isTestnet){LogLevel = this.log?.Level ?? LogLevel.Information}))
            {
                var getByOnce = 500;
                var lastResponseItemCount = 0;
                var ind = 0;
                do
                {
                    var instruments = await bitmexClient.MarginClient.GetInstrumentsAsync(new Objects.Requests.BitmexRequestWithFilter().WithStartingFrom(ind).WithResultsCount(500).AddColumnsToGetInRequest(new string[] { "symbol", "tickSize" }));
                    if (instruments)
                    {
                        lastResponseItemCount = instruments.Data.Count;
                        for (int i = 0; i < instruments.Data.Count; i++)
                        {
                            instrumentsIndexesAndTicks.Add(instruments.Data[i].Symbol, new BitmexInstrumentIndexWithTick(ind++, instruments.Data[i].TickSize));
                        }
                    }
                    else
                    {
                        log.Write(LogLevel.Error, "Instrument indicies and price ticks for calculation was not obtained");
                        instumentGetWaiter.Release();
                        return;
                    }
                }
                while (lastResponseItemCount == getByOnce);
            }
            areInstrumentsLoaded = true;
            instumentGetWaiter.Release();
        }

//Should be removed after refactoring
        private CallResult<T> Deserialize<T>(JToken token)
        {
            return socketClient.DeserializeInternal<T>(token);
        }

        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
        {
            return new BitmexAuthenticationProvider(credentials);
        }  
    }
}
