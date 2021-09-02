using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kontur.Extern.Client.Http.Serialization.SysTextJson.Converters.EnumConverters
{
    internal class NullableJsonEnumConverter<T> : JsonConverter<T?>
        where T : struct, Enum
    {
        private readonly JsonConverter<T> enumConverter;

        public NullableJsonEnumConverter(JsonConverter<T> enumConverter) => 
            this.enumConverter = enumConverter;

        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;

            return enumConverter.Read(ref reader, typeToConvert, options);
        }

        public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
            }
            else
            {
                enumConverter.Write(writer, value.Value, options);
            }
        }
    }
}