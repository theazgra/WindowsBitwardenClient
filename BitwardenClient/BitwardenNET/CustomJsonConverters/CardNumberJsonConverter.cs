using Newtonsoft.Json;
using BitwardenNET.VaultTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitwardenNET.CustomJsonConverters
{
    internal class CardNumberJsonConverter : JsonConverter<CardNumber>
    {
        public override CardNumber ReadJson(JsonReader reader, Type objectType, CardNumber existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string cardNumber = (string)reader.Value;
            CardNumber parsed = new CardNumber(cardNumber);
            return parsed;
        }

        public override void WriteJson(JsonWriter writer, CardNumber value, JsonSerializer serializer)
        {
            writer.WriteValue(value.GetJsonString());
        }
    }
}
