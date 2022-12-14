using Newtonsoft.Json;

namespace Bitmex.Net.Client.Objects
{
    public class WalletAsset
    {
        [JsonProperty("asset", Required = Required.Always)]
        public string Asset { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("majorCurrency")]
        public string MajorCurrency { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("currencyType")]
        public string СurrencyType { get; set; }

        [JsonProperty("scale")]
        public int Scale { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("isMarginCurrency")]
        public bool IsMarginCurrency { get; set; }

        [JsonProperty("networks")]
        public Network[] Networks { get; set; }
    }
}

