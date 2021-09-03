#nullable enable
using System;
using AutoBogus;
using Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders;
using Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Documents;
using Kontur.Extern.Client.Model.DraftBuilders;
using Kontur.Extern.Client.Tests.TestHelpers.BogusExtensions;

namespace Kontur.Extern.Client.Tests.ApiLevel.Clients.Models.TestDtoGenerators
{
    internal sealed class DraftsBuilderMetasGenerator<T, TData>
        where T : class, IDraftsBuilderMeta<TData>
        where TData : class
    {
        private readonly Func<TData> unknownDataFactory;
        private readonly IAutoFaker faker;

        public DraftsBuilderMetasGenerator(Func<TData> unknownDataFactory)
        {
            this.unknownDataFactory = unknownDataFactory;
            faker = AutoFakerFactory.Create();
        }

        public T GenerateWithData(Type? dataType, DraftBuilderType draftBuilderType)
        {
            var builderMeta = GenerateWithoutData(draftBuilderType);
            if (dataType != null)
            {
                builderMeta.BuilderData = (TData?) faker.Generate(dataType) ?? unknownDataFactory();
            }
            else
            {
                builderMeta.BuilderData = unknownDataFactory();
            }

            return builderMeta;
        }

        public T GenerateWithoutData(DraftBuilderType draftBuilderType)
        {
            var description = faker.Generate<T>(c => c.WithSkip<DraftsBuilderDocumentMeta>(x => x.BuilderData));
            description.BuilderData = null!;
            description.BuilderType = draftBuilderType.ToUrn();
            return description;
        }
    }
}