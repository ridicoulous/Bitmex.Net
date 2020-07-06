using Bitmex.Net.Client.Interfaces;
using Bitmex.Net.Client.Objects;
using Bitmex.Net.Client.Objects.Socket.Repsonses;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Bitmex.Net.Client.HistoricalData
{
    public class BitmexHistoricalTradesLoader : IBitmexHistoricalTradesLoader
    {
        private readonly HttpClient _client;
        private readonly bool _isTest;
        public BitmexHistoricalTradesLoader(bool isTestNet = false)
        {
            _isTest = isTestNet;
            _client = new HttpClient();
            _client.Timeout = TimeSpan.FromMinutes(10);
            _client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.117 Safari/537.36");
        }
        private string GetUrl<T>(DateTime date) where T : IBitmexHistoricalDataEntry
        {
            string testnetPrefix = _isTest ? "-testnet" : String.Empty;
            return $"https://s3-eu-west-1.amazonaws.com/public{testnetPrefix}.bitmex.com/data/{typeof(T).Name.ToLower()}/{date.Date:yyyyMMdd}.csv.gz";
        }
        public List<Trade> GetDailyTrades(DateTime date, params string[] symbols) => GetDailyTradesAsync(date, symbols).GetAwaiter().GetResult();
        public async Task<List<Trade>> GetDailyTradesAsync(DateTime date, params string[] symbols)
        {
            return await LoadDailyData<Trade>(date, symbols);
        }
        public List<Quote> GetDailyQuotes(DateTime date, params string[] symbols) => GetDailyQuotesAsync(date, symbols).GetAwaiter().GetResult();

        public async Task<List<Quote>> GetDailyQuotesAsync(DateTime date, params string[] symbols)
        {
            return await LoadDailyData<Quote>(date, symbols);
        }
        public async Task<List<Trade>> GetTradesByPeriodAsync(DateTime from, DateTime to, params string[] symbols)
        {
            return await LoadDataByPeriodAsync<Trade>(from, to, symbols);
        }
        public List<Quote> GetQuotesByPeriod(DateTime from, DateTime to, params string[] symbols) => GetQuotesByPeriodAsync(from, to, symbols).GetAwaiter().GetResult();
        public async Task<List<Quote>> GetQuotesByPeriodAsync(DateTime from, DateTime to, params string[] symbols)
        {
            return await LoadDataByPeriodAsync<Quote>(from, to, symbols);
        }
        private async Task<List<T>> LoadDataByPeriodAsync<T>(DateTime from, DateTime to, params string[] symbols) where T : IBitmexHistoricalDataEntry
        {
            if (from.Date > to.Date)
            {
                throw new Exception($"{nameof(to)} date must be greater than {nameof(from)} date");
            }
            var daysCount = to.Date.AddDays(1).Subtract(from.Date).Days;
            if (from.Date == to.Date || daysCount == 1)
            {
                return await LoadDailyData<T>(from, symbols);
            }
            
            List<T> result = new List<T>(2_500_000 * daysCount);
            while (from.Date <= to.Date)
            {
                var dailyData = await LoadDailyData<T>(from, symbols: symbols);
                result.AddRange(dailyData);
                from = from.AddDays(1);
            }
            return result;
        }

        private async Task<List<T>> LoadDailyData<T>(DateTime date, params string[] symbols) where T : IBitmexHistoricalDataEntry
        {
            bool shoulCheckSymbols = symbols.Any();
            var result = new List<T>(2_500_000);
            var stream = await _client.GetAsync(GetUrl<T>(date));
            if (!stream.IsSuccessStatusCode)
            {
                throw new Exception($"Seems like here is any info at this date {date}");
            }
            using (var s = await stream.Content.ReadAsStreamAsync())
            {
                using (GZipStream decompressionStream = new GZipStream(s, CompressionMode.Decompress))
                {
                    using (var decompressed = new StreamReader(decompressionStream))
                    {
                        using (var csv = new CsvReader(decompressed, CultureInfo.InvariantCulture))
                        {
                            csv.Configuration.Delimiter = ",";
                            csv.Configuration.IgnoreBlankLines = true;
                            csv.Configuration.RegisterClassMap<TradeMap>();
                            csv.Configuration.RegisterClassMap<QuoteMap>();
                            while (csv.Read())
                            {
                                var row = csv.GetRecord<T>();
                                if (shoulCheckSymbols)
                                {
                                    if (symbols.Contains(row.Symbol))
                                    {
                                        result.Add(row);
                                    }
                                }
                                else
                                {
                                    result.Add(row);
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        public List<Trade> GetTradesByPeriod(DateTime from, DateTime to, params string[] symbols)
        {
            throw new NotImplementedException();
        }
    }
}
