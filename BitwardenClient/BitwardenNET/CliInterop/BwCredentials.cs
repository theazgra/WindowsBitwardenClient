using System;
using System.Collections.Generic;
using System.Text;

namespace BitwardenNET.CliInterop
{
    internal class BwCredentials
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string AuthCode { get; set; }

        public BwCredentials(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public BwCredentials(string email, string password, string authCode)
        {
            Email = email;
            Password = password;
            AuthCode = authCode;
        }
    }
}
