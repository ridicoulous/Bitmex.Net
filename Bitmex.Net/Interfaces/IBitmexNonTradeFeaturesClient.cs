using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Bitmex.Net.Client.Objects;
using Bitmex.Net.Client.Objects.Requests;
using CryptoExchange.Net.Objects;

namespace Bitmex.Net.Client.Interfaces
{
    public interface IBitmexNonTradeFeaturesClient
    {
        #region Public Announcements
        /// <summary>
        /// Get site announcements
        /// </summary>
        /// <param name="columns">Array of column names to fetch. If omitted, will return all columns.</param>
        /// <returns></returns>
        Task<WebCallResult<List<Announcement>>> GetAnnouncementsAsync(List<string> columns = null, CancellationToken ct = default);
        /// <summary>
        /// Get urgent (banner) announcements.
        /// </summary>
        /// <returns></returns>
        Task<WebCallResult<List<Announcement>>> GetUrgentAnnouncementsAsync(CancellationToken ct = default);
        #endregion
        #region Persistent API Keys for Developers
        /// <summary>
        /// Get your API Keys
        /// </summary>
        /// <param name="reverse">If true, will sort results newest first.</param>
        /// <returns></returns>
        Task<WebCallResult<List<APIKey>>> GetApiKeysAsync(bool reverse = false, CancellationToken ct = default);
        #endregion
        #region Chat : Trollbox Data 
        /// <summary>
        /// Get chat messages
        /// </summary>
        /// <returns></returns>
        Task<WebCallResult<List<Chat>>> GetChatMessagesAsync(int channelId, BitmexRequestWithFilter filter = null, CancellationToken ct = default);
        /// <summary>
        /// Send a chat message
        /// </summary>
        /// <param name="channelId">channel id</param>
        /// <param name="message">message</param>
        /// <returns></returns>
        Task<WebCallResult<Chat>> SendChatMessageAsync(int channelId, string message, CancellationToken ct = default);
        /// <summary>
        /// Get available channels
        /// </summary>
        /// <returns></returns>
        Task<WebCallResult<List<ChatChannel>>> GetChannelsAsync(CancellationToken ct = default);
        /// <summary>
        /// Get connected users
        /// </summary>
        /// <returns></returns>
        Task<WebCallResult<ConnectedUsers>> GetConnectedUsersAsync(CancellationToken ct = default);
        #endregion
        #region GlobalNotification : Account Notifications 

        /// <summary>
        /// Get your current GlobalNotifications.
        /// </summary>
        /// <returns></returns>
        Task<WebCallResult<List<GlobalNotification>>> GetGlobalNotificationsAsync(CancellationToken ct = default);
        #endregion
        #region Leaderboard : Information on Top Users 
        /// <summary>
        /// Get current leaderboard
        /// </summary>
        /// <param name="method">Ranking type. Options: "notional", "ROE"</param>
        /// <returns></returns>
        Task<WebCallResult<List<Leaderboard>>> GetLeaderBoardAsync(string method, CancellationToken ct = default);

        /// <summary>
        /// Get your alias on the leaderboard
        /// </summary>
        /// <returns></returns>
        Task<WebCallResult<string>> GetLeaderBoardNameAsync(CancellationToken ct = default);
        #endregion
        #region Schema : Dynamic Schemata for Developers 
        /// <summary>
        /// Get model schemata for data objects returned by this API.
        /// </summary>
        /// <param name="model">Optional model filter. If omitted, will return all models.</param>
        /// <returns></returns>
        Task<WebCallResult<object>> GetSchemaAsync(string model, CancellationToken ct = default);

        /// <summary>
        /// Returns help text & subject list for websocket usage
        /// </summary>
        /// <returns></returns>
        Task<WebCallResult<object>> GetWebsokcetHelpAsync(CancellationToken ct = default);
        #endregion

    }
}