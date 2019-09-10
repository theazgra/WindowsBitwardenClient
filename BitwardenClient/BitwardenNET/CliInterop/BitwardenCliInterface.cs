using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BitwardenNET.CliInterop
{
    internal static class BitwardenCliInterface
    {
        private static readonly string Bitwarden_CLI_Binary = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BitwardenCliBinary", "bw.exe");
        internal static int ProcessTimeout = 30_000;

        internal static BitwardenCliCommandResult ExecuteCommand(string command, params CliFlag[] flags)
        {
            BitwardenCliCommandResult result = new BitwardenCliCommandResult();

            ProcessStartInfo processStartInfo = new ProcessStartInfo(Bitwarden_CLI_Binary)
            {
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute=false,
                Arguments = ConstructArguments(command, flags),
            };

            Process bwProcess = Process.Start(processStartInfo);
            result.TimedOut = !bwProcess.WaitForExit(ProcessTimeout);

            if (result.TimedOut)
            {
                bwProcess.Kill();
            }
            else
            {
                result.StandardOutput = bwProcess.StandardOutput.ReadToEnd();
                result.StandardError = bwProcess.StandardError.ReadToEnd();
                result.ExitCode = bwProcess.ExitCode;
            }
            
            return result;
        }

        internal static Task<BitwardenCliCommandResult> ExecuteCommandAsyc(string command, params CliFlag[] flags)
        {
            return new Task<BitwardenCliCommandResult>(() => ExecuteCommand(command, flags));
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