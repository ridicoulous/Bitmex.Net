using Bitmex.Net.Client.Attributes;
using Bitmex.Net.Client.Objects;
using Bitmex.Net.Client.Objects.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bitmex.Net.Client.Helpers.Extensions
{
    
    /// <summary>
    /// create dictionary from object and vice versa<see href="https://stackoverflow.com/questions/4943817/mapping-object-to-dictionary-and-vice-versa/4944547#4944547"/>
    /// </summary>
    public static class ObjectExtensions
    {
        public static decimal? Normalize(this decimal? value)
        {
            if (value == null)
            {
                return value;
            }
            return value / 1.000000000000000000000000000000000m;
        }
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
            try
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

                    if (value == null)
                    {
                        continue;
                    }
                    if (value is decimal || value is decimal?)
                    {
                        value = (value as decimal?).Normalize();
                    }
                    if (value.GetType().IsEnum)
                    {
                        value = value?.ToString();
                    }
                    if (!result.ContainsKey(key) && !String.IsNullOrEmpty(key) && !String.IsNullOrEmpty(value.ToString()))
                    {
                        result.Add(key, value);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            return filter.AddFilter(key, value);
        }
        public static BitmexRequestWithFilter WithOrderIdFilter(this BitmexRequestWithFilter filter, string orderId)
        {
            return filter.AddFilter("orderID", orderId);
        }
        public static BitmexRequestWithFilter WithClientOrderIdFilter(this BitmexRequestWithFilter filter, string clientOrderId)
        {
            return filter.AddFilter("clOrdID", clientOrderId);
        }
        public static BitmexRequestWithFilter WithStartingFrom(this BitmexRequestWithFilter filter, int startingPointToGetch)
        {
            filter.Start = startingPointToGetch;
            return filter;
        }
        public static BitmexRequestWithFilter WithResultsCount(this BitmexRequestWithFilter filter, int count)
        {
            filter.Count = count;
            return filter;
        }
        public static BitmexRequestWithFilter WithSymbolFilter(this BitmexRequestWithFilter filter, string symbol)
        {
            return filter.AddFilter("symbol", symbol);
        }
        public static BitmexRequestWithFilter WithOnlyActiveOrders(this BitmexRequestWithFilter filter)
        {
            return filter.AddFilter("open", true);
        }
        public static BitmexRequestWithFilter WithSideFilter(this BitmexRequestWithFilter filter, BitmexOrderSide side)
        {
            return filter.AddFilter("side", side.ToString());
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
        /// <see href="https://www.bitmex.com/app/restAPI#---4">timestamp filtering docs. This filter allow accuracy only by minutes</see>
        /// </summary>     
        public static BitmexRequestWithFilter WithStartTimeFilter(this BitmexRequestWithFilter filter, DateTime from)
        {
            return filter.AddFilter("startTime", from.ToString("yyyy-MM-dd HH:mm"));
        }

        /// <summary>
        /// <see href="https://www.bitmex.com/app/restAPI#---4">timestamp filtering docs. This filter allow accuracy only by minutes</see>
        /// </summary>  
        public static BitmexRequestWithFilter WithEndTimeFilter(this BitmexRequestWithFilter filter, DateTime to)
        {
            return filter.AddFilter("endTime", to.ToString("yyyy-MM-dd HH:mm"));
        }
        /// <summary>
        /// <see href="https://www.bitmex.com/app/restAPI#---4">timestamp filtering docs. This filter allow accuracy only by minutes</see>
        /// </summary>  
        public static BitmexRequestWithFilter WithExactDateTimeFilter(this BitmexRequestWithFilter filter, DateTime exactDateTimeFilter)
        {
            return filter.AddFilter("timestamp", exactDateTimeFilter.ToString("yyyy-MM-dd HH:mm"));
        }
        /// <summary>
        /// <see href="https://www.bitmex.com/app/restAPI#---4">timestamp filtering docs</see>
        /// </summary>  
        public static BitmexRequestWithFilter WithExactDateFilter(this BitmexRequestWithFilter filter, DateTime exactDateFilter)
        {
            return filter.AddFilter("timestamp.date", exactDateFilter.ToString("yyyy-MM-dd"));
        }
        public static BitmexRequestWithFilter WithExactMonthFilter(this BitmexRequestWithFilter filter, int year, int month)
        {
            return filter.AddFilter("timestamp.month", $"{year}-{month}");
        }
    }
}
