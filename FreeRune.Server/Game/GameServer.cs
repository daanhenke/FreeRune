using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using FreeRune.Server.Common;

namespace FreeRune.Server.Game
{
    public class GameServer : AbstractAsyncronousSocketServer
    {
        protected override async Task HandleConnection(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            BinaryWriter writer = new BinaryWriter(stream);
            
            Console.WriteLine("I need a good logger, also someone connected lol");
        }
    }
}