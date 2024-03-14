using System;
using WebSocketSharp;

namespace SocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebSocket webSocket = new WebSocket("ws://127.0.0.1:7890/Echo"))
            {
                webSocket.OnMessage += webSocket_OnMessage;

                webSocket.Connect();

                webSocket.Send("Hello Server!");

                Console.ReadKey();
            }
        }

        private static void webSocket_OnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine("Received from the server: " + e.Data);
        }
    }
}