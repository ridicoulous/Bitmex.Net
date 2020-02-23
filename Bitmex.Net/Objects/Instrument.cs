using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{
  
        /// <summary>Tradeable Contracts, Indices, and History</summary>

        public class Instrument
    {
        [JsonProperty("symbol", Required = Required.Always)]
      
        public string Symbol { get; set; }

        [JsonProperty("rootSymbol", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string RootSymbol { get; set; }

        [JsonProperty("state", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }

        [JsonProperty("typ", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Typ { get; set; }

        [JsonProperty("listing", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? Listing { get; set; }

        [JsonProperty("front", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? Front { get; set; }

        [JsonProperty("expiry", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? Expiry { get; set; }

        [JsonProperty("settle", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? Settle { get; set; }

        [JsonProperty("relistInterval", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? RelistInterval { get; set; }

        [JsonProperty("inverseLeg", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string InverseLeg { get; set; }

        [JsonProperty("sellLeg", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string SellLeg { get; set; }

        [JsonProperty("buyLeg", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string BuyLeg { get; set; }

        [JsonProperty("optionStrikePcnt", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? OptionStrikePcnt { get; set; }

        [JsonProperty("optionStrikeRound", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? OptionStrikeRound { get; set; }

        [JsonProperty("optionStrikePrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? OptionStrikePrice { get; set; }

        [JsonProperty("optionMultiplier", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? OptionMultiplier { get; set; }

        [JsonProperty("positionCurrency", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string PositionCurrency { get; set; }

        [JsonProperty("underlying", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Underlying { get; set; }

        [JsonProperty("quoteCurrency", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string QuoteCurrency { get; set; }

        [JsonProperty("underlyingSymbol", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string UnderlyingSymbol { get; set; }

        [JsonProperty("reference", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Reference { get; set; }

        [JsonProperty("referenceSymbol", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string ReferenceSymbol { get; set; }

        [JsonProperty("calcInterval", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? CalcInterval { get; set; }

        [JsonProperty("publishInterval", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? PublishInterval { get; set; }

        [JsonProperty("publishTime", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? PublishTime { get; set; }

        [JsonProperty("maxOrderQty", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? MaxOrderQty { get; set; }

        [JsonProperty("maxPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? MaxPrice { get; set; }

        [JsonProperty("lotSize", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? LotSize { get; set; }

        [JsonProperty("tickSize", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? TickSize { get; set; }

        [JsonProperty("multiplier", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? Multiplier { get; set; }

        [JsonProperty("settlCurrency", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string SettlCurrency { get; set; }

        [JsonProperty("underlyingToPositionMultiplier", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? UnderlyingToPositionMultiplier { get; set; }

        [JsonProperty("underlyingToSettleMultiplier", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? UnderlyingToSettleMultiplier { get; set; }

        [JsonProperty("quoteToSettleMultiplier", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? QuoteToSettleMultiplier { get; set; }

        [JsonProperty("isQuanto", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsQuanto { get; set; }

        [JsonProperty("isInverse", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsInverse { get; set; }

        [JsonProperty("initMargin", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? InitMargin { get; set; }

        [JsonProperty("maintMargin", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? MaintMargin { get; set; }

        [JsonProperty("riskLimit", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? RiskLimit { get; set; }

        [JsonProperty("riskStep", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? RiskStep { get; set; }

        [JsonProperty("limit", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? Limit { get; set; }

        [JsonProperty("capped", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Capped { get; set; }

        [JsonProperty("taxed", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Taxed { get; set; }

        [JsonProperty("deleverage", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Deleverage { get; set; }

        [JsonProperty("makerFee", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? MakerFee { get; set; }

        [JsonProperty("takerFee", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? TakerFee { get; set; }

        [JsonProperty("settlementFee", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? SettlementFee { get; set; }

        [JsonProperty("insuranceFee", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? InsuranceFee { get; set; }

        [JsonProperty("fundingBaseSymbol", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string FundingBaseSymbol { get; set; }

        [JsonProperty("fundingQuoteSymbol", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string FundingQuoteSymbol { get; set; }

        [JsonProperty("fundingPremiumSymbol", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string FundingPremiumSymbol { get; set; }

        [JsonProperty("fundingTimestamp", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? FundingTimestamp { get; set; }

        [JsonProperty("fundingInterval", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? FundingInterval { get; set; }

        [JsonProperty("fundingRate", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? FundingRate { get; set; }

        [JsonProperty("indicativeFundingRate", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? IndicativeFundingRate { get; set; }

        [JsonProperty("rebalanceTimestamp", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? RebalanceTimestamp { get; set; }

        [JsonProperty("rebalanceInterval", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? RebalanceInterval { get; set; }

        [JsonProperty("openingTimestamp", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? OpeningTimestamp { get; set; }

        [JsonProperty("closingTimestamp", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? ClosingTimestamp { get; set; }

        [JsonProperty("sessionInterval", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? SessionInterval { get; set; }

        [JsonProperty("prevClosePrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? PrevClosePrice { get; set; }

        [JsonProperty("limitDownPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? LimitDownPrice { get; set; }

        [JsonProperty("limitUpPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? LimitUpPrice { get; set; }

        [JsonProperty("bankruptLimitDownPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? BankruptLimitDownPrice { get; set; }

        [JsonProperty("bankruptLimitUpPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? BankruptLimitUpPrice { get; set; }

        [JsonProperty("prevTotalVolume", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? PrevTotalVolume { get; set; }

        [JsonProperty("totalVolume", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? TotalVolume { get; set; }

        [JsonProperty("volume", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? Volume { get; set; }

        [JsonProperty("volume24h", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? Volume24h { get; set; }

        [JsonProperty("prevTotalTurnover", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? PrevTotalTurnover { get; set; }

        [JsonProperty("totalTurnover", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? TotalTurnover { get; set; }

        [JsonProperty("turnover", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? Turnover { get; set; }

        [JsonProperty("turnover24h", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? Turnover24h { get; set; }

        [JsonProperty("homeNotional24h", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? HomeNotional24h { get; set; }

        [JsonProperty("foreignNotional24h", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? ForeignNotional24h { get; set; }

        [JsonProperty("prevPrice24h", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? PrevPrice24h { get; set; }

        [JsonProperty("vwap", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? Vwap { get; set; }

        [JsonProperty("highPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? HighPrice { get; set; }

        [JsonProperty("lowPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? LowPrice { get; set; }

        [JsonProperty("lastPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? LastPrice { get; set; }

        [JsonProperty("lastPriceProtected", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? LastPriceProtected { get; set; }

        [JsonProperty("lastTickDirection", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string LastTickDirection { get; set; }

        [JsonProperty("lastChangePcnt", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? LastChangePcnt { get; set; }

        [JsonProperty("bidPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? BidPrice { get; set; }

        [JsonProperty("midPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? MidPrice { get; set; }

        [JsonProperty("askPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? AskPrice { get; set; }

        [JsonProperty("impactBidPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? ImpactBidPrice { get; set; }

        [JsonProperty("impactMidPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? ImpactMidPrice { get; set; }

        [JsonProperty("impactAskPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? ImpactAskPrice { get; set; }

        [JsonProperty("hasLiquidity", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? HasLiquidity { get; set; }

        [JsonProperty("openInterest", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? OpenInterest { get; set; }

        [JsonProperty("openValue", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? OpenValue { get; set; }

        [JsonProperty("fairMethod", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string FairMethod { get; set; }

        [JsonProperty("fairBasisRate", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? FairBasisRate { get; set; }

        [JsonProperty("fairBasis", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? FairBasis { get; set; }

        [JsonProperty("fairPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? FairPrice { get; set; }

        [JsonProperty("markMethod", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string MarkMethod { get; set; }

        [JsonProperty("markPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? MarkPrice { get; set; }

        [JsonProperty("indicativeTaxRate", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? IndicativeTaxRate { get; set; }

        [JsonProperty("indicativeSettlePrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? IndicativeSettlePrice { get; set; }

        [JsonProperty("optionUnderlyingPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? OptionUnderlyingPrice { get; set; }

        [JsonProperty("settledPrice", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public double? SettledPrice { get; set; }

        [JsonProperty("timestamp", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? Timestamp { get; set; }


    }

}

