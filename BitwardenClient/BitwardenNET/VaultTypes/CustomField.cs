using BitwardenNET.CustomJsonConverters;
using Newtonsoft.Json;

namespace BitwardenNET.VaultTypes.ItemTypes
{
    public class CustomField
    {
        [JsonProperty("type")]
        [JsonConverter(typeof(FieldTypeJsonConverter))]
        public CustomFieldType FieldType { get; }

        [JsonProperty("name")]
        public string FieldName { get; set; }

        [JsonProperty("value")]
        public object Value { get; set; }
    }
}
