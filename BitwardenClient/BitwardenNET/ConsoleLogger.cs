using System;
using System.Collections.Generic;
using System.Text;

namespace BitwardenNET
{
    internal static class ConsoleLogger
    {
        internal static void LogError(string err)
        {
            if (string.IsNullOrWhiteSpace(err))
                return;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error:");
            Console.WriteLine(err);
            Console.ForegroundColor = ConsoleColor.White;
        }

        internal static void Log(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg))
                return;
            
            Console.WriteLine("Log:");
            Console.WriteLine(msg);
        }
    }
}
