using System;
using AutoBogus;
using Kontur.Extern.Client.ApiLevel.Models.Docflows;
using Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions;
using Kontur.Extern.Client.Model.Docflows;
using Kontur.Extern.Client.Tests.TestHelpers.BogusExtensions;

namespace Kontur.Extern.Client.Tests.ApiLevel.Clients.Models.TestDtoGenerators
{
    internal class DocflowDescriptionGenerator
    {
        private readonly IAutoFaker faker;

        public DocflowDescriptionGenerator() => faker = AutoFakerFactory.Create();

        public Docflow GenerateDocflowWithDescription(Type descriptionType, DocflowType docflowType)
        {
            var docflow = faker.Generate<Docflow>(c => c.WithSkip<Docflow>(nameof(Docflow.Description)));
            docflow.Description = (DocflowDescription) faker.Generate(descriptionType);
            docflow.Type = docflowType.ToUrn();
            return docflow;
        }

        public Docflow GenerateDocflowWithoutDescription(DocflowType docflowType)
        {
            var docflow = faker.Generate<Docflow>(c => c.WithSkip<Docflow>(nameof(Docflow.Description)));
            docflow.Description = null;
            docflow.Type = docflowType.ToUrn();
            return docflow;
        }
    }
}