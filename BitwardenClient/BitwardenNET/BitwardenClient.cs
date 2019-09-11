using BitwardenNET.BwLogic;
using BitwardenNET.CliInterop;
using BitwardenNET.VaultTypes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BitwardenNET
{
    public class BitwardenClient : IDisposable
    {
        private BitwardenCredentials _credentials;
        private bool _stayLogged = false;

        /// <summary>
        /// True if some user is logged in the bitwarden.
        /// </summary>
        public bool IsUserLoggedIn { get; private set; } = false;

        /// <summary>
        /// Email of the logged user.
        /// </summary>
        public string UserEmail { get; private set; } = string.Empty;

        /// <summary>
        /// True if the user vault is unlocked.
        /// </summary>
        public bool IsVaultUnlocked { get; private set; } = false;


        /// <summary>
        /// The logic is currently implemented through bitwarden-cli tool,
        /// this is because there is no official API documentation for the 
        /// free version REST API.
        /// 
        /// In the future we would like to replace the cli tool with the REST API.
        /// </summary>
        private IBitwardenLogic _logic = new BitwardenCliLogic();

        public BitwardenClient(string email, string password, bool stayLogged)
        {
            _credentials = new BitwardenCredentials(email, password);
            _stayLogged = stayLogged;
            UserEmail = email;
        }

        public BitwardenClient(string email, string password, string authCode, bool stayLogged)
        {
            _credentials = new BitwardenCredentials(email, password, authCode);
            _stayLogged = stayLogged;
            UserEmail = email;
        }

        /// <summary>
        /// Login user to the bitwarden. This does not unlock the vault.
        /// </summary>
        /// <returns>True if login was successful.</returns>
        public bool Login()
        {
            // Check if someone is already logged.
            if (CheckForLoggedUser())
            {
                // If someone is logged and it is different user logout him and chage UserEmail.
                if (UserEmail != _credentials.Email)
                {
                    Logout();
                    UserEmail = _credentials.Email;
                }
                else
                {
                    ConsoleDebugLogger.Log($"User {UserEmail} is already logged in.");
                    return true;
                }
            }

            IsUserLoggedIn = _logic.Login(_credentials);
            ConsoleDebugLogger.Log(string.Format("Login {0}", IsUserLoggedIn ? "successful" : "failed"));
            return IsUserLoggedIn;
        }

        /// <summary>
        /// Logout the user from the bitwarden.
        /// </summary>
        /// <returns>True if user was logged out.</returns>
        public bool Logout()
        {
            IsUserLoggedIn = !_logic.Logout();
            ConsoleDebugLogger.Log(string.Format("Logout {0}", !IsUserLoggedIn ? "successful" : "failed"));
            
            if (!IsUserLoggedIn && IsVaultUnlocked)
            {
                IsVaultUnlocked = false;
                UserEmail = string.Empty;
            }
            return !IsUserLoggedIn;
        }

        /// <summary>
        /// Unlock the user vault.
        /// </summary>
        /// <returns>True if the vault was unlocked.</returns>
        public bool UnlockVault()
        {
            if (!IsUserLoggedIn)
            {
                ConsoleDebugLogger.LogError("Cann't unlock the vault because the user is not logged in.");
                return false;
            }
            IsVaultUnlocked = _logic.UnlockVault(_credentials);
            ConsoleDebugLogger.Log(string.Format("Unlocking vault {0}", IsVaultUnlocked ? "successful" : "failed"));

            return IsVaultUnlocked;
        }

        /// <summary>
        /// Check if some user is already logged into the bitwarden. See <see cref="UserEmail"/> for logged user.
        /// </summary>
        /// <returns>True is some user is logged in.</returns>
        public bool CheckForLoggedUser()
        {
            IsUserLoggedIn = _logic.IsUserLogged(out string loggedEmail);
            UserEmail = loggedEmail;
            return IsUserLoggedIn;
        }


        /// <summary>
        /// Lock the user vault.
        /// </summary>
        /// <returns>True if the vault was locked.</returns>
        public bool LockVault()
        {
            IsVaultUnlocked = !_logic.LockVault();
            ConsoleDebugLogger.Log(string.Format("Locking vault {0}", !IsVaultUnlocked ? "successful" : "failed"));
            return !IsVaultUnlocked;
        }

        public async Task<AsyncResult<VaultData>> GetVaultDataAsync()
        {
            AsyncResult<VaultData> result = new AsyncResult<VaultData>();
            if (!CheckUnlockedVault())
            {
                result.ErrorMessage = "The user is not logged in or the vault is not unlocked";
                return result;
            }

            result.Result = await _logic.GetVaultDataAsync(_credentials);
            result.Success = result.Result != null;
            return result;
        }


        private bool CheckUnlockedVault()
        {
            if (!IsUserLoggedIn)
            {
                ConsoleDebugLogger.LogError("[CheckUnlockedVault] The user is not logged in.");
                return false;
            }

            if (!IsVaultUnlocked)
            {
                ConsoleDebugLogger.LogError("[CheckUnlockedVault] The vault is not unlocked.");
                return false;
            }
            return true;
        }

        public void Dispose()
        {
            ConsoleDebugLogger.Log("Disposing BitwardenClient");
            if (IsVaultUnlocked)
            {
                ConsoleDebugLogger.Log("[Dispose] Locking unlocked vault.");
                _logic.LockVault();
            }
            if (!_stayLogged)
            {
                ConsoleDebugLogger.Log("[Dispose] Logging off (_stayLogged = false).");
                _logic.Logout();
            }
        }
    }
}
