using Newtonsoft.Json;

namespace Bitmex.Net.Objects
{





    public class InstrumentInterval
    {
        [JsonProperty("intervals", Required = Required.Always)]

        public System.Collections.Generic.ICollection<string> Intervals { get; set; } = new System.Collections.ObjectModel.Collection<string>();

        [JsonProperty("symbols", Required = Required.Always)]

        public System.Collections.Generic.ICollection<string> Symbols { get; set; } = new System.Collections.ObjectModel.Collection<string>();


    }

}

