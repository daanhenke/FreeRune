using System.Runtime.InteropServices;

namespace FreeRune.Server.Runescape
{
    public enum RSLoginResponseCode : byte
    {
        RetryAndCount = 1,
        ExchangeInformation = 0,
        Retry = 1,
        SuccessfulLogin = 2,
        InvalidUsernameOrPassword = 3
    }
}