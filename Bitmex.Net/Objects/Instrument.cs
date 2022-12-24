using CryptoExchange.Net.CommonObjects;
using Newtonsoft.Json;

namespace Bitmex.Net.Client.Objects
{

    /// <summary>Tradeable Contracts, Indices, and History</summary>

    public class Instrument
    {
        [JsonProperty("symbol", Required = Required.Always)]
      
        public string Symbol { get; set; }

        [JsonProperty("rootSymbol")]
        public string RootSymbol { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("typ")]
        public string Typ { get; set; }

        [JsonProperty("listing")]
        public System.DateTime? Listing { get; set; }

        [JsonProperty("front")]
        public System.DateTime? Front { get; set; }

        [JsonProperty("expiry")]
        public System.DateTime? Expiry { get; set; }

        [JsonProperty("settle")]
        public System.DateTime? Settle { get; set; }

        [JsonProperty("relistInterval")]
        public System.DateTime? RelistInterval { get; set; }

        [JsonProperty("positionCurrency")]
        public string PositionCurrency { get; set; }

        [JsonProperty("underlying")]
        public string Underlying { get; set; }

        [JsonProperty("quoteCurrency")]
        public string QuoteCurrency { get; set; }

        [JsonProperty("underlyingSymbol")]
        public string UnderlyingSymbol { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("referenceSymbol")]
        public string ReferenceSymbol { get; set; }

        [JsonProperty("calcInterval")]
        public System.DateTime? CalcInterval { get; set; }

        [JsonProperty("publishInterval")]
        public System.DateTime? PublishInterval { get; set; }

        [JsonProperty("publishTime")]
        public System.DateTime? PublishTime { get; set; }

        [JsonProperty("maxOrderQty")]
        public decimal? MaxOrderQty { get; set; }

        [JsonProperty("maxPrice")]
        public decimal? MaxPrice { get; set; }

        [JsonProperty("lotSize")]
        public decimal? LotSize { get; set; }

        [JsonProperty("tickSize")]
        public decimal? TickSize { get; set; }

        [JsonProperty("multiplier")]
        public decimal? Multiplier { get; set; }

        [JsonProperty("settlCurrency")]
        public string SettlCurrency { get; set; }

        [JsonProperty("underlyingToPositionMultiplier")]
        public decimal? UnderlyingToPositionMultiplier { get; set; }

        [JsonProperty("underlyingToSettleMultiplier")]
        public decimal? UnderlyingToSettleMultiplier { get; set; }

        [JsonProperty("quoteToSettleMultiplier")]
        public decimal? QuoteToSettleMultiplier { get; set; }

        [JsonProperty("isQuanto")]
        public bool? IsQuanto { get; set; }

        [JsonProperty("isInverse")]
        public bool? IsInverse { get; set; }

        [JsonProperty("initMargin")]
        public decimal? InitMargin { get; set; }

        [JsonProperty("maintMargin")]
        public decimal? MaintMargin { get; set; }

        [JsonProperty("riskLimit")]
        public decimal? RiskLimit { get; set; }

        [JsonProperty("riskStep")]
        public decimal? RiskStep { get; set; }

        [JsonProperty("limit")]
        public decimal? Limit { get; set; }

        [JsonProperty("taxed")]
        public bool? Taxed { get; set; }

        [JsonProperty("deleverage")]
        public bool? Deleverage { get; set; }

        [JsonProperty("makerFee")]
        public decimal? MakerFee { get; set; }

        [JsonProperty("takerFee")]
        public decimal? TakerFee { get; set; }

        [JsonProperty("settlementFee")]
        public decimal? SettlementFee { get; set; }

        [JsonProperty("fundingBaseSymbol")]
        public string FundingBaseSymbol { get; set; }

        [JsonProperty("fundingQuoteSymbol")]
        public string FundingQuoteSymbol { get; set; }

        [JsonProperty("fundingPremiumSymbol")]
        public string FundingPremiumSymbol { get; set; }

        [JsonProperty("fundingTimestamp")]
        public System.DateTime? FundingTimestamp { get; set; }

        [JsonProperty("fundingInterval")]
        public System.DateTime? FundingInterval { get; set; }

        [JsonProperty("fundingRate")]
        public decimal? FundingRate { get; set; }

        [JsonProperty("indicativeFundingRate")]
        public decimal? IndicativeFundingRate { get; set; }

        [JsonProperty("rebalanceTimestamp")]
        public System.DateTime? RebalanceTimestamp { get; set; }

