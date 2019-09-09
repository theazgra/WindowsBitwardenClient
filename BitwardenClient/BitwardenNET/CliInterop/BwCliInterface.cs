using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace BitwardenNET.CliInterop
{
    internal static class BwCliInterface
    {
        private static readonly string Bitwarden_CLI_Binary = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BitwardenCliBinary", "bw.exe");

        internal BwCliCommandResult ExecuteCommand(string command, params CliFlag[] flags)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo(Bitwarden_CLI_Binary)
            {
                CreateNoWindow = true,
                UseShellExecute = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                Arguments = ConstructArguments(command, flags)
            };


        }

        private static string ConstructArguments(string command, CliFlag[] flags)
        {
            StringBuilder argumentBuilder = new StringBuilder(command + " ");

            foreach (CliFlag flag in flags)
            {
                argumentBuilder.Append(flag.ToString() + " ");
            }

            return argumentBuilder.ToString();
        }
    }
}