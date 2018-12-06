using System.Runtime.InteropServices;

namespace FreeRune.Server.Game.Request
{
    [StructLayout(LayoutKind.Sequential)]
    public struct LoginHandshakeRequest
    {
        public byte UserNameHash;
    }
}