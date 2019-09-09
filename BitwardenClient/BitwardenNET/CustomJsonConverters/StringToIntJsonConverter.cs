﻿using Newtonsoft.Json;
using System;

namespace BitwardenNET.CustomJsonConverters
{
    internal class StringToIntJsonConverter : JsonConverter<int>
    {
        public override int ReadJson(JsonReader reader, Type objectType, int existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return int.Parse((string)reader.Value);
        }

        public override void WriteJson(JsonWriter writer, int value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}
