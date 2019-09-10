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
        internal string Email { get; set; }

        /// <summary>
        /// User's master password.
        /// </summary>
        internal string Password { get; set; }

        /// <summary>
        /// 2FA authentication code if 2FA is enabled.
        /// </summary>
        internal string AuthCode { get; set; }

        /// <summary>
        /// Current session code.
        /// </summary>
        internal string SessionCode { get; set; }

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
