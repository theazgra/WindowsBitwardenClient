using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BitwardenNET.VaultTypes.ItemTypes
{
    public class ItemLogin
    {
        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("totp")]
        public string TOTP { get; set; }

        [JsonProperty("passwordRevisionDate", DefaultValueHandling = DefaultValueHandling.Populate)]
        public DateTime? PasswordRevisionDate { get; set; }

        [JsonProperty("uris")]
        public List<LoginUri> Uris { get; set; }
    }
}
