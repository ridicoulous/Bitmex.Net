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
        //https://stackoverflow.com/questions/4525854/remove-trailing-zeros/7983330#7983330
       
    }
}
