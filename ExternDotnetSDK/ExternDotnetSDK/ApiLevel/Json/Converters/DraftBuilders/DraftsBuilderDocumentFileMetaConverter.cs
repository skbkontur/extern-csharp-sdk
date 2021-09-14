#nullable enable
using System;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.DocumentFiles;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.DocumentFiles.Data;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Enums;

namespace Kontur.Extern.Api.Client.ApiLevel.Json.Converters.DraftBuilders
{
    internal class DraftsBuilderDocumentFileMetaConverter : DraftsBuilderPolymorphicConverter<DraftsBuilderDocumentFileMeta>
    {
        protected override Type GetSerializationObjectToDataType(DraftBuilderType? builderType)
        {
            Type? builderDataType = null;
            if (builderType is not null)
            {
                builderDataType = DraftBuilderMetasDataTypes.TryGetBuilderDocumentFileDataType(builderType.Value);
            }
            builderDataType ??= typeof (UnknownDraftsBuilderDocumentFileData);
            
            return typeof (SerializationDraftsBuilderDocumentFileMeta<>).MakeGenericType(builderDataType);
        }

        protected override ISerializationDataType DtoToSerializationObject(DraftsBuilderDocumentFileMeta instance) => 
            new SerializationDraftsBuilderDocumentFileMeta<object>(instance);

        private class SerializationDraftsBuilderDocumentFileMeta<TData> : ISerializationDataType
        {
            public SerializationDraftsBuilderDocumentFileMeta()
            {
            }

            public SerializationDraftsBuilderDocumentFileMeta(DraftsBuilderDocumentFileMeta builderDocumentFileMeta)
            {
                FileName = builderDocumentFileMeta.FileName;
                BuilderType = builderDocumentFileMeta.BuilderType;
                BuilderData = (TData) (object) builderDocumentFileMeta.BuilderData;
            }
            
            public string? FileName { get; set; }

            [UsedImplicitly]
            public Urn? BuilderType { get; set; }
            
            [UsedImplicitly]
            public TData? BuilderData { get; set; }

            public DraftsBuilderDocumentFileMeta ConvertToDto() => new()
            {
                FileName = FileName,
                BuilderType = BuilderType,
                BuilderData = BuilderData as DraftsBuilderDocumentFileData ?? new UnknownDraftsBuilderDocumentFileData()
            };
        }
    }
}