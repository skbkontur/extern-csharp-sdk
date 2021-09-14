using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kontur.Extern.Api.Client.Http.Exceptions;

namespace Kontur.Extern.Api.Client.Http.Serialization.SysTextJson.Converters.EnumConverters
{
    internal class HttpStatusCodeConverter : JsonConverter<HttpStatusCode>
    {
        private readonly Dictionary<string, HttpStatusCode> stringToCodeMap;
        private readonly Dictionary<HttpStatusCode, string>? codeToStringMap;

        public HttpStatusCodeConverter(JsonNamingPolicy? namingPolicy, bool serializeAsString = false)
        {
            stringToCodeMap = BuildStringToCodeMap(namingPolicy);
            if (serializeAsString)
            {
                codeToStringMap = BuildCodeToStringMap(namingPolicy);
            }
        }

        public override HttpStatusCode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                {
                    var value = reader.GetString();
                    if (value != null)
                    {
                        if (stringToCodeMap.TryGetValue(value, out var code))
                        {
                            return code;
                        }

                        if (int.TryParse(value, out var intCode))
                        {
                            return (HttpStatusCode) intCode;
                        }
                    }

                    throw Errors.JsonInvalidEnumValue(typeToConvert, value);
                }
                case JsonTokenType.Number:
                    return (HttpStatusCode) reader.GetInt32();
                
                default:
                    throw Errors.JsonTokenIsUnexpected(typeToConvert, reader.TokenType, JsonTokenType.String, JsonTokenType.Number);
            }
        }

        public override void Write(Utf8JsonWriter writer, HttpStatusCode value, JsonSerializerOptions options)
        {
            if (codeToStringMap == null)
            {
                writer.WriteNumberValue((int) value);
            }
            else
            {
                writer.WriteStringValue(codeToStringMap[value]);
            }
        }

        private static Dictionary<HttpStatusCode, string> BuildCodeToStringMap(JsonNamingPolicy? namingPolicy)
        {
            var allCodeValues = Enum.GetValues(typeof (HttpStatusCode));
            var map = new Dictionary<HttpStatusCode, string>(allCodeValues.Length);

            if (namingPolicy == null)
            {
                foreach (var statusCode in allCodeValues.OfType<HttpStatusCode>().Distinct())
                {
                    map.Add(statusCode, statusCode.ToString());
                }
            }
            else
            {
                foreach (var statusCode in allCodeValues.OfType<HttpStatusCode>().Distinct())
                {
                    map.Add(statusCode, namingPolicy.ConvertName(statusCode.ToString()));
                }
            }

            return map;
        }

        private static Dictionary<string, HttpStatusCode> BuildStringToCodeMap(JsonNamingPolicy? namingPolicy)
        {
            var allCodeValues = Enum.GetValues(typeof (HttpStatusCode));
            
            var stringVariantsPerCode = 3;
            if (namingPolicy != null)
                stringVariantsPerCode++;

            var capacity = allCodeValues.Length*stringVariantsPerCode;
            var map = new Dictionary<string, HttpStatusCode>(capacity);
            
            foreach (var httpStatusCode in allCodeValues.OfType<HttpStatusCode>())
            {
                var stringRepresentation = httpStatusCode.ToString();
                TryAddStringRepresentation(map, stringRepresentation, httpStatusCode, namingPolicy);
            }
            
            TryAddStringRepresentation(map, nameof(HttpStatusCode.Ambiguous), HttpStatusCode.Ambiguous, namingPolicy);
            TryAddStringRepresentation(map, nameof(HttpStatusCode.MultipleChoices), HttpStatusCode.MultipleChoices, namingPolicy);
            
            return map;

            static void TryAddStringRepresentation(Dictionary<string, HttpStatusCode> map, string stringRepresentation, HttpStatusCode httpStatusCode, JsonNamingPolicy? jsonNamingPolicy)
            {
                if (map.ContainsKey(stringRepresentation))
                    return;

                map.Add(stringRepresentation, httpStatusCode);

                var lowerCaseValue = stringRepresentation.ToLowerInvariant();
                if (!map.ContainsKey(lowerCaseValue))
                {
                    map.Add(lowerCaseValue, httpStatusCode);
                }

                var upperCaseValue = stringRepresentation.ToUpperInvariant();
                if (!map.ContainsKey(upperCaseValue))
                {
                    map.Add(upperCaseValue, httpStatusCode);
                }

                if (jsonNamingPolicy != null)
                {
                    var namingPolicyValue = jsonNamingPolicy.ConvertName(stringRepresentation);
                    if (!map.ContainsKey(namingPolicyValue))
                    {
                        map.Add(namingPolicyValue, httpStatusCode);
                    }
                }
            }
        }
    }
}