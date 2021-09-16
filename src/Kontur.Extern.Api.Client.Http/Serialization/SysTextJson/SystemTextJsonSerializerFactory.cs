using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Http.Exceptions;
using Kontur.Extern.Api.Client.Http.Serialization.SysTextJson.Converters;
using Kontur.Extern.Api.Client.Http.Serialization.SysTextJson.Converters.EnumConverters;

namespace Kontur.Extern.Api.Client.Http.Serialization.SysTextJson
{
    [PublicAPI]
    public class SystemTextJsonSerializerFactory
    {
        private Dictionary<Type, JsonNamingPolicy?> enumTypeNamingPoliciesMap = new();
        private bool ignoreIndentation;
        private readonly JsonSerializerOptions options;

        public SystemTextJsonSerializerFactory()
        {
            var encoderSettings = new TextEncoderSettings();
            encoderSettings.AllowRange(UnicodeRanges.BasicLatin);
            encoderSettings.AllowRange(UnicodeRanges.Cyrillic);
            encoderSettings.AllowRange(UnicodeRanges.CyrillicSupplement);
            encoderSettings.AllowRange(UnicodeRanges.CyrillicExtendedA);
            encoderSettings.AllowRange(UnicodeRanges.CyrillicExtendedB);
            encoderSettings.AllowRange(UnicodeRanges.CyrillicExtendedC);

            options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IncludeFields = true,
                Encoder = JavaScriptEncoder.Create(encoderSettings),
            };
        }

        public SystemTextJsonSerializerFactory AddConverter(JsonConverter jsonConverter)
        {
            var converter = jsonConverter ?? throw new ArgumentNullException(nameof(jsonConverter));
            if (converter is JsonStringEnumConverter)
                throw Errors.OverridingJsonStringEnumConverterIsUnsupported(nameof(jsonConverter));
            
            options.Converters.Add(converter);
            return this;
        }

        public SystemTextJsonSerializerFactory WithNamingPolicy(JsonNamingPolicy? policy)
        {
            options.PropertyNamingPolicy = policy;
            return this;
        }

        public SystemTextJsonSerializerFactory IgnoreIndentation(bool enabled = true)
        {
            ignoreIndentation = enabled;
            return this;
        }

        public SystemTextJsonSerializerFactory SetCustomNamingPolicyForSerializationEnumOf<T>(JsonNamingPolicy? namingPolicy)
            where T : struct, Enum
        {
            enumTypeNamingPoliciesMap[typeof (T)] = namingPolicy;
            return this;
        }

        public SystemTextJsonSerializerFactory IgnoreNullValues(bool enabled = true)
        {
            options.IgnoreNullValues = enabled;
            return this;
        }

        public IJsonSerializer CreateSerializer()
        {
            var serializerOptions = AddDefaultConverters(options, enumTypeNamingPoliciesMap);
            return new SystemTextJsonSerializer(serializerOptions, ignoreIndentation);
        }

        private static JsonSerializerOptions AddDefaultConverters(JsonSerializerOptions options, Dictionary<Type, JsonNamingPolicy?> enumTypeNamingPoliciesMap)
        {
            var serializerOptions = new JsonSerializerOptions(options);
            
            var jsonStringEnumConverterAdded = false;
            foreach (var converter in serializerOptions.Converters)
            {
                if (converter is NamingPolicyRespectJsonStringEnumConverter)
                {
                    jsonStringEnumConverterAdded = true;
                }
            }

            if (!jsonStringEnumConverterAdded)
            {
                serializerOptions.Converters.Add(new NamingPolicyRespectJsonStringEnumConverter(enumTypeNamingPoliciesMap));
            }

            serializerOptions.Converters.Add(new DateOnlyConverter());
            serializerOptions.Converters.Add(new IPAddressConverter());
            return serializerOptions;
        }
    }
}