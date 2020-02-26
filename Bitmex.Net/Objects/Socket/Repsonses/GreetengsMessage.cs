using System;
using System.Collections.Generic;
using System.Text;

namespace Bitmex.Net.Client.Objects.Socket.Repsonses
{
    public class GreetingsMessage : BitmexBaseMessage
    {
        public string Info { get; set; }
        public DateTime? Version { get; set; }
        public DateTime? Timestamp { get; set; }
        public string Docs { get; set; }
        public Dictionary<string, object> Limit { get; set; }
    }

}
