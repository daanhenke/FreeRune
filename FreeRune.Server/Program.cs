using System;
using System.Threading;
using FreeRune.Server.Game;

namespace FreeRune.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            GameServer gameServer = new GameServer();
            gameServer.Port = 43594;
            gameServer.Listen();

            while (gameServer.IsRunning)
            {
                Thread.Sleep(100);
            }
        }
    }
}