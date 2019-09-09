using BitwardenNET.VaultTypes;
using Newtonsoft.Json;
using System;

namespace BitwardenNET.CustomJsonConverters
{
    internal class VaultItemTypeJsonConverter : JsonConverter<VaultItemType>
    {
        public override VaultItemType ReadJson(JsonReader reader, Type objectType, VaultItemType existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            long typeId = (long)reader.Value;
            VaultItemType result = (VaultItemType)typeId;
            return result;
        }

        public override void WriteJson(JsonWriter writer, VaultItemType value, JsonSerializer serializer)
        {
            int typeId = (int)value;
            writer.WriteValue(typeId.ToString());
        }
    }
}
