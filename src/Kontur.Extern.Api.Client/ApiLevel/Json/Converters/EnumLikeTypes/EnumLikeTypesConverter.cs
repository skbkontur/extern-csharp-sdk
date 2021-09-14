using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kontur.Extern.Api.Client.Models.Docflows.Documents.Enums;
using Kontur.Extern.Api.Client.Models.Docflows.Enums;
using Kontur.Extern.Api.Client.Models.Drafts.Enums;
using Kontur.Extern.Api.Client.Models.Numbers.BusinessRegistration;

namespace Kontur.Extern.Api.Client.ApiLevel.Json.Converters.EnumLikeTypes
{
    internal class EnumLikeTypesConverter : JsonConverterFactory
    {
        private readonly Dictionary<Type, JsonConverter> converters = new();

        public EnumLikeTypesConverter()
        {
            AddConverter(x => new DocflowType(x), x => x.ToUrn()?.ToString());
            AddConverter(x => new DocflowStatus(x), x => x.ToUrn()?.ToString());
            AddConverter(x => new DocflowState(x), x => x.ToUrn()?.ToString());
            AddConverter(x => new DocumentType(x), x => x.ToUrn()?.ToString());
            AddConverter(x => new PfrLetterType(x), x => x.ToUrn()?.ToString());
            AddConverter(SvdregCode.Parse, x => x.Code?.ToString());
        }

        public override bool CanConvert(Type typeToConvert) => 
            converters.ContainsKey(typeToConvert);

        public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options) => 
            converters[typeToConvert];

        private void AddConverter<T>(Func<string, T> fromString, Func<T, string?> toString)
            where T : struct
        {
            converters.Add(typeof (T), new WrapperConverter<T>(fromString, toString));
        }

        private class WrapperConverter<T> : JsonConverter<T>
            where T : struct
        {
            private readonly Func<string, T> fromString;
            private readonly Func<T, string?> toString;

            public WrapperConverter(Func<string, T> fromString, Func<T, string?> toString)
            {
                this.fromString = fromString;
                this.toString = toString;
            }
        
            public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var value = reader.GetString();
                return value is null ? default : fromString(value);
            }

            public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
            {
                var stringValue = toString(value);
                if (stringValue is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteStringValue(stringValue);
                }
            }
        }
    }
}