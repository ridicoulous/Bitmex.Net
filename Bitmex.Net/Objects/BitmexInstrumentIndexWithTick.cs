using System;
using System.Collections.Generic;
using System.Text;

namespace Bitmex.Net.Client.Objects
{
    public class BitmexInstrumentIndexWithTick
    {
        public BitmexInstrumentIndexWithTick(int index, decimal? tick)
        {
            Index = index;
            TickSize = tick ?? 0.01m;
        }
        public int Index { get; set;}
        public decimal TickSize { get; set; }
    }
}
