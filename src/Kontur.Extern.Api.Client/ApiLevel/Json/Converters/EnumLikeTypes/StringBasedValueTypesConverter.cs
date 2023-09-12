using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kontur.Extern.Api.Client.Models.Common.Enums;
using Kontur.Extern.Api.Client.Models.Docflows.Documents.Enums;
using Kontur.Extern.Api.Client.Models.Docflows.Enums;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Enums;
using Kontur.Extern.Api.Client.Models.Numbers;
using Kontur.Extern.Api.Client.Models.Numbers.BusinessRegistration;

namespace Kontur.Extern.Api.Client.ApiLevel.Json.Converters.EnumLikeTypes
{
    internal class StringBasedValueTypesConverter : JsonConverterFactory
    {
        private readonly Dictionary<Type, JsonConverter> converters = new();

        public StringBasedValueTypesConverter()
        {
            AddConverter(x => new DocflowType(x), x => x.ToUrn().ToString());
            AddConverter(x => new DocflowStatus(x), x => x.ToUrn().ToString());
            AddConverter(x => new DocflowState(x), x => x.ToUrn().ToString());
            AddConverter(x => new DocumentType(x), x => x.ToUrn().ToString());
            AddConverter(x => new DraftBuilderType(x), x => x.ToUrn().ToString());
            AddConverter(x => new PfrLetterType(x), x => x.ToUrn().ToString());
            AddConverter(x => new DocflowExtendedStatus(x), x => x.ToUrn().ToString());
            AddConverter(x => new SfrReportCorrectionType(x), x => x.ToUrn().ToString());
            AddConverter(x => new SvdregCode(x), x => x.Code?.ToString());

            AddConverterForClass(s => new Knd(s), x => x.Value.ToString());
            AddConverterForClass(s => new Kpp(s), x => x.Value.ToString());
            AddConverterForClass(s => new InnKpp(s), x => x.Value.ToString());
            AddConverterForClass(s => new Okud(s), x => x.Value.ToString());
            AddConverterForClass(s => new Okpo(s), x => x.Value.ToString());
            AddConverterForClass(s => new Ogrn(s), x => x.Value.ToString());
            AddConverterForClass(s => new Snils(s), x => x.Value.ToString());
            AddConverterForClass(s => new Inn(s), x => x.Value.ToString());
            AddConverterForClass(s => new LegalEntityInn(s), x => x.Value.ToString());

            AddConverterForClass(s => new FssCode(s), x => x.Value.ToString());
            AddConverterForClass(s => new IfnsCode(s), x => x.Value.ToString());
            AddConverterForClass(s => new MriCode(s), x => x.Value.ToString());
            AddConverterForClass(s => new TogsCode(s), x => x.Value.ToString());
            AddConverterForClass(s => new UpfrCode(s), x => x.Value.ToString());

            AddConverterForClass(s => new PfrRegNumber(s), x => x.Value.ToString());
            AddConverterForClass(s => new SfrRegNumber(s), x => x.Value.ToString());
            AddConverterForClass(s => new FssRegNumber(s), x => x.Value.ToString());
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

        private void AddConverterForClass<T>(Func<string, T> fromString, Func<T, string?> toString)
            where T : class
        {
            converters.Add(typeof (T), new WrapperConverterForClass<T>(fromString, toString));
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

        private class WrapperConverterForClass<T> : JsonConverter<T>
            where T : class
        {
            private readonly Func<string, T> fromString;
            private readonly Func<T, string?> toString;

            public WrapperConverterForClass(Func<string, T> fromString, Func<T, string?> toString)
            {
                this.fromString = fromString;
                this.toString = toString;
            }

            public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var value = reader.GetString();
                return string.IsNullOrEmpty(value) ? default : fromString(value!);
            }

            public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options)
            {
                var stringValue = value is null ? null : toString(value);
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