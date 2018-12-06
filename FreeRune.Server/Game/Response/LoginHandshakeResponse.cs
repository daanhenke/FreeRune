using System.Reflection.Emit;
using System.Runtime.InteropServices;
using FreeRune.Server.Runescape;

namespace FreeRune.Server.Game.Response
{
    [StructLayout(LayoutKind.Sequential)]
    public struct LoginHandshakeResponse
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] IgnoredBytes;
        public byte ResponseCode;
        public long ServerSessionKey;

        public LoginHandshakeResponse(RSLoginResponseCode responseCode, long serverSessionKey)
        {
            IgnoredBytes = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                IgnoredBytes[i] = 0;
            }
            
            ResponseCode = (byte) responseCode;
            ServerSessionKey = serverSessionKey;
        }
    }
}