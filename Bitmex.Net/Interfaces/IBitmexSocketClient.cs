using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bitmex.Net.Client.Interfaces
{
    public interface IBitmexSocketClient
    {
        BitmexSocketStream MainSocketStreams { get; }
        BitmexSocketStream NonTradeSocketStreams { get; }

    }
}