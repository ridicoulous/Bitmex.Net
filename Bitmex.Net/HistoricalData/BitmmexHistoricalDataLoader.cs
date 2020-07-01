using Bitmex.Net.Client.Objects;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Bitmex.Net.Client.HistoricalData
{
    public class BitmexHistoricalTradesLoader
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
        private string GetUrl(DateTime date)
        {
            string testnetPrefix = _isTest ? "-testnet" : String.Empty;
            return $"https://s3-eu-west-1.amazonaws.com/public{testnetPrefix}.bitmex.com/data/trade/{date:yyyyMMdd}.csv.gz";
        }    
        public async Task<List<Trade>> GetDailyTrades(DateTime date, CancellationToken cancellationToken = default, params string[] symbols)
        {
            bool shoulCheckSymbols = symbols.Any();
            var result = new List<Trade>(2_000_000);

            var stream = await _client.GetAsync(GetUrl(date),cancellationToken);
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
                            while (csv.Read())
                            {
                                try
                                {
                                    var row = csv.GetRecord<Trade>();
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
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
        public sealed class TradeMap : ClassMap<Trade>
        {
            public TradeMap()
            {
                CultureInfo en = new CultureInfo("en-US");
                Map(m => m.Timestamp).Index(0).TypeConverter<DateTimeConverter>();
                Map(m => m.Symbol).Index(1);
                Map(m => m.Side).Index(2);
                Map(m => m.Size).Index(3);
                Map(m => m.Price).Index(4).TypeConverterOption.NumberStyles(NumberStyles.Float);
                Map(m => m.TickDirection).Index(5);
                Map(m => m.TrdMatchID).Index(6);
                Map(m => m.GrossValue).Index(7);
                Map(m => m.HomeNotional).Index(8).TypeConverterOption.NumberStyles(NumberStyles.Float);
                Map(m => m.ForeignNotional).Index(9).TypeConverterOption.NumberStyles(NumberStyles.Float);
            }
        }
        private class DateTimeConverter : DefaultTypeConverter
        {
            /// <summary>
            /// Converts the string to an object.
            /// </summary>
            /// <param name="text">The string to convert to an object.</param>
            /// <param name="row">The <see cref="IReaderRow"/> for the current record.</param>
            /// <param name="memberMapData">The <see cref="MemberMapData"/> for the member being created.</param>
            /// <returns>The object created from the string.</returns>
            public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
            {
                if (text == null)
                {
                    return base.ConvertFromString(null, row, memberMapData);
                }
                text = text.Replace("D", " ");
                var formatProvider = (IFormatProvider)memberMapData.TypeConverterOptions.CultureInfo.GetFormat(typeof(DateTimeFormatInfo)) ?? memberMapData.TypeConverterOptions.CultureInfo;
                var dateTimeStyle = memberMapData.TypeConverterOptions.DateTimeStyle ?? DateTimeStyles.None;

                return memberMapData.TypeConverterOptions.Formats == null || memberMapData.TypeConverterOptions.Formats.Length == 0
                    ? DateTime.Parse(text, formatProvider, dateTimeStyle)
                    : DateTime.ParseExact(text, memberMapData.TypeConverterOptions.Formats, formatProvider, dateTimeStyle);
            }
        }
    }
}
