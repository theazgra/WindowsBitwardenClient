using System;
using BitwardenNET.CliInterop;
using System.Collections.Generic;
using System.Text;

namespace BitwardenNET.BwLogic
{
    internal class BitwardenCliLogic : IBitwardenLogic
    {
        #region CommandLineConstants
        internal static readonly CliFlag BwRawFlag = CliFlag.Flag("raw");
        private const string LoginCommand = "login";
        private const string LogoutCommand = "logout";
        private const string UnlockCommand = "unlock";
        private const string LockCommand = "lock";
        private const string AlreadyLoggedMessage = "You are already logged in as ";
        private const int SuccessExitCode = 0;
        #endregion

        public bool Login(BitwardenCredentials credentials)
        {
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
                credentials.SessionCode = result.StandardOutput;
                return true;
            }
            else
            {
                ConsoleLogger.LogError(result.StandardError);
                return false;
            }
        }

        public bool IsUserLogged(out string loggedUserEmail)
        {
            loggedUserEmail = string.Empty;

            int originalTimeout = BitwardenCliInterface.ProcessTimeout;
            BitwardenCliInterface.ProcessTimeout = 1000;
            BitwardenCliCommandResult result = BitwardenCliInterface.ExecuteCommand(LoginCommand);
            BitwardenCliInterface.ProcessTimeout = originalTimeout;
            ConsoleLogger.LogError(result.StandardError);
            // We are expecting exit code of 1.
            if (result.ExitCode == 1 && result.StandardOutput.StartsWith(AlreadyLoggedMessage))
            {
                loggedUserEmail = result.StandardOutput.Substring(AlreadyLoggedMessage.Length).TrimEnd('.');
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UnlockVault(BitwardenCredentials credentials)
        {
            CliFlag[] flags =
            {   
                CliFlag.StringValue(credentials.Password),
                BwRawFlag
            };

            BitwardenCliCommandResult result = BitwardenCliInterface.ExecuteCommand(UnlockCommand, flags);
            ConsoleLogger.Log($"UnlockVault exited with code {result.ExitCode}");
            ConsoleLogger.Log(result.StandardOutput);
            if (!result.TimedOut && result.ExitCode == SuccessExitCode)
            {
                credentials.SessionCode = result.StandardOutput;
                return true;
            }
            else
            {
                ConsoleLogger.LogError(result.StandardError);
                return false;
            }
        }

        public bool LockVault()
        {
            BitwardenCliCommandResult result = BitwardenCliInterface.ExecuteCommand(LockCommand, BwRawFlag);
            ConsoleLogger.LogError(result.StandardError);
            return (!result.TimedOut && result.ExitCode == SuccessExitCode);
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
