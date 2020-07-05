using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketsChat3.Services;

namespace WebSocketsChat3.Controllers
{
    public class StreamController : Controller
    {
        private WebSocketsHandler _handler;

        public StreamController(WebSocketsHandler handler)
        {
            _handler = handler;
        }


        public async Task Get(string username)
        {
            var context = ControllerContext.HttpContext;
            var isWebSocketRequest = context.WebSockets.IsWebSocketRequest;

            if (isWebSocketRequest)
            {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                await _handler.HandleAsync(username, webSocket);
                //await SendMessage(webSocket);
            }
            else
            {
                context.Response.StatusCode = 400;
            }
        }

        private async Task SendMessage(WebSocket webSocket)
        {
            for (int i = 0; ; i++)
            {
                var bytes = Encoding.ASCII.GetBytes("Message" + i);
                await webSocket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
                Thread.Sleep(1000);
            }
        }
    }
}
