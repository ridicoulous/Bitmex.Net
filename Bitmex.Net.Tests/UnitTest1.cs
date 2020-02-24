
using Bitmex.Net.Objects.Requests;
using System;
using System.Text.Json;
using Xunit;
using Bitmex.Net.Helpers;
using System.Linq;

namespace Bitmex.Net.Objects.Tests
{
    public class UnitTest1
    {
        BitmexClient _client = new BitmexClient(new BitmexClientOptions("", "",true));
        [Fact]
        public void ShouldReturnFourLastBuyTrades()
        {
            var t = _client.GetTrades();
            var trades = _client.GetTrades(new BitmexRequestWithFilter()
                .WithSymbolFilter("XBTUSD")
                .WithSideFilter(BitmexOrderSide.Buy)
                .WithExactDateFilter(new DateTime(2019, 1, 1))
                .SetResultsCount(4));
            Assert.True(trades);
            Assert.True(trades.Data.Count == 4);
            Assert.True(trades.Data.All(c => c.Side == BitmexOrderSide.Buy && c.Symbol == "XBTUSD"));
        }
        [Fact]
        public void ShouldPlaceOrder()
        {
            var t = _client.PlaceOrder(new PlaceOrderRequest("XBTUSD") { BitmexOrderType = BitmexOrderType.Limit, Price = 25000, Side = BitmexOrderSide.Sell, Quantity = 10 });
            Assert.True(t);
            var cancel = _client.CancelAllOrders();
            Assert.True(cancel);
        }
    }
}
