using System;
using System.Collections.Generic;
using System.Text;

namespace Bitmex.Net.Client.Objects.Socket
{
   
    /// <summary>
    /// Message which is used as base for every request and response
    /// </summary>
    public class BitmexBaseMessage
    {
        /// <summary>
        /// Unique operation, is serialized as "op": "command"
        /// </summary>
        public virtual MessageType Op { get; set; }
    }
}
