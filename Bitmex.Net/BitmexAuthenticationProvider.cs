using CryptoExchange.Net.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitmex.Net
{
    public class BitmexAuthenticationProvider : AuthenticationProvider
    {
        public BitmexAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
        }
    }
}
