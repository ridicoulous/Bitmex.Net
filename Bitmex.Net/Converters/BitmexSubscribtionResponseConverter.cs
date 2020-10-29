using Bitmex.Net.Client.Objects.Socket;
using Bitmex.Net.Client.Objects.Socket.Repsonses;
using Bitmex.Net.Client.Objects.Socket.Requests;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Bitmex.Net.Client.Converters
{
    public class BitmexSubscribtionResponseConverter : JsonConverter<BitmexSubscribeRequest>
    {

        public override BitmexSubscribeRequest ReadJson(JsonReader reader, Type objectType, [AllowNull] BitmexSubscribeRequest existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject t = JObject.Load(reader);
            var arg = new List<string>();
            if (t["args"] != null)
            {
                var args = t["args"];
                if (args.Type == JTokenType.String)
                {
                    arg.Add(args.ToString());
                }
                if (args.Type == JTokenType.Array)
                    arg.AddRange(JsonConvert.DeserializeObject<List<string>>(args.ToString()));
            }
            return new BitmexSubscribeRequest()
            {
                Args = arg,
                Op = new BitmexWebSocketOperationConverter(false).ReadString((string)t["op"])
            };
        }



        public override void WriteJson(JsonWriter writer, [AllowNull] BitmexSubscribeRequest value, JsonSerializer serializer)
        {

        }
    }
}
