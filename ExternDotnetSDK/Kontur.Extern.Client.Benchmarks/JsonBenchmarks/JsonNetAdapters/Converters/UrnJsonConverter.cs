using System;
using Kontur.Extern.Client.Models.Common;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Benchmarks.JsonBenchmarks.JsonNetAdapters.Converters
{
    internal class UrnJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) =>
            writer.WriteValue(((Urn?) value)?.ToString());

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            if (reader.TokenType != JsonToken.String)
                throw new JsonSerializationException("Unexpected token parsing URN. Expected String");

            if (reader.Value == null)
                return null;
            
            var value = reader.Value.ToString();
            return Urn.Parse(value!);
        }

        public override bool CanConvert(Type objectType) => typeof (Urn).IsAssignableFrom(objectType);
    }
}