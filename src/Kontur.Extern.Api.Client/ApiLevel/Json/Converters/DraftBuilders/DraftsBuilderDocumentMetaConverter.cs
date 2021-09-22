using System;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents.Data;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Enums;

namespace Kontur.Extern.Api.Client.ApiLevel.Json.Converters.DraftBuilders
{
    internal class DraftsBuilderDocumentMetaConverter : DraftsBuilderPolymorphicConverter<DraftsBuilderDocumentMeta>
    {
        protected override Type GetSerializationObjectToDataType(DraftBuilderType? builderType)
        {
            Type? builderDataType = null;
            if (builderType is not null)
            {
                builderDataType = DraftBuilderMetasDataTypes.TryGetBuilderDocumentDataType(builderType.Value);
            }
            builderDataType ??= typeof (UnknownDraftsBuilderData);
            
            return typeof (SerializationDraftsBuilderDocumentMeta<>).MakeGenericType(builderDataType);
        }

        protected override ISerializationDataType DtoToSerializationObject(DraftsBuilderDocumentMeta instance) => 
            new SerializationDraftsBuilderDocumentMeta<object>(instance);

        private class SerializationDraftsBuilderDocumentMeta<TData> : ISerializationDataType
        {
            [JsonConstructor]
            public SerializationDraftsBuilderDocumentMeta()
            {
            }

            public SerializationDraftsBuilderDocumentMeta(DraftsBuilderDocumentMeta dto)
            {
                BuilderType = dto.BuilderType;
                BuilderData = (TData?) (object?) dto.BuilderData;
            }

            [UsedImplicitly]
            public DraftBuilderType BuilderType { get; set; }
            
            [UsedImplicitly]
            public TData? BuilderData { get; set; }

            public DraftsBuilderDocumentMeta ConvertToDto() => new(
                BuilderType,
                BuilderData as DraftsBuilderDocumentData ?? new UnknownBuilderDocumentData()
            );
        }
    }
}