using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kontur.Extern.Api.Client.ApiLevel.Json.Converters.DraftBuilders
{
    internal class WriteAbstractTypeValueAsConcreteTypeValueConverter : JsonConverter<object>
    {
        public override bool CanConvert(Type typeToConvert) => 
            typeToConvert.IsAbstract;

        public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => 
            throw new NotSupportedException();

        public override void Write(Utf8JsonWriter writer, object? value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
            }
            else
            {
                JsonSerializer.Serialize(writer, value, value.GetType(), options);
            }
        }
    }
}