using System;
using AutoBogus;
using Kontur.Extern.Client.ApiLevel.Models.Documents;
using Kontur.Extern.Client.ApiLevel.Models.Documents.Requisites;
using Kontur.Extern.Client.Model.Documents;
using Kontur.Extern.Client.Tests.TestHelpers.BogusExtensions;

namespace Kontur.Extern.Client.Tests.ApiLevel.Clients.Models.TestDtoGenerators
{
    internal class DocflowDocumentDescriptionGenerator
    {
        private readonly IAutoFaker faker;

        public DocflowDocumentDescriptionGenerator() => faker = AutoFakerFactory.Create();

        public DocflowDocumentDescription GenerateWithRequisites(Type requisitesType, DocumentType documentType)
        {
            var description = GenerateWithoutRequisites(documentType);
            description.Requisites = (DocflowDocumentRequisites) faker.Generate(requisitesType);
            return description;
        }

        public DocflowDocumentDescription GenerateWithoutRequisites(DocumentType documentType)
        {
            var description = faker.Generate<DocflowDocumentDescription>(c => c.WithSkip<DocflowDocumentDescription>(x => x.Requisites));
            description.Requisites = null;
            description.Type = documentType.ToUrn();
            return description;
        }
    }
}