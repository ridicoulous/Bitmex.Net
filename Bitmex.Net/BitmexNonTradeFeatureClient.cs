using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Bitmex.Net.Client;
using Bitmex.Net.Client.Interfaces;
using Bitmex.Net.Client.Objects;
using Bitmex.Net.Client.Objects.Requests;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;

namespace Bitmex.Net
{
    public class BitmexNonTradeFeatureClient : BitmexBaseClient, IBitmexNonTradeFeaturesClient
    {
        #region Endpoints
        private const string GetAnnouncementsEndpoint = "announcement";
        private const string GetUrgentAnnouncementsEndpoint = "announcement/urgent";
        private const string GetApiKeysEndpoint = "apiKey";
        private const string ChatMessagesEndpoint = "chat";
        private const string GetAvailableChannelsEndpoint = "chat/channels";
        private const string GetConnectedUsersEndpoint = "chat/connected";
        private const string GlobalNotificationEndpoint = "globalNotification";
        private const string LeaderBoardEndpoint = "leaderboard";
        private const string LeaderBoardByNameEndpoint = "leaderboard/name";
        private const string Schemandpoint = "schema";
        private const string SchemaWebsokcetHelpEndpoint = "schema/websocketHelp";

        #endregion

        internal BitmexNonTradeFeatureClient(string name, BitmexClientOptions options, Log log, BitmexClient client) : base(name, options, log, client)
        {
        }

        #region Public Announcements
        public async Task<WebCallResult<List<Announcement>>> GetAnnouncementsAsync(List<string> columns = null, CancellationToken ct = default)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("columns", JsonConvert.SerializeObject(columns));
            return await SendRequestAsync<List<Announcement>>(GetAnnouncementsEndpoint, HttpMethod.Get, ct, parameters);
        }
        public async Task<WebCallResult<List<Announcement>>> GetUrgentAnnouncementsAsync(CancellationToken ct = default)
        {
            return await SendRequestAsync<List<Announcement>>(GetUrgentAnnouncementsEndpoint, HttpMethod.Get, ct);
        }

        #endregion
        #region Persistent API Keys for Developers
        public async Task<WebCallResult<List<APIKey>>> GetApiKeysAsync(bool reverse = false, CancellationToken ct = default)
        {
            return await SendRequestAsync<List<APIKey>>(GetApiKeysEndpoint, HttpMethod.Get, ct);
        }
        #endregion
        #region Chat : Trollbox Data 
        public async Task<WebCallResult<List<Chat>>> GetChatMessagesAsync(int channelId, BitmexRequestWithFilter requestWithFilter = null, CancellationToken ct = default)
        {
            var parameters = GetParameters(requestWithFilter);
            parameters.Add("channelId", channelId);
            return await SendRequestAsync<List<Chat>>(ChatMessagesEndpoint, HttpMethod.Get, ct);
        }
        public async Task<WebCallResult<Chat>> SendChatMessageAsync(int channelId, string message, CancellationToken ct = default)
        {
            var parameters = GetParameters();
            parameters.Add("channelId", channelId);
            parameters.Add("message", message);
            return await SendRequestAsync<Chat>(ChatMessagesEndpoint, HttpMethod.Post, ct, parameters);
        }
        public async Task<WebCallResult<List<ChatChannel>>> GetChannelsAsync(CancellationToken ct = default)
        {
            return await SendRequestAsync<List<ChatChannel>>(GetAvailableChannelsEndpoint, HttpMethod.Get, ct);
        }

        public async Task<WebCallResult<ConnectedUsers>> GetConnectedUsersAsync(CancellationToken ct = default)
        {
            return await SendRequestAsync<ConnectedUsers>(GetConnectedUsersEndpoint, HttpMethod.Get, ct);
        }
        #endregion
        #region GlobalNotification : Account Notifications 
        public async Task<WebCallResult<List<GlobalNotification>>> GetGlobalNotificationsAsync(CancellationToken ct = default)
        {
            return await SendRequestAsync<List<GlobalNotification>>(GlobalNotificationEndpoint, HttpMethod.Get, ct);
        }
        #endregion
        #region Leaderboard : Information on Top Users 
        public async Task<WebCallResult<List<Leaderboard>>> GetLeaderBoardAsync(string method, CancellationToken ct = default)
        {
            var parameters = GetParameters();
            parameters.Add("method", method);
            return await SendRequestAsync<List<Leaderboard>>(LeaderBoardEndpoint, HttpMethod.Get, ct, parameters);
        }

        public async Task<WebCallResult<string>> GetLeaderBoardNameAsync(CancellationToken ct = default)
        {
            return await SendRequestAsync<string>(LeaderBoardEndpoint, HttpMethod.Get, ct);
        }
        #endregion
        #region Schema : Dynamic Schemata for Developers 
        public async Task<WebCallResult<object>> GetSchemaAsync(string model, CancellationToken ct = default)
        {
            var parameters = GetParameters();
            parameters.Add("model", model);
            return await SendRequestAsync<object>(Schemandpoint, HttpMethod.Get, ct);
        }

        public async Task<WebCallResult<object>> GetWebsokcetHelpAsync(CancellationToken ct = default)
        {
            return await SendRequestAsync<object>(SchemaWebsokcetHelpEndpoint, HttpMethod.Get, ct);
        }
        #endregion


    }
}