using System.Collections.Generic;
using FluentAssertions;
using Kontur.Extern.Client.ApiLevel.Json.Converters.Docflows;
using Kontur.Extern.Client.Models.Docflows.Enums;
using Kontur.Extern.Client.Tests.TestHelpers;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.ApiLevel.Clients.Models.JsonConverters
{
    [TestFixture]
    internal class DocflowDescriptionTypes_Tests
    {
        [TestCaseSource(nameof(AllDocflowTypes))]
        public void Should_return_a_description_for_docflow_type(DocflowType docflowType)
        {
            var descriptionType = DocflowDescriptionTypes.TryGetDescriptionType(docflowType);

            descriptionType.Should().NotBeNull();
        }

        [Test]
        public void Should_return_null_when_given_unknown_document_type()
        {
            var description = DocflowDescriptionTypes.TryGetDescriptionType("urn:docflow:fss-stimulative-payment");

            description.Should().BeNull();
        }

        private static IEnumerable<DocflowType> AllDocflowTypes => EnumLikeType.AllEnumValuesFromNestedTypesOfStruct<DocflowType>();
    }
}