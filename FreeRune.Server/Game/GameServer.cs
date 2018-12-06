using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using FreeRune.Server.Common;
using FreeRune.Server.Game.Request;
using FreeRune.Server.Game.Response;
using FreeRune.Server.Runescape;

namespace FreeRune.Server.Game
{
    public class GameServer : AbstractAsyncronousSocketServer
    {
        protected override async Task HandleConnection(TcpClient client)
        {
            // Create our stream reader & writer
            NetworkStream stream = client.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            BinaryWriter writer = new BinaryWriter(stream);

            Logger.Info("Accepted connection!");

            // Read the RSConnectionReason
            byte connectionReason = reader.ReadByte();

            // Handle the connection based on the RSConnectionReason
            switch ((RSConnectionReason) connectionReason)
            {
                case RSConnectionReason.Login:
                    Logger.Verbose("Login request");

                    // Read the initial handshake
                    LoginHandshakeRequest handshakeRequest;
                    handshakeRequest.UserNameHash = reader.ReadByte();
                    
                    // Write our response and send it to the client
                    LoginHandshakeResponse handshakeResponse = new LoginHandshakeResponse(RSLoginResponseCode.ExchangeInformation, 110);
                    BinaryUtilities.WriteStructure<LoginHandshakeResponse>(writer, handshakeResponse);
                    writer.Flush();

                    // Read the actual login request
                    LoginDataRequest dataRequest;
                    dataRequest.ConnectionStatus = reader.ReadByte();
                    dataRequest.Size1 = reader.ReadByte();
                    dataRequest.Magic1 = reader.ReadByte();
                    dataRequest.ProtocolVersion = IPAddress.NetworkToHostOrder(reader.ReadInt16());
                    dataRequest.ClientVersion = reader.ReadByte();

                    dataRequest.CRC = new int[9];
                    for (int i = 0; i < dataRequest.CRC.Length; i++)
                    {
                        dataRequest.CRC[i] = IPAddress.NetworkToHostOrder(reader.ReadInt32());
                    }

                    dataRequest.Size2 = reader.ReadByte();
                    dataRequest.Magic2 = reader.ReadByte();
                    dataRequest.ClientSessionKey = IPAddress.NetworkToHostOrder(reader.ReadInt64());
                    dataRequest.ServerSessionKey = IPAddress.NetworkToHostOrder(reader.ReadInt64());
                    dataRequest.UserID = IPAddress.NetworkToHostOrder(reader.ReadInt32());
                    dataRequest.UserName = BinaryUtilities.ReadRSString(reader);
                    dataRequest.Password = BinaryUtilities.ReadRSString(reader);
                    
                    Logger.Info($"Username: {dataRequest.UserName}");
                    Logger.Info($"Password: {dataRequest.Password}");
                    
                    break;
                
                default:
                    int printableReason = connectionReason;
                    Logger.Error($"Unknown RSConnectionReason: {printableReason.ToString()}");
                    break;
            }
        }
    }
}