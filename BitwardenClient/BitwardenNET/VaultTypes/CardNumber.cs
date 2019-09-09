using System;
using System.Collections.Generic;
using System.Text;

namespace BitwardenNET.VaultTypes
{
    public class CardNumber
    {
        private const int CardNumberCount = 16;

        public byte[] Numbers { get; private set; } = new byte[CardNumberCount];

        public CardNumber(string jsonString)
        {
            ParseJsonString(jsonString);
        }

        public string GetJsonString()
        {
            StringBuilder sb = new StringBuilder(CardNumberCount);
            for (int i = 0; i < CardNumberCount; i++)
            {
                sb.Append(Numbers[i]);
            }
            return sb.ToString();
        }

        public bool ParseJsonString(string jsonNumbers)
        {
            jsonNumbers=jsonNumbers.Replace(" ", string.Empty);
            for (int i = 0; i < CardNumberCount; i++)
            {
                
                if (byte.TryParse(jsonNumbers[i].ToString(), out byte number))
                {
                    Numbers[i] = number;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
