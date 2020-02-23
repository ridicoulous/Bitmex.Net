using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitmex.Net
{
    public class BitmexClientOptions : RestClientOptions
    {
        public BitmexClientOptions(bool isTest=false) : base("https://www.bitmex.com/api/v1")
        {
            if (isTest)
            {
                BaseAddress = "https://testnet.bitmex.com/api/v1";
            }
        }
    }
}
