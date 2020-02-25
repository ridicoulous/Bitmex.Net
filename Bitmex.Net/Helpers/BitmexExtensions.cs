using Bitmex.Net.Client.Attributes;
using  Bitmex.Net.Client.Objects;
using  Bitmex.Net.Client.Objects.Requests;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Bitmex.Net.Client.Helpers
{
    /// <summary>
    /// create dictionary from object and vice versa<see href="https://stackoverflow.com/questions/4943817/mapping-object-to-dictionary-and-vice-versa/4944547#4944547"/>
    /// </summary>
    public static class ObjectExtensions
    {
        public static T ToObject<T>(this IDictionary<string, object> source)
        where T : class, new()
        {
            var someObject = new T();
            var someObjectType = someObject.GetType();

            foreach (var item in source)
            {
                someObjectType
                         .GetProperty(item.Key)
                         .SetValue(someObject, item.Value, null);
            }

            return someObject;
        }

        public static Dictionary<string, object> AsDictionary(this object source, 
            BindingFlags bindingAttr = BindingFlags.FlattenHierarchy |
            BindingFlags.Instance |
            BindingFlags.NonPublic |
            BindingFlags.Public |
            BindingFlags.Static)
        {
            var result = new Dictionary<string, object>();
            var props = source.GetType().GetProperties(bindingAttr);
            foreach (var p in props.Where(c => !c.IsDefined(typeof(BitmexRequestIgnoreAttribute))))
            {
                string key = p.Name;
                if (p.IsDefined(typeof(JsonPropertyAttribute)))
                {
                    key = p.GetCustomAttribute<JsonPropertyAttribute>().PropertyName ?? p.Name;
                }
                object value = p.GetValue(source, null);
                if (!result.ContainsKey(key) && !String.IsNullOrEmpty(key) && value != null && value!=default)
                {
                    result.Add(key, value.ToString());
                }
            }
            return result;
        }
    }
    public static class BitmexRequestExtensions
    {
        public static BitmexRequestWithFilter WithResponseColumnFilter(this BitmexRequestWithFilter filter, params string[] columnNames)
        {
            foreach (var c in columnNames)
            {
                filter.AddColumnToGetInRequest(c);
            }
            return filter;
        }
        public static BitmexRequestWithFilter WithFilter(this BitmexRequestWithFilter filter, string key, string value)
        {
            filter.AddFilter(key, value);
            return filter;
        }
        public static BitmexRequestWithFilter SetResultsCount(this BitmexRequestWithFilter filter, int count)
        {
            filter.Count = count;
            return filter;
        }
        public static BitmexRequestWithFilter WithSymbolFilter(this BitmexRequestWithFilter filter, string symbol)
        {
            filter.AddFilter("symbol", symbol);
            return filter;
        }
        public static BitmexRequestWithFilter WithSideFilter(this BitmexRequestWithFilter filter, BitmexOrderSide side)
        {
            filter.AddFilter("side", side.ToString());
            return filter;
        }
        /// <summary>
        /// use this extension to sort results by time descending
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static BitmexRequestWithFilter WithNewestFirst(this BitmexRequestWithFilter filter)
        {
            filter.Reverse = true;
            return filter;
        }
        /// <summary>
        /// use this extension to sort results by time ascending
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static BitmexRequestWithFilter WithOldestFirst(this BitmexRequestWithFilter filter)
        {
            filter.Reverse = false;
            return filter;
        }
        /// <summary>
        /// <see href="https://www.bitmex.com/app/restAPI#---4">timestamp filtering docs</see>
        /// </summary>     
        public static BitmexRequestWithFilter WithStartTimeFilter(this BitmexRequestWithFilter filter, DateTime from)
        {
            filter.AddFilter("startTime", from.ToString("yyyy-MM-dd HH:mm:ss.zzz"));
            return filter;
        }

        /// <summary>
        /// <see href="https://www.bitmex.com/app/restAPI#---4">timestamp filtering docs</see>
        /// </summary>  
        public static BitmexRequestWithFilter WithEndTimeFilter(this BitmexRequestWithFilter filter, DateTime to)
        {
            filter.AddFilter("endTime", to.ToString("yyyy-MM-dd HH:mm:ss.zzz"));
            return filter;
        }
        /// <summary>
        /// <see href="https://www.bitmex.com/app/restAPI#---4">timestamp filtering docs</see>
        /// </summary>  
        public static BitmexRequestWithFilter WithExactDateTimeFilter(this BitmexRequestWithFilter filter, DateTime exactDateTimeFilter)
        {
            filter.AddFilter("timestamp", exactDateTimeFilter.ToString("yyyy-MM-dd HH:mm:ss"));
            return filter;
        }
        /// <summary>
        /// <see href="https://www.bitmex.com/app/restAPI#---4">timestamp filtering docs</see>
        /// </summary>  
        public static BitmexRequestWithFilter WithExactDateFilter(this BitmexRequestWithFilter filter, DateTime exactDateFilter)
        {
            filter.AddFilter("timestamp.date", exactDateFilter.ToString("yyyy-MM-dd"));
            return filter;

        }
        public static BitmexRequestWithFilter WithExactMonthFilter(this BitmexRequestWithFilter filter, int year, int month)
        {
            filter.AddFilter("timestamp.month", $"{year}-{month}");
            return filter;
        }
    }
}
