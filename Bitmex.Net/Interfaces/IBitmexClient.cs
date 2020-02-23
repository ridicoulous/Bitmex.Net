using Bitmex.Net.Objects;
using Bitmex.Net.Objects.Requests;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bitmex.Net.Interfaces
{
    public interface IBitmexClient
    {
        /// <summary>
        /// Get site announcements
        /// </summary>
        /// <param name="columns">Array of column names to fetch. If omitted, will return all columns.</param>
        /// <returns></returns>
        WebCallResult<List<Announcement>> GetAnnouncements(List<string> columns = null);
        /// <summary>
        /// Get urgent (banner) announcements.
        /// </summary>
        /// <returns></returns>
        WebCallResult<List<Announcement>> GetUrgentAnnouncements();
        /// <summary>
        /// Get your API Keys
        /// </summary>
        /// <param name="reverse">If true, will sort results newest first.</param>
        /// <returns></returns>
        WebCallResult<List<APIKey>> GetApiKeys(bool reverse = false);
        /// <summary>
        /// Get chat messages
        /// </summary>
        /// <returns></returns>
        WebCallResult<List<Chat>> GetChatMessages(int channelId, BitmexRequestWithFilter filter);
        /// <summary>
        /// Send a chat message
        /// </summary>
        /// <param name="channelId">channel id</param>
        /// <param name="message">message</param>
        /// <returns></returns>
        WebCallResult<Chat> SendChatMessage(int channelId, string message);
        /// <summary>
        /// Get available channels
        /// </summary>
        /// <returns></returns>
        WebCallResult<List<ChatChannel>> GetChannels();
        /// <summary>
        /// Get connected users
        /// </summary>
        /// <returns></returns>
        WebCallResult<ConnectedUsers> GetConnectedUsers();
        /// <summary>
        /// Get all raw executions for your account
        /// This returns all raw transactions, which includes order opening and cancelation, and order status changes. It can be quite noisy. More focused information is available at /execution/tradeHistory.
        /// You may also use the filter param to target your query.Specify an array as a filter value, such as {"execType": ["Settlement", "Trade"]        ///        }
        /// to filter on multiple values.
        /// <see href="http://www.onixs.biz/fix-dictionary/5.0.SP2/msgType_8_8.html">See the  FIX Spec for explanations of these fields.</see>
        /// </summary>
        /// <param name="requestWithFilter"><see cref="BitmexRequestWithFilter"/></param>
        /// <returns></returns>
        WebCallResult<List<Execution>> GetExecutions(BitmexRequestWithFilter requestWithFilter);
        /// <summary>
        /// Get all balance-affecting executions. This includes each trade, insurance charge, and settlement.
        /// Also see <see cref="GetExecutions(BitmexRequestWithFilter)"/>
        /// </summary>
        /// <param name="requestWithFilter"></param>
        /// <returns></returns>
        WebCallResult<List<Execution>> GetExecutionsTradeHistory(BitmexRequestWithFilter requestWithFilter);
        /// <summary>
        /// Get funding history.
        /// </summary>
        /// <param name="requestWithFilter"></param>
        /// <returns></returns>
        WebCallResult<List<Funding>> GetFunding(BitmexRequestWithFilter requestWithFilter);
        /// <summary>
        /// Get your current GlobalNotifications.
        /// </summary>
        /// <returns></returns>
        WebCallResult<List<GlobalNotification>> GetGlobalNotifications();
        /// <summary>
        /// This returns all instruments and indices, including those that have settled or are unlisted. Use this endpoint if you want to query for individual instruments or use a complex filter. Use /instrument/active to return active instruments, or use a filter like {"state": "Open"}.
        /// </summary>
        /// <param name="requestWithFilter"></param>
        /// <returns></returns>
        WebCallResult<List<Instrument>> GetInstruments(BitmexRequestWithFilter requestWithFilter);
        /// <summary>
        /// Get all active instruments and instruments that have expired in <24hrs
        /// </summary>
        /// <returns></returns>
        WebCallResult<List<Instrument>> GetActiveInstruments();
        /// <summary>
        /// Helper method. Gets all active instruments and all indices. This is a join of the result of /indices and /active
        /// </summary>
        /// <returns></returns>
        WebCallResult<List<Instrument>> GetActiveInstrumentsAndIndicies();
        /// <summary>
        /// This endpoint is useful for determining which pairs are live. It returns two arrays of strings. The first is intervals, such as ["XBT:perpetual", "XBT:quarterly", "XBT:biquarterly", "ETH:quarterly", ...]. These identifiers are usable in any query's symbol param. The second array is the current resolution of these intervals. Results are mapped at the same index.
        /// </summary>
        /// <returns></returns>
        WebCallResult<InstrumentInterval> GetActiveIntervals();
        /// <summary>
        /// Composite indices are built from multiple external price sources.
        /// Use this endpoint to get the underlying prices of an index.For example, send a symbol of .XBT to get the ticks and weights of the constituent exchanges that build the ".XBT" index.
        /// A tick with reference "BMI" and weight null is the composite index tick.
        /// </summary>
        /// <returns></returns>
        WebCallResult<List<IndexComposite>> GetCompositeIndex(BitmexRequestWithFilter requestWithFilter);
        /// <summary>
        /// Get all price indices
        /// </summary>
        /// <returns></returns>
        WebCallResult<List<Instrument>> GetIndicies();
        /// <summary>
        /// Get insurance fund history
        /// </summary>
        /// <returns></returns>
        WebCallResult<List<Insurance>> GetInsuranceFundHistory();
        /// <summary>
        /// Get current leaderboard
        /// </summary>
        /// <param name="method">Ranking type. Options: "notional", "ROE"</param>
        /// <returns></returns>
        WebCallResult<List<Leaderboard>> GetLeaderBoard(string method);
        /// <summary>
        /// Get your alias on the leaderboard
        /// </summary>
        /// <returns></returns>
        WebCallResult<string> GetLeaderBoardName();
        /// <summary>
        /// Get liquidation orders
        /// </summary>
        /// <param name="requestWithFilter"></param>
        /// <returns></returns>
        WebCallResult<List<Liquidation>> GetLiquidations(BitmexRequestWithFilter requestWithFilter);
        /// <summary>
        /// Get your orders
        /// To get open orders only, send {"open": true} in the filter param.
        /// <see href="http://www.onixs.biz/fix-dictionary/5.0.SP2/msgType_D_68.html"> See the FIX Spec for explanations of these fields.</see>
        /// </summary>
        /// <param name="requestWithFilter"></param>
        /// <returns></returns>
        WebCallResult<List<Order>> GetOrders(BitmexRequestWithFilter requestWithFilter);
        /// <summary>
        /// Place order. See instructions at
        /// <see href="https://www.bitmex.com/api/explorer/#!/Order/Order_new">Bitmex docs</see>
        /// </summary>
        /// <param name="placeOrderRequest"></param>
        /// <returns></returns>
        WebCallResult<Order> PlaceOrder(PlaceOrderRequest placeOrderRequest);
        /// <summary>
        /// Update order.
        /// Send an orderID or origClOrdID to identify the order you wish to amend.
        /// Both order quantity and price can be amended.Only one qty field can be used to amend.
        /// Use the leavesQty field to specify how much of the order you wish to remain open. This can be useful if you want to adjust your position's delta by a certain amount,
        /// regardless of how much of the order has already filled.
        /// A leavesQty can be used to make a "Filled" order live again, if it is received within 60 seconds of the fill.
        /// Like order placement, amending can be done in bulk.Simply send a request to PUT /api/v1/order/bulk with a JSON body of the shape: { "orders": [{...}, {...}]}, each object containing the fields used in this endpoint.
        /// </summary>
        /// <param name="updateOrderRequest"></param>
        /// <returns></returns>
        WebCallResult<Order> UpdateOrder(UpdateOrderRequest updateOrderRequest);
        /// <summary>
        /// Cancel order. Either an orderID or a clOrdID must be provided.
        /// </summary>
        /// <param name="cancelOrderRequest"></param>
        /// <returns></returns>
        WebCallResult<Order> CancelOrder(CancelOrderRequest cancelOrderRequest);
        /// <summary>
        /// Cancels all orders
        /// </summary>
        /// <param name="symbol">Optional symbol. If provided, only cancels orders for that symbol.</param>
        /// <param name="filter">Optional filter for cancellation. Use to only cancel some orders, e.g. {"side": "Buy"}.</param>
        /// <param name="text">Optional cancellation annotation. e.g. 'Spread Exceeded'</param>
        /// <returns></returns>
        WebCallResult<List<Order>> CancelAllOrders(string symbol=null, BitmexRequestWithFilter filter=null, string text=null);
        /// <summary>
        /// Account Operations
        /// </summary>
        /// <returns></returns>
        WebCallResult<User> GetUserAccount();
        /// <summary>
        /// Account Operations
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<WebCallResult<User>> GetUserAccountAsync(CancellationToken ct = default);

    }
}
