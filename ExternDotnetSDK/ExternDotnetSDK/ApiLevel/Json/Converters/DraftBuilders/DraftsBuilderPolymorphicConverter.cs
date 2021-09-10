#nullable enable
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kontur.Extern.Client.Exceptions;
using Kontur.Extern.Client.Http.Serialization.SysTextJson.Extensions;
using Kontur.Extern.Client.Http.Serialization.SysTextJson.NamingPolicies;
using Kontur.Extern.Client.Models.DraftsBuilders.Builders;
using Kontur.Extern.Client.Models.DraftsBuilders.Enums;

namespace Kontur.Extern.Client.ApiLevel.Json.Converters.DraftBuilders
{
    internal abstract class DraftsBuilderPolymorphicConverter<T> : JsonConverter<T>
        where T : class
    {
        private readonly Utf8String builderTypePropName = nameof(DraftsBuilderMeta.BuilderType).ToKebabCase();

        protected abstract Type GetSerializationObjectToDataType(DraftBuilderType? builderType);

        protected abstract ISerializationDataType DtoToSerializationObject(T instance);

        public sealed override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var builderType = TryGetBuilderType(reader, builderTypePropName);
            // todo: optimize it by caching
            var serializedType = GetSerializationObjectToDataType(builderType);
            
            var deserialized = (ISerializationDataType?) JsonSerializer.Deserialize(ref reader, serializedType, options);
            return deserialized?.ConvertToDto();

            static DraftBuilderType? TryGetBuilderType(Utf8JsonReader readerClone, in Utf8String typePropName)
            {
                if (!readerClone.ScanToPropertyValue(typePropName.AsUtf8()))
                    throw Errors.JsonDoesNotContainProperty(typePropName.ToString());
                
                var docflowTypeValue = readerClone.GetString();
                return docflowTypeValue == null ? (DraftBuilderType?) null : new DraftBuilderType(docflowTypeValue);
            }
        }
    
        public sealed override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options) => 
            JsonSerializer.Serialize<object>(writer, DtoToSerializationObject(value), options);

        protected interface ISerializationDataType
        {
            T ConvertToDto();
        }
    }
}