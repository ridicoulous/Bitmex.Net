using Bitmex.Net.Client.Objects;
using CsvHelper.Configuration;
using System.Globalization;

namespace Bitmex.Net.Client.HistoricalData
{
    public sealed class TradeMap : ClassMap<Trade>
    {
        public TradeMap()
        {
            CultureInfo en = new CultureInfo("en-US");
            Map(m => m.Timestamp).Index(0).TypeConverter<BitmexCsvDateTimeConverter>();
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
    public sealed class QuoteMap : ClassMap<Quote>
    {
        public QuoteMap()
        {
            CultureInfo en = new CultureInfo("en-US");
            Map(m => m.Timestamp).Index(0).TypeConverter<BitmexCsvDateTimeConverter>();
            Map(m => m.Symbol).Index(1);
            Map(m => m.BidSize).Index(2).TypeConverterOption.NumberStyles(NumberStyles.Float);
            Map(m => m.BidPrice ).Index(3).TypeConverterOption.NumberStyles(NumberStyles.Float);
            Map(m => m.AskPrice).Index(4).TypeConverterOption.NumberStyles(NumberStyles.Float);
            Map(m => m.AskSize).Index(5).TypeConverterOption.NumberStyles(NumberStyles.Float);           
        }
    }
}
