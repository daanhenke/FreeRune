using System;
using System.Threading;
using FreeRune.Server.Common;
using FreeRune.Server.Game;

namespace FreeRune.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set the log level to verbose, we're still in development so this is a semi sane default
            Logger.CurrentLogLevel = LogLevel.VERBOSE;
            
            // Create & run an async game server
            GameServer gameServer = new GameServer();
            gameServer.Port = 43594;
            gameServer.Listen();
            
            // TODO: Implement a JAGGRAB server

            // Sleep until all servers are done executing
            while (gameServer.IsRunning)
            {
                Thread.Sleep(100);
            }
        }
    }
}