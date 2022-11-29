using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Bitmex.Net.Client.Interfaces;
using Bitmex.Net.Client.Objects.Socket;
using Bitmex.Net.Client.Objects.Socket.Repsonses;
using Bitmex.Net.Client.Objects.Socket.Requests;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bitmex.Net.Client
{
    public class BitmexSocketClient : BaseSocketClient, IBitmexSocketClient
    {

        public BitmexSocketClient() : this(BitmexSocketClientOptions.Default.Copy())
        {
        }
        public BitmexSocketClient(BitmexSocketClientOptions options) : base("Bitmex", options)
        {
            SocketStreams = AddApiClient(new BitmexSocketStream(log, this, options));
        }
        public BitmexSocketStream SocketStreams { get; set; }

    }
}