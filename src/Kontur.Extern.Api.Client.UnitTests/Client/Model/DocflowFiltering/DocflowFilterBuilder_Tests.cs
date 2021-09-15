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
            var expectedFilter = new DocflowFilter();
            var docflowFilterBuilder = new DocflowFilterBuilder();

            var docflowFilter = docflowFilterBuilder.CreateFilter();
            
            docflowFilter.Should().BeEquivalentTo(expectedFilter);
        }
        
        [Test]
        public void Should_build_filter_with_non_dependent_fields()
        {
            var createdFrom = new DateOnly(2021, 07, 08);
            var createdTo = new DateOnly(2021, 07, 18);
            var orgId = Guid.Parse("047AE3BA-6F78-48BB-8460-A59A11C65C2E");
            var periodFrom = new DateOnly(2021, 07, 08);
            var periodTo = new DateOnly(2021, 07, 18);
            var expectedFilter = new DocflowFilter
            {
                Cu = "123-456",
                Finished = true,
                Incoming = false,
                Knd = "1234567",
                Okpo = "12345678",
                Okud = "1234567",
                InnKpp = "1234567890-123456789",
                RegNumber = "123-456-789012",
                Types = new[] {DocflowType.Fns.Fns534.Report},
                FormName = "the form",
                CreatedFrom = createdFrom,
                CreatedTo = createdTo,
                DemandsOnReports = true,
                ForAllUsers = false,
                OrgId = orgId,
                PeriodFrom = periodFrom,
                PeriodTo = periodTo
            };

            var docflowFilter = new DocflowFilterBuilder()
                .WithFinishedDocflows()
                .WithIncomingDocflows(false)
                .WithCu(AuthorityCode.Pfr.Parse("123-456"))
                .WithKnd(Knd.Parse("1234567"))
                .WithOkpo(Okpo.LegalEntity.Parse("12345678"))
                .WithInnKppOfALegalEntity(InnKpp.Parse("1234567890-123456789"))
                .WithOkud(Okud.Parse("1234567"))
                .WithRegNumberOfPfrDocflow(PfrRegNumber.Parse("123-456-789012"))
                .WithTypes(DocflowType.Fns.Fns534.Report)
                .WithFormName("the form")
                .WithCreatedFrom(createdFrom)
                .WithCreatedTo(createdTo)
                .WithDemandsOnReports()
                .ForAllUsers(false)
                .WithOrganizationId(orgId)
                .WithReportingPeriod(periodFrom, periodTo)
                .CreateFilter();
            
            docflowFilter.Should().BeEquivalentTo(expectedFilter);
        }

        [Test]
        public void Should_apply_sorting_filter()
        {
            var expectedFilter = new DocflowFilter
            {
                OrderBy = SortOrder.Ascending 
            };
            
            var docflowFilter = new DocflowFilterBuilder()
                .WithSortingFilter(DocflowSortingFilter.OrderByCreationDate(SortOrder.Ascending))
                .CreateFilter();
            
            docflowFilter.Should().BeEquivalentTo(expectedFilter);
        }

        [Test]
        public void Should_apply_inn_filter()
        {
            var expectedFilter = new DocflowFilter
            {
                InnKpp = "123456789012"
            };
            
            var docflowFilter = new DocflowFilterBuilder()
                .WithInnKppOfALegalEntity(InnKpp.Parse("1234567890-123456789"))
                .WithIndividualEntrepreneurInn(Inn.Parse("123456789012"))
                .CreateFilter();
            
            docflowFilter.Should().BeEquivalentTo(expectedFilter);
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
            var from = new DateOnly(2021, 07, 08);
            var to = from.AddDays(-1);
            var docflowFilterBuilder = new DocflowFilterBuilder().WithCreatedFrom(from);

            Action action = () => docflowFilterBuilder.WithCreatedTo(to);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void WithCreatedFrom_should_fail_when_created_from_is_bigger_than_created_to_filter()
        {
            var from = new DateOnly(2021, 07, 08);
            var to = from.AddDays(-1);
            var docflowFilterBuilder = new DocflowFilterBuilder().WithCreatedTo(to);
            
            Action action = () => docflowFilterBuilder.WithCreatedFrom(from);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void WithCreatedFrom_should_set_created_from_filter_without_created_to_bound()
        {
            var from = new DateOnly(2021, 07, 08);
            var expectedFilter = new DocflowFilter
            {
                CreatedFrom = from
            };
            var docflowFilterBuilder = new DocflowFilterBuilder().WithCreatedFrom(from);

            var docflowFilter = docflowFilterBuilder.CreateFilter();
            
            docflowFilter.Should().BeEquivalentTo(expectedFilter);
        }

        [Test]
        public void WithCreatedTo_should_set_created_to_filter_without_created_from_bound()
        {
            var to = new DateOnly(2021, 07, 08);
            var expectedFilter = new DocflowFilter
            {
                CreatedTo = to
            };

            var docflowFilter = new DocflowFilterBuilder().WithCreatedTo(to).CreateFilter();
            
            docflowFilter.Should().BeEquivalentTo(expectedFilter);
        }
    }
}