using System;
using AutoBogus;
using Kontur.Extern.Client.Model.Docflows;
using Kontur.Extern.Client.Models.Docflows;
using Kontur.Extern.Client.Models.Docflows.Descriptions;
using Kontur.Extern.Client.Tests.TestHelpers.BogusExtensions;

namespace Kontur.Extern.Client.Tests.ApiLevel.Clients.Models.TestDtoGenerators
{
    internal class DocflowDescriptionGenerator
    {
        private readonly IAutoFaker faker;

        public DocflowDescriptionGenerator() => faker = AutoFakerFactory.Create();

        public Docflow GenerateDocflowWithDescription(Type descriptionType, DocflowType docflowType)
        {
            var docflow = GenerateDocflowWithType(docflowType);
            docflow.Description = (DocflowDescription) faker.Generate(descriptionType);
            docflow.Type = docflowType;
            return docflow;
        }

        public Docflow GenerateDocflowWithoutDescription(DocflowType docflowType)
        {
            var docflow = GenerateDocflowWithType(docflowType);
            docflow.Description = null;
            return docflow;
        }

        private Docflow GenerateDocflowWithType(DocflowType docflowType)
        {
            var docflow = faker.Generate<Docflow>(c =>
            {
                c.WithSkip<Docflow>(nameof(Docflow.Description));
                c.WithSkip<Docflow>(nameof(Docflow.Type));
            });
            docflow.Type = docflowType;
            return docflow;
        }
    }
}