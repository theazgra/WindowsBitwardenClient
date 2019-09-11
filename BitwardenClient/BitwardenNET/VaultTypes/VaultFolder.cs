using Newtonsoft.Json;
using System;

namespace BitwardenNET.VaultTypes
{
    public class VaultFolder : IVaultObject
    {

        [JsonProperty("id")]
        public Guid? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        public VaultObjectType ObjectType => VaultObjectType.Folder;

        public override string ToString()
        {
            return $"VaultFolder; Name: {Name}; Id: {Id}";
        }
    }
}