        [JsonProperty("rebalanceInterval")]
        public System.DateTime? RebalanceInterval { get; set; }

        [JsonProperty("prevClosePrice")]
        public decimal? PrevClosePrice { get; set; }

        [JsonProperty("limitDownPrice")]
        public decimal? LimitDownPrice { get; set; }

        [JsonProperty("limitUpPrice")]
        public decimal? LimitUpPrice { get; set; }

        [JsonProperty("totalVolume")]
        public decimal? TotalVolume { get; set; }

        [JsonProperty("volume")]
        public decimal? Volume { get; set; }

        [JsonProperty("volume24h")]
        public decimal? Volume24h { get; set; }

        [JsonProperty("prevTotalTurnover")]
        public decimal? PrevTotalTurnover { get; set; }

        [JsonProperty("totalTurnover")]
        public decimal? TotalTurnover { get; set; }

        [JsonProperty("turnover")]
        public decimal? Turnover { get; set; }

        [JsonProperty("turnover24h")]
        public decimal? Turnover24h { get; set; }

        [JsonProperty("homeNotional24h")]
        public decimal? HomeNotional24h { get; set; }

        [JsonProperty("foreignNotional24h")]
        public decimal? ForeignNotional24h { get; set; }

        [JsonProperty("prevPrice24h")]
        public decimal? PrevPrice24h { get; set; }

        [JsonProperty("vwap")]
        public decimal? Vwap { get; set; }

        [JsonProperty("highPrice")]
        public decimal? HighPrice { get; set; }

        [JsonProperty("lowPrice")]
        public decimal? LowPrice { get; set; }

        [JsonProperty("lastPrice")]
        public decimal? LastPrice { get; set; }

        [JsonProperty("lastPriceProtected")]
        public decimal? LastPriceProtected { get; set; }

        [JsonProperty("lastTickDirection")]
        public string LastTickDirection { get; set; }

        [JsonProperty("lastChangePcnt")]
        public decimal? LastChangePcnt { get; set; }

        [JsonProperty("bidPrice")]
        public decimal? BidPrice { get; set; }

        [JsonProperty("midPrice")]
        public decimal? MidPrice { get; set; }

        [JsonProperty("askPrice")]
        public decimal? AskPrice { get; set; }

        [JsonProperty("impactBidPrice")]
        public decimal? ImpactBidPrice { get; set; }

        [JsonProperty("impactMidPrice")]
        public decimal? ImpactMidPrice { get; set; }

        [JsonProperty("impactAskPrice")]
        public decimal? ImpactAskPrice { get; set; }

        [JsonProperty("hasLiquidity")]
        public bool? HasLiquidity { get; set; }

        [JsonProperty("openInterest")]
        public decimal? OpenInterest { get; set; }

        [JsonProperty("openValue")]
        public decimal? OpenValue { get; set; }

        [JsonProperty("fairMethod")]
        public string FairMethod { get; set; }

        [JsonProperty("fairBasisRate")]
        public decimal? FairBasisRate { get; set; }

        [JsonProperty("fairBasis")]
        public decimal? FairBasis { get; set; }

        [JsonProperty("fairPrice")]
        public decimal? FairPrice { get; set; }

        [JsonProperty("markMethod")]
        public string MarkMethod { get; set; }

        [JsonProperty("markPrice")]
        public decimal? MarkPrice { get; set; }

        [JsonProperty("indicativeSettlePrice")]
        public decimal? IndicativeSettlePrice { get; set; }

        [JsonProperty("settledPrice")]
        public decimal? SettledPrice { get; set; }

        [JsonProperty("timestamp")]
        public System.DateTime? Timestamp { get; set; }

        internal Symbol ToCryptoExchangeSymbol()
        {
            return new Symbol()
            {
                SourceObject = this,
                Name = this.Symbol,
                MinTradeQuantity = this.LotSize.GetValueOrDefault(),
                PriceStep = this.TickSize,
                QuantityStep = this.LotSize.GetValueOrDefault(),
                PriceDecimals = TickSize > 0 ? ((int)System.Math.Ceiling(-1 * System.Math.Log10((double)TickSize))) : (int?) TickSize
        };
        }
        internal Ticker ToCryptoExchangeTicker()
        {
            return new Ticker()
            {
                SourceObject = this,
                Symbol = this.Symbol,
                HighPrice = this.HighPrice,
                LowPrice = this.LowPrice,
                LastPrice = this.LastPrice,
                Volume = this.Volume24h,
                Price24H = this.PrevPrice24h
            };
        }
    }

}

