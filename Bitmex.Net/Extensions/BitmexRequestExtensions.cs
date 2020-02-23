using Bitmex.Net.Objects;
using Bitmex.Net.Objects.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitmex.Net.Extensions
{
    public static class BitmexRequestExtensions
    {
        public static BitmexRequestWithFilter WithResponseColumnFilter(this BitmexRequestWithFilter filter, params string[] columnNames)
        {
            foreach(var c in columnNames)
            {
                filter.AddColumnToGetInRequest(c);
            }
            return filter;
        }
        public static BitmexRequestWithFilter WithFilter(this BitmexRequestWithFilter filter,string key, string value)
        {            
            filter.AddFilter(key, value);
            return filter;
        }
        public static BitmexRequestWithFilter WithSymbolFilter(BitmexRequestWithFilter filter, string symbol)
        {
            filter.AddFilter("symbol", symbol);
            return filter;
        }
        public static BitmexRequestWithFilter WithSideFilter(BitmexRequestWithFilter filter, BitmexOrderSide side)
        {
            filter.AddFilter("side", side.ToString());
            return filter;
        }
        /// <summary>
        /// <see href="https://www.bitmex.com/app/restAPI#---4">timestamp filtering docs</see>
        /// </summary>     
        public static BitmexRequestWithFilter WithStartTimeFilter(BitmexRequestWithFilter filter, DateTime from)
        {
            filter.AddFilter("startTime", from.ToString("yyyy-MM-dd HH:mm:ss.zzz"));
            return filter;

        }
        /// <summary>
        /// <see href="https://www.bitmex.com/app/restAPI#---4">timestamp filtering docs</see>
        /// </summary>  
        public static BitmexRequestWithFilter WithEndTimeFilter(BitmexRequestWithFilter filter, DateTime to)
        {
            filter.AddFilter("endTime", to.ToString("yyyy-MM-dd HH:mm:ss.zzz"));
            return filter;
        }
        /// <summary>
        /// <see href="https://www.bitmex.com/app/restAPI#---4">timestamp filtering docs</see>
        /// </summary>  
        public static BitmexRequestWithFilter WithExactDateTimeFilter(BitmexRequestWithFilter filter, DateTime exactDateTimeFilter)
        {
            filter.AddFilter("timestamp", exactDateTimeFilter.ToString("yyyy-MM-dd HH:mm:ss"));
            return filter;
        }
        /// <summary>
        /// <see href="https://www.bitmex.com/app/restAPI#---4">timestamp filtering docs</see>
        /// </summary>  
        public static BitmexRequestWithFilter WithExactDateFilter(BitmexRequestWithFilter filter, DateTime exactDateFilter)
        {
            filter.AddFilter("timestamp.date", exactDateFilter.ToString("yyyy-MM-dd"));
            return filter;

        }
        public static BitmexRequestWithFilter WithExactMonthFilter(BitmexRequestWithFilter filter, int year, int month)
        {
            filter.AddFilter("timestamp.month", $"{year}-{month}");
            return filter;
        }
    }
}
