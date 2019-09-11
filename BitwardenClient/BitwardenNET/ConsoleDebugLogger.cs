using System;
using System.Collections.Generic;
using System.Text;

namespace BitwardenNET
{
    internal static class ConsoleDebugLogger
    {
        internal static void LogError(string err)
        {
#if DEBUG
            if (string.IsNullOrWhiteSpace(err))
                return;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error: " + err);
            Console.ForegroundColor = ConsoleColor.White;
#endif
        }

        internal static void Log(string msg)
        {
#if DEBUG
            if (string.IsNullOrWhiteSpace(msg))
                return;

            Console.WriteLine("Log: " + msg);
#endif
        }
    }
}
