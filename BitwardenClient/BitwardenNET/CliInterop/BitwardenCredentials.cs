using System;
using System.Collections.Generic;
using System.Text;

namespace BitwardenNET.CliInterop
{
    internal class BitwardenCredentials
    {
        /// <summary>
        /// User email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User's master password.
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// 2FA authentication code if 2FA is enabled.
        /// </summary>
        public string AuthCode { get; set; }

        /// <summary>
        /// Simple credentials without 2FA.
        /// </summary>
        public BitwardenCredentials(string email, string password)
        {
            Email = email;
            Password = password;
        }

        /// <summary>
        /// Credentials with 2FA.
        /// </summary>
        public BitwardenCredentials(string email, string password, string authCode)
        {
            Email = email;
            Password = password;
            AuthCode = authCode;
        }
    }
}
