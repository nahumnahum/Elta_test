using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace SocketServer
{
    class Program
    {
        public class Echo : WebSocketBehavior
        {
            protected override void OnMessage(MessageEventArgs e)
            {
                Console.WriteLine("Received message from Echo client: " + e.Data);
                Send(e.Data);
            }
        }

        public class EchoAll : WebSocketBehavior
        {
            protected override void OnMessage(MessageEventArgs e)
            {
                Console.WriteLine("Received message from EchoAll client: " + e.Data);
                Sessions.Broadcast(e.Data);
            }
        }
        static void Main(string[] args)
        {
            WebSocketServer socketServer = new WebSocketServer("ws://127.0.0.1:7890");

            socketServer.AddWebSocketService<Echo>("/Echo");
            socketServer.AddWebSocketService<EchoAll>("/EchoAll");

            socketServer.Start();

            Console.WriteLine("Server started on ws://127.0.0.1g:7890/Echo");
            Console.WriteLine("Server started on ws://127.0.0.1g:7890/EchoAll");

            Console.ReadKey();

            socketServer.Stop();
        }
    }
}