﻿using BitwardenNET.CliInterop;
using BitwardenNET.VaultTypes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BitwardenNET.BwLogic
{
    internal interface IBitwardenLogic
    {
        bool Login(BitwardenCredentials credentials);

        bool IsUserLogged(out string loggedUserEmail);

        bool Logout();

        bool UnlockVault(BitwardenCredentials credentials);

        bool LockVault();

        bool SyncVault(BitwardenCredentials credentials);

        VaultData GetVaultData(BitwardenCredentials credentials);

        Task<VaultData> GetVaultDataAsync(BitwardenCredentials credentials);

        string GenerateString(short len, bool uc, bool lc, bool n, bool sc);
    }
}
