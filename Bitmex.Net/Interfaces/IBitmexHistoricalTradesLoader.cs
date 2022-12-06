using Bitmex.Net.Client.Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bitmex.Net.Client.Interfaces
{
    public interface IBitmexHistoricalTradesLoader
    {
        /// <summary>
        /// Load daily quotes data from <see href="https://www.bitmex.com/app/apiOverview#Historical-Data">Bitmex public historical dataset</see>
        /// </summary>
        /// <param name="date">day to load</param>
        /// <param name="symbols">optionally filter results by symbols</param>
        /// <returns></returns>
        List<Quote> GetDailyQuotes(DateTime date, params string[] symbols);
        /// <summary>
        /// Load daily quotes data from <see href="https://www.bitmex.com/app/apiOverview#Historical-Data">Bitmex public historical dataset</see>
        /// </summary>
        /// <param name="date">day to load</param>
        /// <param name="symbols">optionally filter results by symbols</param>
        /// <returns></returns>
        Task<List<Quote>> GetDailyQuotesAsync(DateTime date, params string[] symbols);
        /// <summary>
        /// Load daily quotes data from <see href="https://www.bitmex.com/app/apiOverview#Historical-Data">Bitmex public historical dataset</see>
        /// </summary>
        /// <param name="date">day to load</param>
        /// <param name="symbols">optionally filter results by symbols</param>
        /// <returns></returns>
        List<BitmexTrade> GetDailyTrades(DateTime date, params string[] symbols);
        /// <summary>
        /// Load daily tick trades data from <see href="https://www.bitmex.com/app/apiOverview#Historical-Data">Bitmex public historical dataset</see>
        /// </summary>
        /// <param name="date">day to load</param>
        /// <param name="symbols">optionally filter results by symbols</param>
        /// <returns></returns>
        Task<List<BitmexTrade>> GetDailyTradesAsync(DateTime date, params string[] symbols);
        /// <summary>
        /// Load period quotes data from <see href="https://www.bitmex.com/app/apiOverview#Historical-Data">Bitmex public historical dataset</see>
        /// </summary>
        /// <param name="from">from day to load</param>  
        /// <param name="to">to day to load</param>
        /// <param name="symbols">optionally filter results by symbols</param>
        /// <returns></returns>
        List<Quote> GetQuotesByPeriod(DateTime from, DateTime to, params string[] symbols);
        /// <summary>
        /// Load period quotes data from <see href="https://www.bitmex.com/app/apiOverview#Historical-Data">Bitmex public historical dataset</see>
        /// </summary>
        /// <param name="date">day to load</param>
        /// <param name="to">to day to load</param>  
        /// <param name="symbols">optionally filter results by symbols</param>
        /// <returns></returns>
        Task<List<Quote>> GetQuotesByPeriodAsync(DateTime from, DateTime to, params string[] symbols);
        /// <summary>
        /// Load period tick trades data from <see href="https://www.bitmex.com/app/apiOverview#Historical-Data">Bitmex public historical dataset</see>
        /// </summary>
        /// <param name="date">day to load</param>
        /// <param name="to">to day to load</param>  
        /// <param name="symbols">optionally filter results by symbols</param>
        /// <returns></returns>
        List<BitmexTrade> GetTradesByPeriod(DateTime from, DateTime to, params string[] symbols);
        /// <summary>
        /// Load period tick trades data from <see href="https://www.bitmex.com/app/apiOverview#Historical-Data">Bitmex public historical dataset</see>
        /// </summary>
        /// <param name="from">from day to load</param>
        /// <param name="to">to day to load</param>        
        /// <param name="symbols">optionally filter results by symbols</param>
        /// <returns></returns>
        Task<List<BitmexTrade>> GetTradesByPeriodAsync(DateTime from, DateTime to, params string[] symbols);
    }
}