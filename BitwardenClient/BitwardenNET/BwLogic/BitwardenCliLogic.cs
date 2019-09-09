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
            ConsoleLogger.LogError(result.StandardError);
            return result.Success;
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
            throw new NotImplementedException();
        }


    }
}
