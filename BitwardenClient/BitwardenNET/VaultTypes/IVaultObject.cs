using System;

namespace BitwardenNET.VaultTypes
{
    public interface IVaultObject
    {
        /// <summary>
        /// Vault object id.
        /// </summary>
        Guid? Id { get; set; }
        /// <summary>
        /// Vault object name.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Vault object type, item, folder...
        /// </summary>
        VaultObjectType ObjectType { get; }
    }
}