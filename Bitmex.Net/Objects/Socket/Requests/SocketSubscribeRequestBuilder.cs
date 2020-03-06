using System;
using System.Collections.Generic;
using System.Text;

namespace Bitmex.Net.Client.Objects.Socket.Requests
{
    public static class SocketSubscribeRequestBuilder
    {      
        public static BitmexSubscribeRequest CreateEmptySubscribeRequest()
        {
            return new BitmexSubscribeRequest();
        }
        //public static BitmexSubscribeRequest Subscribe(this BitmexSubscribeRequest request,  string symbol = null)
        //{

        //    request.AddSubscribtion(PositionEndpoint, symbol);
        //    return request;
        //}
    }
}
