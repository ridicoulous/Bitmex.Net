using System;
using System.Collections.Generic;
using System.Text;

namespace Bitmex.Net.Client.Helpers
{
    public static class OrderBookHelpers
    {
        public static decimal GetPriceFromId(long id, int instrumentIndex, decimal tickSize)
        {
            return (100000000 * instrumentIndex - id) * tickSize;
        }    
    }
}
