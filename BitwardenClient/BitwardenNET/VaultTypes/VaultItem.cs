﻿using System;
using System.Collections.Generic;
using System.Text;
using BitwardenNET.CustomJsonConverters;
using BitwardenNET.VaultTypes.ItemTypes;
using Newtonsoft.Json;

namespace BitwardenNET.VaultTypes
{
    public class VaultItem : IVaultObject
    {
        #region IVaultItemsInterface

        [JsonProperty("id")]
        public Guid? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public VaultObjectType ObjectType => VaultObjectType.Item;

        [JsonProperty("folderId",DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Guid? FolderId { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(VaultItemTypeJsonConverter))]
        public VaultItemType ItemType { get; set; }

        [JsonProperty("fields")]
        public IList<CustomField> CustomFields { get; set; }

        [JsonProperty("revisionDate")]
        public DateTime RevisionDate { get; set; }
        
        [JsonProperty("notes")]
        public string Note { get; set; }

        [JsonProperty("favorite")]
        public bool IsFavorite { get; set; }
        #endregion

        /// VaultItem is of one <see cref="VaultItemType"/> that means that that one property is set.

        [JsonProperty("card")]
        public ItemCard Card { get; set; }

        [JsonProperty("identity")]
        public ItemIdentity Identity { get; set; }

        [JsonProperty("login")]
        public ItemLogin Login { get; set; }
        
        [JsonProperty("secureNote")]
        public ItemSecureNote SecureNote { get; set; }

        public override string ToString()
        {
            switch (ItemType)
            {
                case VaultItemType.Login:
                    return $"VaultItem; Type: Login; Name: {Name}; Id: {Id}; FolderId: {FolderId}";
                case VaultItemType.Note:
                    return $"VaultItem; Type: SecureNote; Name: {Name}; Id: {Id}; FolderId: {FolderId}";
                case VaultItemType.Card:
                    return $"VaultItem; Type: Card; Name: {Name}; Id: {Id}; FolderId: {FolderId}";
                case VaultItemType.Identity:
                    return $"VaultItem; Type: Identity; Name: {Name}; Id: {Id}; FolderId: {FolderId}";
            }
            return base.ToString();
        }
    }
}
