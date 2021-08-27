using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kontur.Extern.Client.Common.Time;

namespace Kontur.Extern.Client.Http.Serialization.SysTextJson.Converters
{
    internal class DateOnlyConverter : JsonConverter<DateOnly>
    {
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => 
            reader.TryGetDateTime(out var dateTime) ? dateTime : DateOnly.Parse(reader.GetString()!);

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options) => 
            writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
    }
}