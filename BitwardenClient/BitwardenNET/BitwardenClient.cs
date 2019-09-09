using BitwardenNET.CliInterop;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitwardenNET
{
    public class BitwardenClient : IDisposable
    {
        private bool _unlocked = false;
        private BwCredentials _credentials;
        private bool _stayLogged = false;

        public BitwardenClient(string email, string password, bool stayLogged)
        {
            _credentials = new BwCredentials(email, password);
            _stayLogged = stayLogged;
        }

        public BitwardenClient(string email, string password, string authCode, bool stayLogged)
        {
            _credentials = new BwCredentials(email, password, authCode);
            _stayLogged = stayLogged;
        }

        public bool Login()
        {
            throw new NotImplementedException();
        }

        public bool UnlockVault()
        {
            throw new NotImplementedException();
        }

        public bool Logout()
        {
            throw new NotImplementedException();
        }

        public bool LockVault()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (_unlocked)
            {
                LockVault();
            }
            if (!_stayLogged)
            {
                Logout();
            }
        }
    }
}
