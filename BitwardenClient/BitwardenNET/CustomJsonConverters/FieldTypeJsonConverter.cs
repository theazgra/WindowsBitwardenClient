using BitwardenNET.VaultTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitwardenNET.CustomJsonConverters
{
    internal class FieldTypeJsonConverter : JsonConverter<CustomFieldType>
    {
        public override CustomFieldType ReadJson(JsonReader reader, Type objectType, CustomFieldType existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            long typeId = (long)reader.Value;
            CustomFieldType result = (CustomFieldType)typeId;
            return result;
        }

        public override void WriteJson(JsonWriter writer, CustomFieldType value, JsonSerializer serializer)
        {
            long typeId = (long)value;
        }
    }
}
