using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitmex.Net.Client.Objects
{
    public partial class WalletHistory
    {
        [JsonProperty("transactID")]
        public string TransactId { get; set; }

        [JsonProperty("account")]
        public long Account { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("transactType")]
        public string TransactType { get; set; }

        [JsonProperty("amount")]
        public decimal? Amount { get; set; }

        [JsonProperty("fee")]
        public decimal? Fee { get; set; }

        [JsonProperty("transactStatus")]
        public string TransactStatus { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("tx")]
        public string Tx { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("transactTime")]
        public DateTime? TransactTime { get; set; }

        [JsonProperty("walletBalance")]
        public decimal WalletBalance { get; set; }

        [JsonProperty("marginBalance")]
        public decimal? MarginBalance { get; set; }

        public decimal WalletBalanceInBtc => WalletBalance / 10e7m;
        public decimal MarginBalanceInBtc => MarginBalance ?? 0 / 10e7m;

        [JsonProperty("timestamp")]
        public DateTime? Timestamp { get; set; }
    }
}
