using System;
using System.Collections.Generic;
using System.Text;

namespace Bitmex.Net.Client.Attributes
{
    /// <summary>
    /// Use it for prevent sending marked properties to bitmex api
    /// </summary>
    public class BitmexRequestIgnoreAttribute : Attribute
    {
    }
}
