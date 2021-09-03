#nullable enable
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Meta;
using Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Builders;
using Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Builders.Data;
using Kontur.Extern.Client.Exceptions;
using Kontur.Extern.Client.Http.Serialization.SysTextJson.Extensions;
using Kontur.Extern.Client.Http.Serialization.SysTextJson.NamingPolicies;
using Kontur.Extern.Client.Model.DraftBuilders;

namespace Kontur.Extern.Client.ApiLevel.Json.Converters.DraftBuilders
{
    internal class DraftsBuilderMetaConverter : JsonConverter<DraftsBuilderMeta>
    {
        private readonly Utf8String builderTypePropName = nameof(DraftsBuilderMeta.BuilderType).ToKebabCase();

        public override DraftsBuilderMeta? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var requisitesType = TryGetDataType(reader, builderTypePropName);
            // todo: optimize it by caching
            var builderMetaType = typeof (SerializationDraftsBuilderMeta<>).MakeGenericType(requisitesType);
            var deserialized = (ISerializationDraftsBuilderMeta?) JsonSerializer.Deserialize(ref reader, builderMetaType, options);
            return deserialized?.ConvertToDto();

            static Type TryGetDataType(Utf8JsonReader readerClone, in Utf8String typePropName)
            {
                if (!readerClone.ScanToPropertyValue(typePropName.AsUtf8()))
                    throw Errors.JsonDoesNotContainProperty(typePropName.ToString());
                
                var docflowTypeValue = readerClone.GetString();
                if (docflowTypeValue == null)
                    return typeof (UnknownDraftsBuilderData);
    
                var builderType = new DraftBuilderType(docflowTypeValue);
                return DraftBuilderDataTypes.TryGetBuilderDataType(builderType) ??
                       typeof (UnknownDraftsBuilderData);
            }
        }
    
        public override void Write(Utf8JsonWriter writer, DraftsBuilderMeta value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, new SerializationDraftsBuilderMeta<object>(value), options);
        }
        
        private interface ISerializationDraftsBuilderMeta
        {
            DraftsBuilderMeta ConvertToDto();
        }
        
        private class SerializationDraftsBuilderMeta<TData> : ISerializationDraftsBuilderMeta
        {
            [JsonConstructor]
            public SerializationDraftsBuilderMeta()
            {
            }

            public SerializationDraftsBuilderMeta(DraftsBuilderMeta draftsBuilderMeta)
            {
                Sender = draftsBuilderMeta.Sender;
                Payer = draftsBuilderMeta.Payer;
                Recipient = draftsBuilderMeta.Recipient;
                BuilderType = draftsBuilderMeta.BuilderType;
                BuilderData = (TData) (object) draftsBuilderMeta.BuilderData;
            }

            [UsedImplicitly]
            public Sender? Sender { get; set; }
            
            [UsedImplicitly]
            public AccountInfo? Payer { get; set; }
            
            [UsedImplicitly]
            public RecipientInfo? Recipient { get; set; }
            
            [UsedImplicitly]
            public Urn? BuilderType { get; set; }
            
            [UsedImplicitly]
            public TData? BuilderData { get; set; }

            public DraftsBuilderMeta ConvertToDto() => new()
            {
                Sender = Sender,
                Payer = Payer,
                Recipient = Recipient,
                BuilderType = BuilderType,
                BuilderData = BuilderData as DraftsBuilderData ?? new UnknownDraftsBuilderData()
            };
        }
    }
}