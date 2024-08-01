using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kontur.Extern.Api.Client.Http.Exceptions;
using Kontur.Extern.Api.Client.Http.Serialization.SysTextJson.NamingPolicies;

namespace Kontur.Extern.Api.Client.Http.Serialization.SysTextJson.Converters.EnumConverters
{
    internal class NamingPolicyRespectJsonStringEnumConverter<T> : JsonConverter<T>
        where T : struct, Enum
    {
        private static readonly Type TypeToConvert = typeof(T);
        private static readonly TypeCode BackingTypeCode = Type.GetTypeCode(typeof(T));
        private readonly bool serializeAsStrings;
        private readonly Dictionary<T, JsonEncodedText> valuesToNames;
        private readonly Dictionary<string, T> stringsToValues;

        public NamingPolicyRespectJsonStringEnumConverter(JsonNamingPolicy? namingPolicy, JavaScriptEncoder? encoder, bool serializeAsStrings)
        {
            this.serializeAsStrings = serializeAsStrings;
            namingPolicy ??= new AsIsNamingPolicy();

            var names = Enum.GetNames(TypeToConvert);
            var values = Enum.GetValues(TypeToConvert);
            Debug.Assert(names.Length == values.Length);

            valuesToNames = new Dictionary<T, JsonEncodedText>(names.Length);
            stringsToValues = new Dictionary<string, T>(names.Length);
            for (var i = 0; i < names.Length; i++)
            {
                var value = (T) values.GetValue(i)!;
                var enumMemberAttribute = ((EnumMemberAttribute[])TypeToConvert.GetField(names[i])
                    .GetCustomAttributes(typeof(EnumMemberAttribute), true)).FirstOrDefault();
                var name = enumMemberAttribute?.Value ?? namingPolicy.ConvertName(names[i]);
                valuesToNames.Add(value, JsonEncodedText.Encode(name, encoder));
                stringsToValues.Add(name, value);
            }
        }

        public override bool CanConvert(Type typeToConvert) =>
            base.CanConvert(typeToConvert) &&
            typeToConvert.GetCustomAttribute(typeof (FlagsAttribute)) is null &&
            (BackingTypeCode == TypeCode.Int32 || BackingTypeCode == TypeCode.Int64 ||
             BackingTypeCode == TypeCode.UInt32 || BackingTypeCode == TypeCode.UInt64);

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var token = reader.TokenType;
            return token switch
            {
                JsonTokenType.String => ReadAsString(ref reader, stringsToValues),
                JsonTokenType.Number => ReadAsNumber(ref reader),
                _ => throw Errors.JsonTokenIsUnexpected(token, JsonTokenType.String, JsonTokenType.Number)
            };

            static T ReadAsString(ref Utf8JsonReader reader, Dictionary<string, T> stringsToValues)
            {
                var enumString = reader.GetString();
                if (enumString is not null && stringsToValues.TryGetValue(enumString, out var value))
                    return value;
                
                if (!Enum.TryParse(enumString, out value) && !Enum.TryParse(enumString, true, out value))
                {
                    throw Errors.CannotParseJsonStringValueToEnumOfType(enumString, TypeToConvert);
                }

                return value;
            }

            static T ReadAsNumber(ref Utf8JsonReader reader)
            {
                switch (BackingTypeCode)
                {
                    case TypeCode.Int32:
                        if (reader.TryGetInt32(out var int32))
                        {
                            return Unsafe.As<int, T>(ref int32);
                        }

                        break;
                    case TypeCode.UInt32:
                        if (reader.TryGetUInt32(out var uint32))
                        {
                            return Unsafe.As<uint, T>(ref uint32);
                        }

                        break;
                    case TypeCode.UInt64:
                        if (reader.TryGetUInt64(out var uint64))
                        {
                            return Unsafe.As<ulong, T>(ref uint64);
                        }

                        break;
                    case TypeCode.Int64:
                        if (reader.TryGetInt64(out var int64))
                        {
                            return Unsafe.As<long, T>(ref int64);
                        }

                        break;
                    default:
                        throw Errors.EnumValueOfBackingTypeIsNotSupported(BackingTypeCode);
                }

                throw Errors.CannotReadNumberFromJsonValue();
            }
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            if (serializeAsStrings)
            {
                var formatted = valuesToNames[value];
                writer.WriteStringValue(formatted);
                return;
            }

            switch (BackingTypeCode)
            {
                case TypeCode.Int32:
                    writer.WriteNumberValue(Unsafe.As<T, int>(ref value));
                    break;
                case TypeCode.UInt32:
                    writer.WriteNumberValue(Unsafe.As<T, uint>(ref value));
                    break;
                case TypeCode.UInt64:
                    writer.WriteNumberValue(Unsafe.As<T, ulong>(ref value));
                    break;
                case TypeCode.Int64:
                    writer.WriteNumberValue(Unsafe.As<T, long>(ref value));
                    break;
                default:
                    throw Errors.EnumValueOfBackingTypeIsNotSupported(BackingTypeCode);
            }
        }
    }
}