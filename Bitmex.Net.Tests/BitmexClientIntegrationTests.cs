using Bitmex.Net.Client.Objects.Requests;
using System;
using System.Text.Json;
using Xunit;
using Bitmex.Net.Client.Helpers.Extensions;
using System.Linq;
using Bitmex.Net.Client.Interfaces;
using Bitmex.Net.Client.Objects;
using Newtonsoft.Json;
using Bitmex.Net.Client.Converters;
using Bitmex.Net.Client.Attributes;
using Newtonsoft.Json.Linq;

namespace Bitmex.Net.Client.Tests
{
    public class BitmexClientIntegrationTests
    {
        BitmexClient _client = new BitmexClient(new BitmexClientOptions("42", "42", true));
        [Fact]
        public void ShouldReturnFourLastBuyTrades()
        {
            var t = _client.GetTrades();
            var trades = _client.GetTrades(new BitmexRequestWithFilter()
                .WithSymbolFilter("XBTUSD")
                .WithSideFilter(BitmexOrderSide.Buy)
                .WithExactDateFilter(new DateTime(2019, 1, 1))
                .WithResultsCount(4));
            Assert.True(trades);
            Assert.True(trades.Data.Count == 4);
            Assert.True(trades.Data.All(c => c.Side == BitmexOrderSide.Buy && c.Symbol == "XBTUSD"));
        }
        //[Fact]
        //public void ShouldPlaceOrder()
        //{
        //    var t = _client.PlaceOrder(new PlaceOrderRequest("XBTUSD") { BitmexOrderType = BitmexOrderType.Limit, Price = 22000, Side = BitmexOrderSide.Sell, Quantity = 10 });
        //    var t2 = _client.PlaceOrder(new PlaceOrderRequest("XBTUSD") { BitmexOrderType = BitmexOrderType.Limit, Price = 27000, Side = BitmexOrderSide.Sell, Quantity = 10 });
        //    Assert.True(t);
        //    var update = _client.UpdateOrder(new UpdateOrderRequest(42000,t2.Data.Id));
        //    Assert.True(update);

        //    var c = _client.CancelOrder(new CancelOrderRequest(new string[] { t2.Data.Id, t.Data.Id }));

        //    Assert.True(c);
        //}        
        [Fact]
        public void ShouldCreateDictionaryFromObject()
        {
            var o = new PlaceOrderRequest("XBTUSD")
            {
                BitmexOrderType = BitmexOrderType.Limit,
                Price = 42,
                Quantity = 10,
                Side = BitmexOrderSide.Buy
            };
            var dic = o.AsDictionary();
            Assert.Equal(5, dic.Count);
        }
        [Fact]
        public void ShouldParseBitmexError() 
        {
            string error = "{ \"error\": { \"message\": \"Error message\", \"name\": \"Error name\" } }";
            JToken token = JToken.Parse(error);
            var e = token["error"];
            Assert.NotNull(e);
            Assert.Equal("Error message", e["message"]);
        }
        [Fact]
        public void ShoulThrowError()
        {
            var order = _client.PlaceOrder(new PlaceOrderRequest("745242") { BitmexOrderType = BitmexOrderType.Limit, Price = 22000, Side = BitmexOrderSide.Sell, Quantity = 10 });
            Assert.False(order);
            Assert.NotNull(order.Error);
        }
    }
}
