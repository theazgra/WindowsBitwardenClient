﻿using BitwardenNET.CliInterop;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitwardenNET.BwLogic
{
    internal interface IBitwardenLogic
    {
        bool Login(BitwardenCredentials credentials);

        bool IsUserLogged(out string loggedUserEmail);
        
        bool Logout();

        bool UnlockVault(BitwardenCredentials credentials);

        bool LockVault();
    }
}
