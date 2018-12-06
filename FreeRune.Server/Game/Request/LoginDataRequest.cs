using System;
using System.Runtime.InteropServices;

namespace FreeRune.Server.Game.Request
{
    public struct LoginDataRequest
    {
        public byte ConnectionStatus;
        public byte Size1;
        public byte Magic1;
        public short ProtocolVersion;
        public byte ClientVersion;
        public int[] CRC;
        public byte Size2;
        public byte Magic2;
        public long ClientSessionKey;
        public long ServerSessionKey;
        public int UserID;
        public string UserName;
        public string Password;

    }
}