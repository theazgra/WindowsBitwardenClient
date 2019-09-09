using System;
using BitwardenNET.CliInterop;
using System.Collections.Generic;
using System.Text;

namespace BitwardenNET.BwLogic
{
    internal class BitwardenCliLogic : IBitwardenLogic
    {
        internal static readonly CliFlag BwRawFlag = CliFlag.Flag("raw");
        private const string LoginCommand = "login";
        private const string LogoutCommand = "logout";
        private const string UnlockCommand = "unlock";
        private const string LockCommand = "lock";
        private const int SuccessExitCode = 0;

        public bool Login(BitwardenCredentials credentials, out string sessionCode)
        {
            sessionCode = null;
            CliFlag[] flags;
            if (!string.IsNullOrWhiteSpace(credentials.AuthCode))
            {
                // Use 2FA code.
                flags = new CliFlag[4]
                {
                    CliFlag.StringValue(credentials.Email),
                    CliFlag.StringValue(credentials.Password),
                    CliFlag.FlagWithValue("--code", credentials.AuthCode),
                    BwRawFlag
                };
            }
            else
            {
                flags = new CliFlag[3]
                {
                    CliFlag.StringValue(credentials.Email),
                    CliFlag.StringValue(credentials.Password),
                    BwRawFlag
                };
            }

            BitwardenCliCommandResult result = BitwardenCliInterface.ExecuteCommand(LoginCommand, flags);
            ConsoleLogger.Log($"Login exited with code {result.ExitCode}");
            ConsoleLogger.Log(result.StandardOutput);
            if (!result.TimedOut && result.ExitCode == SuccessExitCode)
            {
                sessionCode = result.StandardOutput;
            }
            else
            {
                ConsoleLogger.LogError(result.StandardError);
            }
            
            return result.TimedOut;
        }

        public bool UnlockVault(BitwardenCredentials credentials)
        {
            throw new NotImplementedException();
        }

        public bool LockVault()
        {
            throw new NotImplementedException();
        }


        public bool Logout()
        {
            BitwardenCliCommandResult result = BitwardenCliInterface.ExecuteCommand(LogoutCommand);
            ConsoleLogger.Log($"Logout exited with code {result.ExitCode}");
            ConsoleLogger.Log(result.StandardOutput);
            
            if (result.TimedOut || result.ExitCode != SuccessExitCode)
            {
                ConsoleLogger.LogError(result.StandardError);
                return false;
            }
            else
            {
                return true;   
            }
        }
    }
}
