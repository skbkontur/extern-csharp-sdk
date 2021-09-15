using System.Collections.Generic;
using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Json.Converters.Docflows;
using Kontur.Extern.Api.Client.Models.Docflows.Enums;
using Kontur.Extern.Api.Client.UnitTests.TestHelpers;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.UnitTests.ApiLevel.Json.Converters.Docflows
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