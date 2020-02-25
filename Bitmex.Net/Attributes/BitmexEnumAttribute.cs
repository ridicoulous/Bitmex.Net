using System;
using System.Collections.Generic;
using System.Text;

namespace Bitmex.Net.Client.Attributes
{
    public class BitmexEnumAttribute:Attribute
    {
        public readonly string BitmexValue;
        public BitmexEnumAttribute()
        {

        }
        public BitmexEnumAttribute(string name)
        {
            BitmexValue = name;
        }
    }
}
