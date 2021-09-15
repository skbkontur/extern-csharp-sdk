using System;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Models.Drafts.Meta;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Enums;

namespace Kontur.Extern.Api.Client.ApiLevel.Json.Converters.DraftBuilders
{
    internal class DraftsBuilderMetaConverter : DraftsBuilderPolymorphicConverter<DraftsBuilderMeta>
    {
        protected override Type GetSerializationObjectToDataType(DraftBuilderType? builderType)
        {
            Type? builderDataType = null;
            if (builderType is not null)
            {
                builderDataType = DraftBuilderMetasDataTypes.TryGetBuildersDataType(builderType.Value);
            }
            builderDataType ??= typeof (UnknownDraftsBuilderData);
            
            return typeof (SerializationDraftsBuilderMeta<>).MakeGenericType(builderDataType);
        }

        protected override ISerializationDataType DtoToSerializationObject(DraftsBuilderMeta instance) => 
            new SerializationDraftsBuilderMeta<object>(instance);

        private class SerializationDraftsBuilderMeta<TData> : ISerializationDataType
        {
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
                Sender = Sender!,
                Payer = Payer!,
                Recipient = Recipient!,
                BuilderType = BuilderType!,
                BuilderData = BuilderData as DraftsBuilderData ?? new UnknownDraftsBuilderData()
            };
        }
    }
}