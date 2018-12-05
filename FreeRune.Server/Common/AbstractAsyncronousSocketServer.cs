using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace FreeRune.Server.Common
{
    public abstract class AbstractAsyncronousSocketServer
    {
        public IPAddress Address;
        public int Port;

        public bool IsRunning;

        public TcpListener Listener;
        
        public AbstractAsyncronousSocketServer()
        {
            Address = IPAddress.Any;
            Port = 0;

            IsRunning = false;
            
            Listener = null;
        }

        public async void Listen()
        {
            Listener = new TcpListener(Address, Port);
            Listener.Start();

            IsRunning = true;
            while (IsRunning)
            {
                TcpClient client = await Listener.AcceptTcpClientAsync();
                
                Task task = HandleConnection(client);
                await task;
            }
        }

        abstract protected Task HandleConnection(TcpClient client);
    }
}