using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kontur.Extern.Api.Client.Http.Serialization.SysTextJson.Converters
{
    internal class IPAddressConverter : JsonConverter<IPAddress>
    {
        public override IPAddress? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return value is null ? null : IPAddress.Parse(value);
        }

        public override void Write(Utf8JsonWriter writer, IPAddress? value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
            }
            else
            {
                writer.WriteStringValue(value.ToString());
            }
        }
    }
}