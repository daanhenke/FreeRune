using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace FreeRune.Server.Common
{
    public enum LogLevel
    {
        VERBOSE,
        INFO,
        ERROR
    }

    public class Logger
    {
        public static LogLevel CurrentLogLevel = LogLevel.ERROR;

        public static void Raw(LogLevel level, string memberName, string message)
        {
            if (level >= CurrentLogLevel)
            {
                string levelString = Enum.GetName(typeof(LogLevel), level);
                Console.Write($"[{memberName}] {levelString}: ");
                Console.WriteLine(message);
            }
        }

        public static void Verbose(string message, [CallerMemberName] string memberName = "")
        {
            Raw(LogLevel.VERBOSE, memberName, message);
        }

        public static void Info(string message, [CallerMemberName] string memberName = "")
        {
            Raw(LogLevel.INFO, memberName, message);
        }

        public static void Error(string message, [CallerMemberName] string memberName = "")
        {
            Raw(LogLevel.ERROR, memberName, message);
        }
    }
}