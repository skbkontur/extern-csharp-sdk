using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kontur.Extern.Client.Http.Serialization.SysTextJson.Converters.EnumConverters
{
    public class NamingPolicyRespectJsonStringEnumConverter : JsonConverterFactory
    {
        private readonly Dictionary<Type, JsonNamingPolicy?> enumTypeNamingPoliciesMap;
        private readonly bool serializeAsStrings;
        private readonly bool serializeHttpCodeAsStrings;
        private readonly JsonStringEnumConverter defaultJsonStringEnumConverter = new();

        public NamingPolicyRespectJsonStringEnumConverter(bool serializeAsStrings = true, bool serializeHttpCodeAsStrings = false)
            : this(new Dictionary<Type, JsonNamingPolicy?>(), serializeHttpCodeAsStrings)
        {
        }

        public NamingPolicyRespectJsonStringEnumConverter(Dictionary<Type, JsonNamingPolicy?> enumTypeNamingPoliciesMap, bool serializeAsStrings = true, bool serializeHttpCodeAsStrings = false)
        {
            this.enumTypeNamingPoliciesMap = enumTypeNamingPoliciesMap;
            this.serializeAsStrings = serializeAsStrings;
            this.serializeHttpCodeAsStrings = serializeHttpCodeAsStrings;
        }
        
        public override bool CanConvert(Type typeToConvert)
        {
            if (!typeToConvert.IsValueType)
                return false;
            if (typeToConvert.IsEnum)
                return true;

            var nullableUnderlyingType = Nullable.GetUnderlyingType(typeToConvert);
            return nullableUnderlyingType is not null && nullableUnderlyingType.IsEnum;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            if (typeToConvert.IsEnum)
                return CreateJsonStringEnumConverter(typeToConvert, options);

            var nullableUnderlyingType = Nullable.GetUnderlyingType(typeToConvert);
            if (nullableUnderlyingType == null)
                throw new InvalidOperationException();
            
            var jsonStringEnumConverter = CreateJsonStringEnumConverter(nullableUnderlyingType, options);
            var converterType = typeof (NullableJsonEnumConverter<>).MakeGenericType(nullableUnderlyingType);
            return (JsonConverter) Activator.CreateInstance(converterType, jsonStringEnumConverter);
        }

        private JsonConverter CreateJsonStringEnumConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var namingPolicy = enumTypeNamingPoliciesMap.TryGetValue(typeToConvert, out var overridenNamingPolicy) 
                ? overridenNamingPolicy 
                : options.PropertyNamingPolicy;

            if (typeToConvert == typeof (HttpStatusCode))
            {
                return new HttpStatusCodeConverter(namingPolicy, serializeHttpCodeAsStrings);
            }
            
            if ((namingPolicy is not null || !serializeAsStrings)  && IsNamingPolicyConverterSupported(typeToConvert))
            {
                var converterType = typeof (NamingPolicyRespectJsonStringEnumConverter<>).MakeGenericType(typeToConvert);
                return (JsonConverter) Activator.CreateInstance(converterType, namingPolicy, options.Encoder, serializeAsStrings);
            }

            return defaultJsonStringEnumConverter.CreateConverter(typeToConvert, options);
        }

        private static bool IsNamingPolicyConverterSupported(Type enumType)
        {
            var backingTypeCode = Type.GetTypeCode(enumType);
            return enumType.GetCustomAttribute(typeof (FlagsAttribute)) is null &&
                   (backingTypeCode == TypeCode.Int32 || backingTypeCode == TypeCode.Int64 ||
                    backingTypeCode == TypeCode.UInt32 || backingTypeCode == TypeCode.UInt64);
        }
    }
}