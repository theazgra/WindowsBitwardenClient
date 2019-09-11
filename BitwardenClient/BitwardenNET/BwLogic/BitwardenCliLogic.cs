using System;
using BitwardenNET.CliInterop;
using System.Collections.Generic;
using System.Text;
using BitwardenNET.VaultTypes;
using Newtonsoft.Json;
using System.IO;

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
        private const string ExportCommand = "export";
        private const string SyncCommand = "sync";
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
            if (!result.TimedOut && result.ExitCode == SuccessExitCode)
            {
                credentials.SessionCode = result.StandardOutput;
                return true;
            }
            else
            {
                ConsoleDebugLogger.LogError(result.StandardError);
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
            ConsoleDebugLogger.LogError(result.StandardError);
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
            if (!result.TimedOut && result.ExitCode == SuccessExitCode)
            {
                credentials.SessionCode = result.StandardOutput;
                return true;
            }
            else
            {
                ConsoleDebugLogger.LogError(result.StandardError);
                return false;
            }
        }

        public bool LockVault()
        {
            BitwardenCliCommandResult result = BitwardenCliInterface.ExecuteCommand(LockCommand, BwRawFlag);
            ConsoleDebugLogger.LogError(result.StandardError);
            return (!result.TimedOut && result.ExitCode == SuccessExitCode);
        }

        public bool Logout()
        {
            BitwardenCliCommandResult result = BitwardenCliInterface.ExecuteCommand(LogoutCommand);
            if (result.TimedOut || result.ExitCode != SuccessExitCode)
            {
                ConsoleDebugLogger.LogError(result.StandardError);
                return false;
            }
            else
            {
                return true;
            }
        }



        public bool SyncVault(BitwardenCredentials credentials)
        {
            if (!CheckSessionCode(credentials))
            {
                return false;
            }

            BitwardenCliCommandResult result = BitwardenCliInterface.ExecuteCommand(SyncCommand,
                BwRawFlag,
                CliFlag.FlagWithValue("session", credentials.SessionCode));

            return (result.ExitCode == SuccessExitCode && !result.TimedOut);
        }

        public VaultData GetVaultData(BitwardenCredentials credentials)
        {
            if (!CheckSessionCode(credentials))
            {
                return null;
            }

            string tmpFilePath = Path.GetTempFileName();
            Console.WriteLine("tmp file: " + tmpFilePath);

            try
            {
                BitwardenCliCommandResult result = BitwardenCliInterface.ExecuteCommandWithoutIO(ExportCommand,
                                                                                                BwRawFlag,
                                                                                                CliFlag.FlagWithValue("format", "json"),
                                                                                                CliFlag.FlagWithValue("output", tmpFilePath),
                                                                                                CliFlag.FlagWithValue("session", credentials.SessionCode),
                                                                                                CliFlag.StringValue(credentials.Password));

                if (result.TimedOut || result.ExitCode != SuccessExitCode)
                {
                    File.Delete(tmpFilePath);
                    return null;
                }

                string jsonExport = File.ReadAllText(tmpFilePath);
                File.Delete(tmpFilePath);

                try
                {
                    VaultData data = JsonConvert.DeserializeObject<VaultData>(jsonExport);
                    return data;
                }
                catch (JsonException ex)
                {
                    ConsoleDebugLogger.LogError("Failed JsonDeserialization in [GetVaultData]");
                    ConsoleDebugLogger.LogError(ex.Message);
                    return null;
                }
            }
            finally
            {
                if (File.Exists(tmpFilePath))
                {
                    File.Delete(tmpFilePath);
                }
            }
        }

        /// <summary>
        /// Check if session code is set.
        /// </summary>
        /// <param name="credentials">Credentials with session code.</param>
        /// <returns>True if valid session code is present.</returns>
        private bool CheckSessionCode(BitwardenCredentials credentials)
        {
            if (string.IsNullOrWhiteSpace(credentials.SessionCode))
            {
                ConsoleDebugLogger.LogError("Session code is not set.");
                return false;
            }
            return true;
        }
    }
}
