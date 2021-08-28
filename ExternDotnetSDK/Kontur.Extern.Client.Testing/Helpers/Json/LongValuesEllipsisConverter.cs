using System;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Testing.Helpers.Json
{
    internal class LongValuesEllipsisConverter : JsonConverter
    {
        private const string Ellipsis = "...";
        private const int MinStringSizeLimit = 10;
        private readonly int ellipsisThreshold;
        private readonly int bothSideSize;

        public LongValuesEllipsisConverter(int ellipsisThreshold)
        {
            if (ellipsisThreshold < MinStringSizeLimit)
                throw new ArgumentOutOfRangeException(nameof(ellipsisThreshold), ellipsisThreshold, $"The value should be greater than {MinStringSizeLimit - 1}");
            this.ellipsisThreshold = ellipsisThreshold;
            bothSideSize = (int) Math.Ceiling((ellipsisThreshold - Ellipsis.Length)/2d);
        }

        public override bool CanConvert(Type objectType) => 
            objectType == typeof (byte[]) || objectType == typeof (string);

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value is string stringValue)
            {
                WriteString(writer, stringValue);
            }
            else if (value is byte[] bytesValue)
            {
                WriteBytes(writer, bytesValue);
            }
            else
            {
                serializer.Serialize(writer, value);
            }
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer) => 
            serializer.Deserialize(reader, objectType);

        private void WriteString(JsonWriter writer, string value)
        {
            if (value.Length >= ellipsisThreshold)
            {
                var head = value.Substring(0, bothSideSize);
                var tail = value.Substring(value.Length - bothSideSize, bothSideSize);
                    
                value = $"{head}{Ellipsis}{tail}";
            }
                
            writer.WriteValue(value);
        }
        
        private void WriteBytes(JsonWriter writer, byte[] value)
        {
            if (value.Length >= ellipsisThreshold)
            {
                var span = value.AsSpan();
                var head = span[..bothSideSize];
                var tail = span[..^bothSideSize];
                var stringValue = Convert.ToBase64String(head) + Ellipsis + Convert.ToBase64String(tail);
                writer.WriteValue(stringValue);
            }
            else
            {
                writer.WriteValue(value);
            }
        }
    }
}