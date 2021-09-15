using System;
using AutoBogus;
using Kontur.Extern.Api.Client.Models.DraftsBuilders;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Enums;
using Kontur.Extern.Api.Client.UnitTests.TestHelpers.BogusExtensions;

namespace Kontur.Extern.Api.Client.UnitTests.ApiLevel.Clients.Models.TestDtoGenerators
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
            var description = faker.Generate<T>(c => c.WithSkip<T>(x => x.BuilderData));
            description.BuilderData = null!;
            description.BuilderType = draftBuilderType.ToUrn() ?? throw new NullReferenceException(nameof(draftBuilderType));
            return description;
        }
    }
}