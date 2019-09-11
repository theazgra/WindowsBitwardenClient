using Newtonsoft.Json;
using System.Collections.Generic;

namespace BitwardenNET.VaultTypes
{
    public class VaultData
    {
        [JsonProperty("folders")]
        public IEnumerable<VaultFolder> VaultFolders { get; set; }

        [JsonProperty("items")]
        public IEnumerable<VaultItem> VaultItems { get; set; }
    }
}
