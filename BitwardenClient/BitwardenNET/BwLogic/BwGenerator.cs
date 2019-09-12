using System;
using System.Collections.Generic;
using System.Text;

namespace BitwardenNET.BwLogic
{
    public class BwGenerator
    {
        /// <summary>
        /// Include upper case characters in the generated string.
        /// </summary>
        public bool IncludeUpperCase { get; set; } = true;

        /// <summary>
        /// Include lower case characters in the generated string.
        /// </summary>
        public bool IncludeLowerCase{ get; set; } = true;

        /// <summary>
        /// Include numbers in the generated string.
        /// </summary>
        public bool IncludeNumbers { get; set; } = true;

        /// <summary>
        /// Include special characters in the generated string.
        /// </summary>
        public bool IncludeSpecialCharacters { get; set; } = true;

        private IBitwardenLogic _logic = new BitwardenCliLogic();

        /// <summary>
        /// Generate random string with given length.
        /// </summary>
        /// <param name="generatedStringLen">Lenght of the generated string.</param>
        /// <returns>Random generated string.</returns>
        public string GenerateString(short generatedStringLen)
        {
            if (generatedStringLen < 5)
            {
                ConsoleDebugLogger.LogError("[GenerateString] generatedStringLen is too small, < 5.");
                return string.Empty;
            }
            string generated = _logic.GenerateString(generatedStringLen, IncludeUpperCase, IncludeLowerCase, IncludeNumbers, IncludeSpecialCharacters);
            if (!string.IsNullOrEmpty(generated))
            {
                return generated;
            }
            else
            {
                ConsoleDebugLogger.LogError("[GenerateString] Failed to generate random string");
                return generated;
            }
        }
    }
}
