#nullable enable
using System;
using AutoBogus;
using Kontur.Extern.Client.ApiLevel.Json.Converters.DraftBuilders;
using Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Builders;
using Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Builders.Data;
using Kontur.Extern.Client.Model.DraftBuilders;
using Kontur.Extern.Client.Tests.TestHelpers.BogusExtensions;

namespace Kontur.Extern.Client.Tests.ApiLevel.Clients.Models.TestDtoGenerators
{
    internal class DraftsBuilderMetaGenerator
    {
        private readonly IAutoFaker faker;

        public DraftsBuilderMetaGenerator() => faker = AutoFakerFactory.Create();

        public DraftsBuilderMeta GenerateWithData(DraftBuilderType draftBuilderType)
        {
            var dataType = DraftBuilderDataTypes.TryGetBuilderDataType(draftBuilderType);
            return GenerateWithData(dataType, draftBuilderType);
        }
        
        public DraftsBuilderMeta GenerateWithData(Type? dataType, DraftBuilderType draftBuilderType)
        {
            var builderMeta = GenerateWithoutData(draftBuilderType);
            if (dataType != null)
            {
                builderMeta.BuilderData = (DraftsBuilderData?) faker.Generate(dataType) ?? new UnknownDraftsBuilderData();
            }
            else
            {
                builderMeta.BuilderData = new UnknownDraftsBuilderData();
            }

            return builderMeta;
        }

        public DraftsBuilderMeta GenerateWithoutData(DraftBuilderType draftBuilderType)
        {
            var description = faker.Generate<DraftsBuilderMeta>(c => c.WithSkip<DraftsBuilderMeta>(x => x.BuilderData));
            description.BuilderData = null;
            description.BuilderType = draftBuilderType.ToUrn();
            return description;
        }
    }
}