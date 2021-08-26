using System;
using System.Text.Json;
using Kontur.Extern.Client.Exceptions;

namespace Kontur.Extern.Client.ApiLevel.Json.SysTextJsonExtensions
{
    internal static class Utf8JsonReaderExtension
    {
        public static string Dump(this Utf8JsonReader readerClone)
        {
            using var document = JsonDocument.ParseValue(ref readerClone);
            return document.RootElement.GetRawText();
        }
        
        public static bool ScanToPropertyValue(this ref Utf8JsonReader reader, in ReadOnlySpan<byte> utf8PropertyName)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
                throw Errors.JsonIsNotAnObject();

            var openedScopes = 0;
            while (reader.Read())
            {
                if (openedScopes > 0)
                {
                    if (reader.TokenType == JsonTokenType.EndArray || reader.TokenType == JsonTokenType.EndObject)
                        openedScopes--;

                    continue;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    if (reader.ValueTextEquals(utf8PropertyName))
                    {
                        reader.Read();
                        return true;
                    }
                }
                else if (reader.TokenType == JsonTokenType.StartArray || reader.TokenType == JsonTokenType.StartObject)
                {
                    openedScopes++;
                }
            }

            return false;
        }
    }
}