using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kontur.Extern.Client.Http.Exceptions;

namespace Kontur.Extern.Client.Http.Serialization.SysTextJson.Converters
{
    public class YesNoUnknownBooleanConverter : JsonConverter<bool?>
    {
        private const string YesValue = "yes";
        private const string NoValue = "no";
        private const string UnknownValue = "unknown";
        private static readonly string[] AlternativeTrues = {bool.TrueString, YesValue};
        private static readonly string[] AlternativeFalsies = {bool.FalseString, NoValue};
        private static readonly string[] AlternativeNulls = {UnknownValue};
        private static readonly string[] PossibleValues = AlternativeTrues
            .Concat(AlternativeFalsies)
            .Concat(AlternativeNulls)
            .Select(x => $"\"{x}\"")
            .Prepend(bool.FalseString)
            .Prepend(bool.TrueString)
            .ToArray();

        public override bool HandleNull => true;

        public override bool? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.TokenType switch
            {
                JsonTokenType.Null => null,
                JsonTokenType.False => false,
                JsonTokenType.True => true,
                JsonTokenType.String => ConvertStringToBoolean(ref reader),
                _ => throw Errors.JsonTokenIsUnexpected(reader.TokenType, JsonTokenType.String, JsonTokenType.False, JsonTokenType.True, JsonTokenType.Null)
            };

            static bool? ConvertStringToBoolean(ref Utf8JsonReader reader)
            {
                var value = reader.GetString();
                if (value == null)
                    return null;

                if (AlternativeTrues.Contains(value, StringComparer.OrdinalIgnoreCase))
                    return true;

                if (AlternativeFalsies.Contains(value, StringComparer.OrdinalIgnoreCase))
                    return false;

                if (AlternativeNulls.Contains(value, StringComparer.OrdinalIgnoreCase))
                    return null;

                throw Errors.UnknownJsonBooleanValue(value, PossibleValues);
            }
        }

        public override void Write(Utf8JsonWriter writer, bool? value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case null:
                    writer.WriteStringValue(UnknownValue);
                    break;
                case true:
                    writer.WriteStringValue(YesValue);
                    break;
                case false:
                    writer.WriteStringValue(NoValue);
                    break;
            }
        }
    }
}