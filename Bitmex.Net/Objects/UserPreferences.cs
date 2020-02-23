using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{
    public class UserPreferences
    {
        [JsonProperty("alertOnLiquidations", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? AlertOnLiquidations { get; set; }

        [JsonProperty("animationsEnabled", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? AnimationsEnabled { get; set; }

        [JsonProperty("announcementsLastSeen", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? AnnouncementsLastSeen { get; set; }

        [JsonProperty("chatChannelID", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public decimal? ChatChannelID { get; set; }

        [JsonProperty("colorTheme", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string ColorTheme { get; set; }

        [JsonProperty("currency", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        [JsonProperty("debug", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Debug { get; set; }

        [JsonProperty("disableEmails", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<string> DisableEmails { get; set; }

        [JsonProperty("disablePush", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<string> DisablePush { get; set; }

        [JsonProperty("hideConfirmDialogs", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<string> HideConfirmDialogs { get; set; }

        [JsonProperty("hideConnectionModal", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? HideConnectionModal { get; set; }

        [JsonProperty("hideFromLeaderboard", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? HideFromLeaderboard { get; set; } = false;

        [JsonProperty("hideNameFromLeaderboard", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? HideNameFromLeaderboard { get; set; } = true;

        [JsonProperty("hideNotifications", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<string> HideNotifications { get; set; }

        [JsonProperty("locale", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string Locale { get; set; } = "en-US";

        [JsonProperty("msgsSeen", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<string> MsgsSeen { get; set; }

        [JsonProperty("orderBookBinning", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public object OrderBookBinning { get; set; }

        [JsonProperty("orderBookType", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string OrderBookType { get; set; }

        [JsonProperty("orderClearImmediate", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? OrderClearImmediate { get; set; } = false;

        [JsonProperty("orderControlsPlusMinus", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? OrderControlsPlusMinus { get; set; }

        [JsonProperty("showLocaleNumbers", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? ShowLocaleNumbers { get; set; } = true;

        [JsonProperty("sounds", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<string> Sounds { get; set; }

        [JsonProperty("strictIPCheck", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? StrictIPCheck { get; set; } = false;

        [JsonProperty("strictTimeout", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? StrictTimeout { get; set; } = true;

        [JsonProperty("tickerGroup", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string TickerGroup { get; set; }

        [JsonProperty("tickerPinned", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public bool? TickerPinned { get; set; }

        [JsonProperty("tradeLayout", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string TradeLayout { get; set; }


    }

}

