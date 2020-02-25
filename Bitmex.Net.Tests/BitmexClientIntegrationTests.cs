using Bitmex.Net.Client.Objects.Requests;
using System;
using System.Text.Json;
using Xunit;
using Bitmex.Net.Client.Helpers;
using System.Linq;
using Bitmex.Net.Client.Interfaces;
using Bitmex.Net.Client.Objects;
using Newtonsoft.Json;
using Bitmex.Net.Client.Converters;
using Bitmex.Net.Client.Attributes;

namespace Bitmex.Net.Client.Tests
{
    public class BitmexClientIntegrationTests
    {
        BitmexClient _client = new BitmexClient(new BitmexClientOptions());
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
            var t = _client.PlaceOrder(new PlaceOrderRequest("XBTUSD") { BitmexOrderType = BitmexOrderType.Limit, Price = 22000, Side = BitmexOrderSide.Sell, Quantity = 10 });

            var t2 = _client.PlaceOrder(new PlaceOrderRequest("XBTUSD") { BitmexOrderType = BitmexOrderType.Limit, Price = 27000, Side = BitmexOrderSide.Sell, Quantity = 10 });
            //   var update = _client.UpdateOrder(new UpdateOrderRequest(42000,t2.Data.OrderID));
            var c = _client.CancelOrder(new CancelOrderRequest(new string[] { t2.Data.OrderID, t.Data.OrderID }));

            Assert.True(c);
        }        
    }
}
