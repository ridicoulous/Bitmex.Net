using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bitmex.Net.Client.Converters
{  
    public class BitmexSubscribeResponseConverter : JsonConverter
    {
        // Declared as abstract in JsonConverter so must be overridden
        public override bool CanConvert(Type objectType) { return true; }

        // Declared as abstract in JsonConverter so must be overridden
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) 
        {
            //List<string> result = new List<string>();
            //if(!value.ToString().StartsWith("["))
            //{
            //    var ttt = (List<string>)value;
            //    result.AddRange(ttt);
            //    writer.WriteValue(result);
            //    return;
            //}
            //var token = JToken.Parse(value.ToString());
            //if (token.Type == JTokenType.String)
            //{
            //    result.Add(token.ToString());
            //    writer.WriteValue(result);
            //}
            //else if(token.Type==JTokenType.Array)
            //{
            //    writer.WriteValue(value);
            //}
            return;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);

            return token.Type == JTokenType.Array ? token.ToObject<string[]>() : new string[] { token.ToString() };
        }
    }
}
