using Newtonsoft.Json;

namespace Bitmex.Net.Client.Objects
{
    public class Network
    {
        [JsonProperty("asset")]
        public string Asset { get; set; }

        [JsonProperty("tokenAddress")]
        public string TokenAddress { get; set; }

        [JsonProperty("depositEnabled")]
        public bool DepositEnabled { get; set; }

        [JsonProperty("withdrawalEnabled")]
        public bool WithdrawalEnabled { get; set; }

        [JsonProperty("withdrawalFee")]
        public bool WithdrawalFee { get; set; }

        [JsonProperty("minFee")]
        public int MinFee { get; set; }

        [JsonProperty("maxFee")]
        public int MaxFee { get; set; }
    }
}
