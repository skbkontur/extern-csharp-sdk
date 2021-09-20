using System;
using AutoBogus;
using Kontur.Extern.Api.Client.Models.Docflows.Documents;
using Kontur.Extern.Api.Client.Models.Docflows.Documents.Enums;
using Kontur.Extern.Api.Client.Models.Docflows.Documents.Requisites;
using Kontur.Extern.Api.Client.UnitTests.ApiLevel.Clients.Models.TestDtoGenerators.AutoFaker;
using Kontur.Extern.Api.Client.UnitTests.TestHelpers.BogusExtensions;

namespace Kontur.Extern.Api.Client.UnitTests.ApiLevel.Clients.Models.TestDtoGenerators.Docflow
{
    internal class DocflowDocumentDescriptionGenerator
    {
        private readonly IAutoFaker faker;

        public DocflowDocumentDescriptionGenerator() => faker = AutoFakerFactory.CreateFaker();

        public DocflowDocumentDescription GenerateWithRequisites(Type requisitesType, DocumentType documentType)
        {
            var description = GenerateWithoutRequisites(documentType);
            description.Requisites = (DocflowDocumentRequisites?) faker.Generate(requisitesType);
            return description;
        }

        public DocflowDocumentDescription GenerateWithoutRequisites(DocumentType documentType)
        {
            var description = faker.Generate<DocflowDocumentDescription>(c => c.WithSkip<DocflowDocumentDescription>(x => x.Requisites));
            description.Requisites = null;
            description.Type = documentType;
            return description;
        }
    }
}