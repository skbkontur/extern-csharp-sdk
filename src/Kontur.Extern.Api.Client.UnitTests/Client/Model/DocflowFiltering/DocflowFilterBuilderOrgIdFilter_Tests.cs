using System;
using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Docflows;
using Kontur.Extern.Api.Client.Model.DocflowFiltering;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.UnitTests.Client.Model.DocflowFiltering
{
    class DocflowFilterBuilderOrgIdFilter_Tests
    {
        [Test]
        public void Should_correct_create_filter_by_org_id()
        {
            var id = Guid.NewGuid();
            var filter = new DocflowFilterBuilder()
                .WithOrganizationId(id)
                .CreateFilter();

            ShouldHaveExpectedQueryParameters(filter, ("orgId", id.ToString()));
        }

        [Test]
        public void Should_correct_create_filter_by_empty_guid_in_org_id()
        {
            var id = Guid.Empty;
            var filter = new DocflowFilterBuilder()
                .WithOrganizationId(id)
                .CreateFilter();

            ShouldHaveExpectedQueryParameters(filter, ("orgId", id.ToString()));
        }

        [Test]
        public void Should_correct_create_filter_by_null_in_org_id()
        {
            var filter = new DocflowFilterBuilder().CreateFilter();
            filter.SetOrgId(null);

            filter.ToQueryParameters().Should().BeEmpty();
        }

        private static void ShouldHaveExpectedQueryParameters(DocflowFilter docflowFilter, params (string name, string value)[] expectedQueryParameters)
        {
            docflowFilter.ToQueryParameters().Should().BeEquivalentTo(expectedQueryParameters);
        }
    }
}