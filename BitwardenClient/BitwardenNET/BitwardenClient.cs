using BitwardenNET.BwLogic;
using BitwardenNET.CliInterop;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitwardenNET
{
    public class BitwardenClient : IDisposable
    {
        private bool _unlocked = false;
        private BitwardenCredentials _credentials;
        private bool _stayLogged = false;
        private string _sessionCode = null;

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
        }

        public BitwardenClient(string email, string password, string authCode, bool stayLogged)
        {
            _credentials = new BitwardenCredentials(email, password, authCode);
            _stayLogged = stayLogged;
        }

        public bool Login()
        {
            return _logic.Login(_credentials, out _sessionCode);
        }

        public bool UnlockVault()
        {
            return _logic.UnlockVault(_credentials);
        }

        public bool Logout()
        {
            return _logic.Logout();
        }

        public bool LockVault()
        {
            return _logic.LockVault();
        }

        public void Dispose()
        {
            if (_unlocked)
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
