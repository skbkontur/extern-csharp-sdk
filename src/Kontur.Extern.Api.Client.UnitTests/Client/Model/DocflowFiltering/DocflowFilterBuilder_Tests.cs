using System;
using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Docflows;
using Kontur.Extern.Api.Client.Common.Time;
using Kontur.Extern.Api.Client.Model.DocflowFiltering;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Models.Docflows.Enums;
using Kontur.Extern.Api.Client.Models.Numbers;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.UnitTests.Client.Model.DocflowFiltering
{
    [TestFixture]
    internal class DocflowFilterBuilder_Tests
    {
        [Test]
        public void Should_create_an_empty_filter_when_when_builder_does_not_specify_anything()
        {
            var docflowFilterBuilder = new DocflowFilterBuilder();

            var docflowFilter = docflowFilterBuilder.CreateFilter();
            
            ShouldHaveExpectedQueryParameters(docflowFilter, Array.Empty<(string name, string value)>());
        }
        
        [Test]
        public void Should_build_filter_with_non_dependent_fields()
        {
            var createdFrom = new DateTime(2021, 07, 08, 01, 01, 01);
            var createdTo = new DateTime(2021, 07, 18, 01, 01, 01);
            var orgId = Guid.Parse("047AE3BA-6F78-48BB-8460-A59A11C65C2E");
            var periodFrom = new DateOnly(2021, 07, 08);
            var periodTo = new DateOnly(2021, 07, 18);

            var docflowFilter = new DocflowFilterBuilder()
                .WithFinishedDocflows()
                .WithIncomingDocflows(false)
                .WithCu(AuthorityCode.Pfr.Parse("123-456"))
                .WithKnd(Knd.Parse("1234567"))
                .WithOkpo(Okpo.LegalEntity.Parse("12345678"))
                .WithInnKppOfALegalEntity(InnKpp.Parse("1234567890-123456789"))
                .WithOkud(Okud.Parse("1234567"))
                .WithRegNumberOfPfrDocflow(PfrRegNumber.Parse("123-456-789012"))
                .WithTypes(DocflowType.Fns.Fns534.Report, DocflowType.Fns.Fns534.Letter)
                .WithFormName("the form")
                .WithCreatedFrom(createdFrom)
                .WithCreatedTo(createdTo)
                .WithDemandsOnReports()
                .ForAllUsers(false)
                .WithOrganizationId(orgId)
                .WithReportingPeriod(periodFrom, periodTo)
                .CreateFilter();

            ShouldHaveExpectedQueryParameters(docflowFilter,
                ("cu", "123-456"),
                ("finished", "true"),
                ("incoming", "false"),
                ("knd", "1234567"),
                ("okpo", "12345678"),
                ("okud", "1234567"),
                ("innKpp", "1234567890-123456789"),
                ("regNumber", "123-456-789012"),
                ("formName", "the form"),
                ("createdFrom", "2021-07-08T01:01:01.0000000"),
                ("createdTo", "2021-07-18T01:01:01.0000000"),
                ("demandsOnReports", "true"),
                ("forAllUsers", "false"),
                ("orgId", orgId.ToString()),
                ("periodFrom", "2021-07-08T00:00:00.0000000"),
                ("periodTo", "2021-07-18T00:00:00.0000000"),
                ("type", "fns534-report"),
                ("type", "fns534-letter")
            );
        }

        [Test]
        public void Should_apply_sorting_filter()
        {
            var docflowFilter = new DocflowFilterBuilder()
                .WithSortingFilter(DocflowSortingFilter.OrderByCreationDate(SortOrder.Ascending))
                .CreateFilter();
            
            ShouldHaveExpectedQueryParameters(docflowFilter, ("orderBy", "ascending"));
        }

        [Test]
        public void Should_apply_inn_filter()
        {
            var docflowFilter = new DocflowFilterBuilder()
                .WithInnKppOfALegalEntity(InnKpp.Parse("1234567890-123456789"))
                .WithIndividualEntrepreneurInn(Inn.Parse("123456789012"))
                .CreateFilter();
            
            ShouldHaveExpectedQueryParameters(docflowFilter, ("innKpp", "123456789012"));
        }

        [Test]
        public void Should_fail_when_reporting_period_bounds_are_invalid()
        {
            var from = new DateOnly(2021, 07, 08);
            var to = from.AddDays(-1);
            
            Action action = () => new DocflowFilterBuilder().WithReportingPeriod(from, to);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void WithCreatedTo_should_fail_when_created_from_is_bigger_than_created_to_filter()
        {
            var from = new DateTime(2021, 07, 08, 01, 01, 01);
            var to = from.AddDays(-1);
            var docflowFilterBuilder = new DocflowFilterBuilder().WithCreatedFrom(from);

            Action action = () => docflowFilterBuilder.WithCreatedTo(to);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void WithCreatedFrom_should_fail_when_created_from_is_bigger_than_created_to_filter()
        {
            var from = new DateTime(2021, 07, 08, 01, 01, 01);
            var to = from.AddDays(-1);
            var docflowFilterBuilder = new DocflowFilterBuilder().WithCreatedTo(to);
            
            Action action = () => docflowFilterBuilder.WithCreatedFrom(from);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void WithCreatedFrom_should_set_created_from_filter_without_created_to_bound()
        {
            var from = new DateTime(2021, 07, 08, 01, 01, 01);
            var docflowFilterBuilder = new DocflowFilterBuilder().WithCreatedFrom(from);

            var docflowFilter = docflowFilterBuilder.CreateFilter();
            
            ShouldHaveExpectedQueryParameters(docflowFilter, ("createdFrom", "2021-07-08T01:01:01.0000000"));
        }

        [Test]
        public void WithCreatedTo_should_set_created_to_filter_without_created_from_bound()
        {
            var to = new DateTime(2021, 07, 08, 01, 01, 01);

            var docflowFilter = new DocflowFilterBuilder().WithCreatedTo(to).CreateFilter();
            
            ShouldHaveExpectedQueryParameters(docflowFilter, ("createdTo", "2021-07-08T01:01:01.0000000"));
        }
        
        private static void ShouldHaveExpectedQueryParameters(DocflowFilter docflowFilter, params (string name, string value)[] expectedQueryParameters)
        {
            docflowFilter.ToQueryParameters().Should().BeEquivalentTo(expectedQueryParameters);
        }
    }
}