using Newtonsoft.Json;

namespace    Bitmex.Net.Client.Objects
{
    public class UserPreferences
    {
        [JsonProperty("alertOnLiquidations")]
        public bool? AlertOnLiquidations { get; set; }

        [JsonProperty("animationsEnabled")]
        public bool? AnimationsEnabled { get; set; }

        [JsonProperty("announcementsLastSeen")]
        public System.DateTime? AnnouncementsLastSeen { get; set; }

        [JsonProperty("chatChannelID")]
        public long? ChatChannelID { get; set; }

        [JsonProperty("colorTheme")]
        public string ColorTheme { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("debug")]
        public bool? Debug { get; set; }

        [JsonProperty("disableEmails")]
        public System.Collections.Generic.ICollection<string> DisableEmails { get; set; }

        [JsonProperty("disablePush")]
        public System.Collections.Generic.ICollection<string> DisablePush { get; set; }

        [JsonProperty("hideConfirmDialogs")]
        public System.Collections.Generic.ICollection<string> HideConfirmDialogs { get; set; }

        [JsonProperty("hideConnectionModal")]
        public bool? HideConnectionModal { get; set; }

        [JsonProperty("hideFromLeaderboard")]
        public bool? HideFromLeaderboard { get; set; } = false;

        [JsonProperty("hideNameFromLeaderboard")]
        public bool? HideNameFromLeaderboard { get; set; } = true;

        [JsonProperty("hideNotifications")]
        public System.Collections.Generic.ICollection<string> HideNotifications { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; } = "en-US";

        [JsonProperty("msgsSeen")]
        public System.Collections.Generic.ICollection<string> MsgsSeen { get; set; }

        [JsonProperty("orderBookBinning")]
        public object OrderBookBinning { get; set; }

        [JsonProperty("orderBookType")]
        public string OrderBookType { get; set; }

        [JsonProperty("orderClearImmediate")]
        public bool? OrderClearImmediate { get; set; } = false;

        [JsonProperty("orderControlsPlusMinus")]
        public bool? OrderControlsPlusMinus { get; set; }

        [JsonProperty("showLocaleNumbers")]
        public bool? ShowLocaleNumbers { get; set; } = true;

        [JsonProperty("sounds")]
        public System.Collections.Generic.ICollection<string> Sounds { get; set; }

        [JsonProperty("strictIPCheck")]
        public bool? StrictIPCheck { get; set; } = false;

        [JsonProperty("strictTimeout")]
        public bool? StrictTimeout { get; set; } = true;

        [JsonProperty("tickerGroup")]
        public string TickerGroup { get; set; }

        [JsonProperty("tickerPinned")]
        public bool? TickerPinned { get; set; }

        [JsonProperty("tradeLayout")]
        public string TradeLayout { get; set; }


    }

}

