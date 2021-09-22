using System;
using AutoBogus;
using Kontur.Extern.Api.Client.Models.DraftsBuilders;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents.Data.FnsInventory;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Enums;
using Kontur.Extern.Api.Client.UnitTests.ApiLevel.Clients.Models.TestDtoGenerators.AutoFaker;
using Kontur.Extern.Api.Client.UnitTests.TestHelpers.BogusExtensions;

namespace Kontur.Extern.Api.Client.UnitTests.ApiLevel.Clients.Models.TestDtoGenerators.DraftsBuilders
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
            faker = new AutoFakerFactory()
                .AddDraftsBuilderEntitiesGeneration()
                .AddConfiguration((c, _) => c.RuleForPropNameOf(nameof(FnsInventoryDraftsBuilderDocumentBackgroundProcessing.TotalFileCount), x => x.Random.Int(0)))
                .Create();
        }

        public T GenerateWithData(Type? dataType, DraftBuilderType draftBuilderType)
        {
            var data = dataType is null ? unknownDataFactory() : (TData?) faker.Generate(dataType) ?? unknownDataFactory();
            return Generate(draftBuilderType, data);
        }

        public T GenerateWithoutData(DraftBuilderType draftBuilderType) =>
            faker.Generate<T>(c =>
            {
                c.RuleForType(_ => draftBuilderType);
                c.WithSkip<T>(x => x.BuilderData);
            });
        
        public T GenerateWithoutType(DraftBuilderType draftBuilderType) =>
            faker.Generate<T>(c =>
            {
                c.RuleForType(_ => draftBuilderType);
                c.WithSkip<T>(x => x.BuilderData);
            });

        private T Generate(DraftBuilderType draftBuilderType, TData? data)
        {
            return faker.Generate<T>(c =>
            {
                c.RuleForType(_ => draftBuilderType);
                c.RuleForPropNameOf(nameof(IDraftsBuilderMeta<TData>.BuilderData), _ => data);
            });
        }
    }
}