using System;
using AutoBogus;
using Kontur.Extern.Api.Client.Models.Docflows.Descriptions;
using Kontur.Extern.Api.Client.Models.Docflows.Enums;
using Kontur.Extern.Api.Client.UnitTests.ApiLevel.Clients.Models.TestDtoGenerators.AutoFaker;
using Kontur.Extern.Api.Client.UnitTests.TestHelpers.BogusExtensions;

namespace Kontur.Extern.Api.Client.UnitTests.ApiLevel.Clients.Models.TestDtoGenerators.Docflow
{
    internal class DocflowDescriptionGenerator
    {
        private readonly IAutoFaker faker;

        public DocflowDescriptionGenerator() => faker = AutoFakerFactory.CreateFaker();

        public Extern.Api.Client.Models.Docflows.Docflow GenerateDocflowWithDescription(Type descriptionType, DocflowType docflowType)
        {
            var docflow = GenerateDocflowWithType(docflowType);
            docflow.Description = (DocflowDescription?) faker.Generate(descriptionType);
            docflow.Type = docflowType;
            return docflow;
        }

        public Extern.Api.Client.Models.Docflows.Docflow GenerateDocflowWithoutDescription(DocflowType docflowType)
        {
            var docflow = GenerateDocflowWithType(docflowType);
            docflow.Description = null;
            return docflow;
        }

        private Extern.Api.Client.Models.Docflows.Docflow GenerateDocflowWithType(DocflowType docflowType)
        {
            var docflow = faker.Generate<Extern.Api.Client.Models.Docflows.Docflow>(c =>
            {
                c.WithSkip<Extern.Api.Client.Models.Docflows.Docflow>(nameof(Extern.Api.Client.Models.Docflows.Docflow.Description));
                c.WithSkip<Extern.Api.Client.Models.Docflows.Docflow>(nameof(Extern.Api.Client.Models.Docflows.Docflow.Type));
            });
            docflow.Type = docflowType;
            return docflow;
        }
    }
}