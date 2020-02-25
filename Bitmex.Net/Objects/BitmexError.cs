using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitmex.Net.Client.Objects
{
    public class BitmexError : Error
    {
        public BitmexError(int code, string message, object data) : base(code, message, data)
        {
        }
    }
    //public class Error
    //{
    //    public string message { get; set; }
    //    public string name { get; set; }
    //}

    //public class RootObject
    //{
    //    public Error error { get; set; }
    //}
}
