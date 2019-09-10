using BitwardenNET.BwLogic;
using BitwardenNET.CliInterop;
using System;
using System.Collections.Generic;
using System.Text;

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
            IsUserLoggedIn = _logic.Login(_credentials);
            return IsUserLoggedIn;
        }

        /// <summary>
        /// Logout the user from the bitwarden.
        /// </summary>
        /// <returns>True if user was logged out.</returns>
        public bool Logout()
        {
            IsUserLoggedIn = !_logic.Logout();

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
                ConsoleLogger.LogError("Cann't unlock the vault because the user is not logged in.");
                return false;
            }
            IsVaultUnlocked = _logic.UnlockVault(_credentials);
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
            return !IsVaultUnlocked;
        }

        public void Dispose()
        {
            if (IsVaultUnlocked)
            {
                _logic.LockVault();
            }
            if (!_stayLogged)
            {
                _logic.Logout();
            }
        }
    }
}
