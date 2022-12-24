using System;
using CryptoExchange.Net.CommonObjects;
using Newtonsoft.Json;

namespace    Bitmex.Net.Client.Objects
{

    /// <summary>Summary of Open and Closed Positions</summary>
    public class BitmexPosition
    {
        [JsonProperty("account", Required = Required.Always)]
        public long Account { get; set; }

        [JsonProperty("symbol", Required = Required.Always)]

        public string Symbol { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("underlying")]
        public string Underlying { get; set; }

        [JsonProperty("quoteCurrency")]
        public string QuoteCurrency { get; set; }

        [JsonProperty("commission")]
        public decimal? Commission { get; set; }

        [JsonProperty("initMarginReq")]
        public decimal? InitMarginReq { get; set; }

        [JsonProperty("maintMarginReq")]
        public decimal? MaintMarginReq { get; set; }

        [JsonProperty("riskLimit")]
        public decimal? RiskLimit { get; set; }

        [JsonProperty("leverage")]
        public decimal? Leverage { get; set; }

        [JsonProperty("crossMargin")]
        public bool? CrossMargin { get; set; }

        [JsonProperty("deleveragePercentile")]
        public decimal? DeleveragePercentile { get; set; }

        [JsonProperty("rebalancedPnl")]
        public decimal? RebalancedPnl { get; set; }

        [JsonProperty("prevRealisedPnl")]
        public decimal? PrevRealisedPnl { get; set; }

        [JsonProperty("prevUnrealisedPnl")]
        public decimal? PrevUnrealisedPnl { get; set; }

        [JsonProperty("openingQty")]
        public decimal? OpeningQty { get; set; }

        [JsonProperty("openOrderBuyQty")]
        public decimal? OpenOrderBuyQty { get; set; }

        [JsonProperty("openOrderBuyCost")]
        public decimal? OpenOrderBuyCost { get; set; }

        [JsonProperty("openOrderBuyPremium")]
        public decimal? OpenOrderBuyPremium { get; set; }

        [JsonProperty("openOrderSellQty")]
        public decimal? OpenOrderSellQty { get; set; }

        [JsonProperty("openOrderSellCost")]
        public decimal? OpenOrderSellCost { get; set; }

        [JsonProperty("openOrderSellPremium")]
        public decimal? OpenOrderSellPremium { get; set; }

        [JsonProperty("currentQty")]
        public decimal? CurrentQty { get; set; }

        [JsonProperty("currentCost")]
        public decimal? CurrentCost { get; set; }

        [JsonProperty("currentComm")]
        public decimal? CurrentComm { get; set; }

        [JsonProperty("realisedCost")]
        public decimal? RealisedCost { get; set; }

        [JsonProperty("unrealisedCost")]
        public decimal? UnrealisedCost { get; set; }

        [JsonProperty("grossOpenPremium")]
        public decimal? GrossOpenPremium { get; set; }

        [JsonProperty("isOpen")]
        public bool? IsOpen { get; set; }

        [JsonProperty("markPrice")]
        public decimal? MarkPrice { get; set; }

        [JsonProperty("markValue")]
        public decimal? MarkValue { get; set; }

        [JsonProperty("riskValue")]
        public decimal? RiskValue { get; set; }

        [JsonProperty("homeNotional")]
        public decimal? HomeNotional { get; set; }

        [JsonProperty("foreignNotional")]
        public decimal? ForeignNotional { get; set; }

        [JsonProperty("posState")]
        public string PosState { get; set; }

        [JsonProperty("posCost")]
        public decimal? PosCost { get; set; }

        [JsonProperty("posCross")]
        public decimal? PosCross { get; set; }

        [JsonProperty("posComm")]
        public decimal? PosComm { get; set; }

        [JsonProperty("posLoss")]
        public decimal? PosLoss { get; set; }

        [JsonProperty("posMargin")]
        public decimal? PosMargin { get; set; }

        [JsonProperty("posMaint")]
        public decimal? PosMaint { get; set; }

        [JsonProperty("initMargin")]
        public decimal? InitMargin { get; set; }

        [JsonProperty("maintMargin")]
        public decimal? MaintMargin { get; set; }

        [JsonProperty("realisedPnl")]
        public decimal? RealisedPnlAfterRebalancing { get; set; }

        [JsonProperty("unrealisedPnl")]
        public decimal? UnrealisedPnl { get; set; }

        [JsonProperty("unrealisedPnlPcnt")]
        public decimal? UnrealisedPnlPcnt { get; set; }

        [JsonProperty("unrealisedRoePcnt")]
        public decimal? UnrealisedRoePcnt { get; set; }

        [JsonProperty("avgEntryPrice")]
        public decimal? AvgEntryPrice { get; set; }

        [JsonProperty("breakEvenPrice")]
        public decimal? BreakEvenPrice { get; set; }

        [JsonProperty("marginCallPrice")]
        public decimal? MarginCallPrice { get; set; }

        [JsonProperty("liquidationPrice")]
        public decimal? LiquidationPrice { get; set; }

        [JsonProperty("bankruptPrice")]
        public decimal? BankruptPrice { get; set; }

        [JsonProperty("timestamp")]
        public System.DateTime? Timestamp { get; set; }

        /// <summary>
        /// for closed positions it equal to PrevRealisedPnl, for closed ones it equal to RealisedPnlAfterRebalancing + RebalancedPnl
        /// in a case of socket update use with cautions: some fields required for calculation may be null
        /// </summary>
        public decimal? RealisedPnl => IsOpen == false? PrevRealisedPnl : RealisedPnlAfterRebalancing + RebalancedPnl;
        internal Position ToCryptoExchangePosition()
        {
            return new Position()
            {
                SourceObject = this,
                EntryPrice = this.AvgEntryPrice,
                Isolated = !this.CrossMargin,
                Leverage = this.Leverage.GetValueOrDefault(),
                LiquidationPrice = this.LiquidationPrice,
                MaintananceMargin = this.MaintMargin,
                MarkPrice = this.MarkPrice,
                PositionMargin = this.PosMargin,
                Quantity = Math.Abs(this.CurrentQty.GetValueOrDefault()),
                RealizedPnl = RealisedPnl,
                Side = this.CurrentQty switch
                {
                    < 0 => CommonPositionSide.Short,
                    > 0 => CommonPositionSide.Long,
                    _ => null
                },
                Symbol = this.Symbol,
                UnrealizedPnl = this.UnrealisedPnl
            };
        }
    }

}

