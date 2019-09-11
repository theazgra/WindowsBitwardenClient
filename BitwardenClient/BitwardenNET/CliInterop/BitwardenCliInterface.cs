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
        internal static int ProcessTimeout = 10_000;

        internal static BitwardenCliCommandResult ExecuteCommand(string command, params CliFlag[] flags)
        {
            BitwardenCliCommandResult result = new BitwardenCliCommandResult();

            ProcessStartInfo processStartInfo = new ProcessStartInfo(Bitwarden_CLI_Binary)
            {
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                Arguments = ConstructArguments(command, flags)
            };

            /*
                Process p = new Process();
                p.StartInfo = psi;
                p.OutputDataReceived += (sender, data) => Console.WriteLine("recv: " + data.Data);
                p.Start();
                p.BeginOutputReadLine();
                p.WaitForExit(); 
             */
            StringBuilder stdOutBuffer = new StringBuilder();
            StringBuilder stdErrBuffer = new StringBuilder();


            using (Process bwProcess = new Process() { StartInfo = processStartInfo })
            {
                bwProcess.OutputDataReceived += (sender, processData) => stdOutBuffer.Append(processData.Data);
                bwProcess.ErrorDataReceived += (sender, processData) => stdErrBuffer.Append(processData.Data);
                

                if (!bwProcess.Start())
                {
                    ConsoleDebugLogger.LogError("Failed to start bitwarden process.");
                    result.ExitCode = -1;
                    return result;
                }
                
                bwProcess.BeginOutputReadLine();
                result.TimedOut = !bwProcess.WaitForExit(ProcessTimeout);

                if (result.TimedOut)
                {
                    bwProcess.Kill();
                    ConsoleDebugLogger.LogError("Process timeout. Process was killed without result.");
                }
                else
                {
                    result.StandardOutput = stdOutBuffer.ToString();
                    result.StandardError = stdErrBuffer.ToString();
                    result.ExitCode = bwProcess.ExitCode;
                }
            }

            return result;
        }

        internal static BitwardenCliCommandResult ExecuteCommandWithoutIO(string command, params CliFlag[] flags)
        {
            BitwardenCliCommandResult result = new BitwardenCliCommandResult();

            ProcessStartInfo processStartInfo = new ProcessStartInfo(Bitwarden_CLI_Binary)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                Arguments = ConstructArguments(command, flags)
            };

            Process bwProcess = Process.Start(processStartInfo);
            result.TimedOut = !bwProcess.WaitForExit(ProcessTimeout);

            if (result.TimedOut)
            {
                bwProcess.Kill();
                ConsoleDebugLogger.LogError("Process timeout. Process was killed without result.");
            }
            else
            {
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