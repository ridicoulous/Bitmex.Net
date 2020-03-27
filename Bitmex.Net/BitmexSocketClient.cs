using Bitmex.Net.Client.Converters;
using Bitmex.Net.Client.Helpers.Extensions;
using Bitmex.Net.Client.Interfaces;
using Bitmex.Net.Client.Objects;
using Bitmex.Net.Client.Objects.Socket;
using Bitmex.Net.Client.Objects.Socket.Repsonses;
using Bitmex.Net.Client.Objects.Socket.Requests;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bitmex.Net.Client
{
    public class BitmexSocketClient : SocketClient, IBitmexSocketClient
    {
        private static BitmexSocketClientOptions defaultOptions = new BitmexSocketClientOptions();
        private static BitmexSocketClientOptions DefaultOptions => defaultOptions.Copy<BitmexSocketClientOptions>();
        public readonly Dictionary<string, BitmexInstrumentIndexWithTick> InstrumentsIndexesAndTicks = new Dictionary<string, BitmexInstrumentIndexWithTick>();
        private bool isTestnet;
        public BitmexSocketClient() : this(DefaultOptions)
        {
        }
        
        public BitmexSocketClient(BitmexSocketClientOptions bitmexSocketClientOptions) : base(bitmexSocketClientOptions, bitmexSocketClientOptions.ApiCredentials == null ? null : new BitmexAuthenticationProvider(bitmexSocketClientOptions.ApiCredentials))
        {
            //var rest = Query<GreetingsMessage>("ping", false).Result;
            
            //SendPeriodic(TimeSpan.FromSeconds(10), conn => "ping");
            if (bitmexSocketClientOptions.LoadInstruments)
            {
                using (var bitmexClient = new BitmexClient(new BitmexClientOptions(bitmexSocketClientOptions.IsTestnet)))
                {
                    var instruments = bitmexClient.GetInstruments(new Objects.Requests.BitmexRequestWithFilter().WithResultsCount(500).AddColumnsToGetInRequest(new string[] { "symbol", "tickSize" }));
                    if (instruments)
                    {
                        for (int i = 0; i < instruments.Data.Count; i++)
                        {
                            InstrumentsIndexesAndTicks.Add(instruments.Data[i].Symbol, new BitmexInstrumentIndexWithTick(i, instruments.Data[i].TickSize));
                        }
                    }
                    else
                    {
                        log.Write(LogVerbosity.Error, "Instrument indicies and price ticks for calculation was not obtained");
                    }
                }
            }

        }
        public event Action OnPongReceived;
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
        public event Action<BitmexSocketEvent<Trade>> OnTradeUpdate;
        public event Action<BitmexSocketEvent<TradeBin>> OnOneMinuteTradeBinUpdate;
        public event Action<BitmexSocketEvent<TradeBin>> OnFiveMinuteTradeBinUpdate;
        public event Action<BitmexSocketEvent<TradeBin>> OnOneHourTradeBinUpdate;
        public event Action<BitmexSocketEvent<TradeBin>> OnDailyTradeBinUpdate;
        public event Action<BitmexSocketEvent<Affiliate>> OnUserAffiliatesUpdate;
        public event Action<BitmexSocketEvent<Execution>> OnUserExecutionsUpdate;
        public event Action<BitmexSocketEvent<Order>> OnUserOrdersUpdate;
        public event Action<BitmexSocketEvent<Margin>> OnUserMarginUpdate;
        public event Action<BitmexSocketEvent<Position>> OnUserPositionsUpdate;
        public event Action<BitmexSocketEvent<Transaction>> OnUserTransactionsUpdate;
        public event Action<BitmexSocketEvent<Wallet>> OnUserWalletUpdate;

        protected override IWebsocket CreateSocket(string address)
        {
            Dictionary<string, string> empty = new Dictionary<string, string>();
            var s = SocketFactory.CreateWebsocket(this.log, address, empty, 
                this.authProvider == null ? empty : this.authProvider.AddAuthenticationToHeaders("bitmex.com/realtime", HttpMethod.Get, null, true));
            s.Origin = $"https://{(isTestnet?"testnet":"www")}.bitmex.com";            
            return s;
        }
        protected override async Task<CallResult<bool>> AuthenticateSocket(SocketConnection s)
        {
            if (authProvider == null)
            {
                return new CallResult<bool>(false, new ServerError("Need to create auth provider"));
            }
            return new CallResult<bool>(true, null);
        }

        protected override async Task<bool> Unsubscribe(SocketConnection connection, SocketSubscription s)
        {            
            if(s.Request is BitmexSubscribeRequest)
            {
                var unsub = s.Request as BitmexSubscribeRequest;
                unsub.Op = BitmexWebSocketOperation.Unsubscribe;
                var handler = new Action<string>(data=> 
                {
                    log.Write(LogVerbosity.Debug, data);
                });
                await Subscribe(unsub, handler);
            }
            return true;
        }

        protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
        {
            callResult = new CallResult<T>(data.ToObject<T>(), null);
            return callResult;
        }

        protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object> callResult)
        {
            callResult = null;

            if (message.Type == JTokenType.String && (string)message == "pong")
            {
                //callResult = new CallResult<object>(, null);
                return true;
            }
           
            // var greetings = message.ToObject<GreetingsMessage>();
            var response = Deserialize<BitmexSubscriptionResponse>(message, false);

            var bRequest = (BitmexSubscribeRequest)request;
            if (response.Success && response.Data.Success)
            {
                //if (response.Data.Request.Args.Contains(response.Data.Subscribe) || response.Data.Request.Args.Contains(response.Data.Unsubscribe))
                //{
                //    callResult = new CallResult<object>(response, response.Success ? null : new ServerError("Subscribtion was not success", response));

                //    return true;
                //}
                if (bRequest.Args.Contains(response.Data.Subscribe))
                {
                    callResult = new CallResult<object>(response, response.Success ? null : new ServerError("Subscribtion was not success", response));

                    return true;
                }

            }
            if (message.Type != JTokenType.Object)
                return false;

            if (!response || !response.Data.Success || bRequest.Args.All(c => c.ToString() != response.Data.Subscribe) || String.IsNullOrEmpty(response.Data.Subscribe) || String.IsNullOrEmpty(response.Data.Unsubscribe))
            {
                return false;
            }

            callResult = new CallResult<object>(response, response.Success ? null : new ServerError("Subscribtion was not success", response));
            return true;
        }

        protected override bool MessageMatchesHandler(JToken message, object request)
        {
            return true;
        }

        protected override bool MessageMatchesHandler(JToken message, string identifier)
        {
            return true;
        }
        public CallResult<UpdateSubscription> SubscribeToOrderBookUpdates(Action<BitmexSocketEvent<BitmexOrderBookEntry>> onData, string symbol = "", bool full = false) => SubscribeToOrderBookUpdatesAsync(onData, symbol, full).Result;

        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(Action<BitmexSocketEvent<BitmexOrderBookEntry>> onData, string symbol = "", bool full = false)
        {
            BitmexSubscribtions orderbookType = full ? BitmexSubscribtions.OrderBookL2 : BitmexSubscribtions.OrderBookL2_25;
            return await Subscribe(new BitmexSubscribeRequest().AddSubscription(orderbookType, symbol), onData);
        }

        public CallResult<UpdateSubscription> Subscribe(BitmexSubscribeRequest bitmexSubscribeRequest) => SubscribeAsync(bitmexSubscribeRequest).Result;

        ResponseTableToDataTypeMapping Map = new ResponseTableToDataTypeMapping();
        public async Task<CallResult<UpdateSubscription>> SubscribeAsync(BitmexSubscribeRequest bitmexSubscribeRequest)
        {
            var handler = new Action<string>(data =>
            {
                if (data == "pong")
                {
                    OnPongReceived?.Invoke();
                    return;
                }

                var token = JToken.Parse(data);
                var table = (string)token["table"];
                if (String.IsNullOrEmpty(table) || !Map.Mappings.ContainsKey(table))
                {
                    log.Write(LogVerbosity.Warning, $"Unknown table [{table}] update catched at data {data}");
                    return;
                }
                BitmexSubscribtions updatedTable = Map.Mappings[table];

                switch (updatedTable)
                {
                    case BitmexSubscribtions.Announcements:
                        {
                            var result = Deserialize<BitmexSocketEvent<Announcement>>(token, false);
                            if (result.Success)
                                OnAnnouncementUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Chat:
                        {
                            var result = Deserialize<BitmexSocketEvent<Chat>>(token, false);
                            if (result.Success)
                                OnChatMessageUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Connected:
                        {
                            var result = Deserialize<BitmexSocketEvent<ConnectedUsers>>(token, false);
                            if (result.Success)
                                OnChatConnectionUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Funding:
                        {
                            var result = Deserialize<BitmexSocketEvent<Funding>>(token, false);
                            if (result.Success)
                                OnFundingUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Instrument:
                        {
                            var result = Deserialize<BitmexSocketEvent<Instrument>>(token, false);
                            if (result.Success)
                                OnInstrimentUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Insurance:
                        {
                            var result = Deserialize<BitmexSocketEvent<Insurance>>(token, false);
                            if (result.Success)
                                OnInsuranceUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Liquidation:
                        {
                            var result = Deserialize<BitmexSocketEvent<Liquidation>>(token, false);
                            if (result.Success)
                                OnLiquidationUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.OrderBookL2_25:
                        {
                            var result = Deserialize<BitmexSocketEvent<BitmexOrderBookEntry>>(token, false);
                            if (result.Success)
                            {
                                if (InstrumentsIndexesAndTicks.Any())
                                {
                                    //  result.Data.Data.ForEach(c => c.SetPrice(InstrumentIndicies[c.Symbol].Index, InstrumentIndicies[c.Symbol].TickSize));
                                    foreach (var level in result.Data.Data)
                                    {
                                        var symbolTickInfo = InstrumentsIndexesAndTicks[level.Symbol];
                                        level.SetPrice(symbolTickInfo.Index, symbolTickInfo.TickSize);
                                    }
                                }
                                OnOrderBookL2_25Update?.Invoke(result.Data);
                            }
                            else
                                log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.OrderBookL2:
                        {
                            var result = Deserialize<BitmexSocketEvent<BitmexOrderBookEntry>>(token, false);
                            if (result.Success)
                            {
                                if (InstrumentsIndexesAndTicks.Any())
                                {
                                    foreach (var level in result.Data.Data)
                                    {
                                        var symbolTickInfo = InstrumentsIndexesAndTicks[level.Symbol];
                                        level.SetPrice(symbolTickInfo.Index, symbolTickInfo.TickSize);
                                    }
                                }
                                OnorderBookL2Update?.Invoke(result.Data);
                            }
                            else
                                log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.OrderBook10:
                        {
                            var result = Deserialize<BitmexSocketEvent<BitmexOrderBookL10>>(token, false);
                            if (result.Success)
                                OnOrderBook10Update?.Invoke(result.Data);
                            else
                                log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.PublicNotifications:
                        {
                            var result = Deserialize<BitmexSocketEvent<GlobalNotification>>(token, false);
                            if (result.Success)
                                OnGlobalNotificationUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Quote:
                        {
                            var result = Deserialize<BitmexSocketEvent<Quote>>(token, false);
                            if (result.Success)
                                OnQuotesUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.QuoteBin1m:
                        {
                            var result = Deserialize<BitmexSocketEvent<Quote>>(token, false);
                            if (result.Success)
                                OnOneMinuteQuoteBinUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.QuoteBin5m:
                        {
                            var result = Deserialize<BitmexSocketEvent<Quote>>(token, false);
                            if (result.Success)
                                OnFiveMinuteQuoteBinUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.QuoteBin1h:
                        {
                            var result = Deserialize<BitmexSocketEvent<Quote>>(token, false);
                            if (result.Success)
                                OnOneHourQuoteBinUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.QuoteBin1d:
                        {
                            var result = Deserialize<BitmexSocketEvent<Quote>>(token, false);
                            if (result.Success)
                                OnDailyQuoteBinUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Settlement:
                        {
                            var result = Deserialize<BitmexSocketEvent<Settlement>>(token, false);
                            if (result.Success)
                                OnSettlementUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Trade:
                        {
                            var result = Deserialize<BitmexSocketEvent<Trade>>(token, false);
                            if (result.Success)
                                OnTradeUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.TradeBin1m:
                        {
                            var result = Deserialize<BitmexSocketEvent<TradeBin>>(token, false);
                            if (result.Success)
                                OnOneMinuteTradeBinUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.TradeBin5m:
                        {
                            var result = Deserialize<BitmexSocketEvent<TradeBin>>(token, false);
                            if (result.Success)
                                OnFiveMinuteTradeBinUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.TradeBin1h:
                        {
                            var result = Deserialize<BitmexSocketEvent<TradeBin>>(token, false);
                            if (result.Success)
                                OnOneHourTradeBinUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.TradeBin1d:
                        {
                            var result = Deserialize<BitmexSocketEvent<TradeBin>>(token, false);
                            if (result.Success)
                                OnDailyTradeBinUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Affiliate:
                        {
                            var result = Deserialize<BitmexSocketEvent<Affiliate>>(token, false);
                            if (result.Success)
                                OnUserAffiliatesUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Execution:
                        {
                            var result = Deserialize<BitmexSocketEvent<Execution>>(token, false);
                            if (result.Success)
                                OnUserExecutionsUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Order:
                        {
                            var result = Deserialize<BitmexSocketEvent<Order>>(token, false);
                            if (result.Success)
                                OnUserOrdersUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Margin:
                        {
                            var result = Deserialize<BitmexSocketEvent<Margin>>(token, false);
                            if (result.Success)
                                OnUserMarginUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Position:
                        {
                            var result = Deserialize<BitmexSocketEvent<Position>>(token, false);
                            if (result.Success)
                                OnUserPositionsUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Transact:
                        {
                            var result = Deserialize<BitmexSocketEvent<Transaction>>(token, false);
                            if (result.Success)
                                OnUserTransactionsUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    case BitmexSubscribtions.Wallet:
                        {
                            var result = Deserialize<BitmexSocketEvent<Wallet>>(token, false);
                            if (result.Success)
                                OnUserWalletUpdate?.Invoke(result.Data);
                            else
                                log.Write(LogVerbosity.Warning, "Couldn't deserialize data received from  stream: " + result.Error);
                            break;
                        }
                    default:
                        {
                            log.Write(LogVerbosity.Warning, $"Catched inknown table update: {data}");
                            break;
                        }
                }
            });
            return await Subscribe(bitmexSubscribeRequest, handler);
        }
        private async Task<CallResult<UpdateSubscription>> Subscribe<T>(BitmexSubscribeRequest request, Action<T> onData)
        {
            request.Args.ValidateNotNull(nameof(request));
            var url = BaseAddress + $"?{request.Op.ToString().ToLower()}={String.Join(",", request.Args)}";
            return await Subscribe(url, request, url + NextId(), authProvider != null, onData).ConfigureAwait(false);
        }
        public void Ping()
        {
            PingAsync().Wait();
        }
        public async Task PingAsync()
        {
            await Query<string>("ping", false);
        }

    }
}
