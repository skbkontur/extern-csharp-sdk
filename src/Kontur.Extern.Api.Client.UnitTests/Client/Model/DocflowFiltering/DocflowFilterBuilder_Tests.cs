using System;
using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Docflows;
using Kontur.Extern.Api.Client.Common.Time;
using Kontur.Extern.Api.Client.Model.DocflowFiltering;
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

            ShouldHaveExpectedQueryParameters(
                docflowFilter,
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

        [TestCaseSource(nameof(ControlUnitCases))]
        public void Should_create_correct_control_unit_filter(AuthorityCode cu)
        {
            var filter = new DocflowFilterBuilder()
                .WithCu(cu)
                .CreateFilter();

            ShouldHaveExpectedQueryParameters(filter, ("cu", cu.ToString()));
        }

        [Test]
        public void Should_create_correct_control_unit_filter_when_cu_is_null()
        {
            var filter = new DocflowFilterBuilder()
                .CreateFilter();
            filter.SetCu(null);

            filter.ToQueryParameters().Should().BeEmpty();
        }

        private static readonly AuthorityCode[] ControlUnitCases =
        {
            AuthorityCode.Pfr.Parse("000-007"),
            AuthorityCode.Fss.Parse("00007"),
            AuthorityCode.Fns.Parse("0007"),
            AuthorityCode.Rosstat.Parse("00-07")
        };

        [Test]
        public void Should_create_correct_filter_with_non_existing_type_in_sdk()
        {
            var filter = new DocflowFilterBuilder()
                .WithTypes(new DocflowType("urn:docflow:unknown-type"))
                .CreateFilter();

            ShouldHaveExpectedQueryParameters(filter, ("type", "unknown-type"));
        }

        [Test]
        public void Should_correct_create_filter_by_types()
        {
            var types = new[]
            {
                DocflowType.Pfr.Report,
                DocflowType.Cbrf.Report,
                DocflowType.Fns.Fns534.Inventory,
                DocflowType.Fns.BusinessRegistration
            };

            var filter = new DocflowFilterBuilder().WithTypes(types).CreateFilter();

            ShouldHaveExpectedQueryParameters(
                filter,
                ("type", DocflowType.Pfr.Report.ToUrn()!.Nss),
                ("type", DocflowType.Cbrf.Report.ToUrn()!.Nss),
                ("type", DocflowType.Fns.Fns534.Inventory.ToUrn()!.Nss),
                ("type", DocflowType.Fns.BusinessRegistration.ToUrn()!.Nss));
        }

        [TestCaseSource(nameof(TypeCases))]
        public void Should_create_correct_filter_with_existing_docflow_type(DocflowType type)
        {
            var filter = new DocflowFilterBuilder()
                .WithTypes(type)
                .CreateFilter();

            ShouldHaveExpectedQueryParameters(filter, ("type", type.ToUrn()!.Nss));
        }

        private static readonly DocflowType[] TypeCases =
        {
            DocflowType.Pfr.Ancillary,
            DocflowType.Pfr.Letter,
            DocflowType.Pfr.Report,
            DocflowType.Cbrf.Report,
            DocflowType.Fns.Fns534.CuLetter,
            DocflowType.Fns.Fns534.Inventory,
            DocflowType.Fns.BusinessRegistration,
            DocflowType.Fss.Report,
            DocflowType.Fss.SickReport,
            DocflowType.Fss.Sedo.PvsoNotification,
            DocflowType.Rosstat.CuBroadcast
        };

        [Test]
        public void Should_apply_inn_filter()
        {
            var docflowFilter = new DocflowFilterBuilder()
                .WithInnKppOfALegalEntity(InnKpp.Parse("1234567890-123456789"))
                .WithIndividualEntrepreneurInn(Inn.Parse("123456789012"))
                .CreateFilter();

            ShouldHaveExpectedQueryParameters(docflowFilter, ("innKpp", "123456789012"));
        }

        private static void ShouldHaveExpectedQueryParameters(DocflowFilter docflowFilter, params (string name, string value)[] expectedQueryParameters)
        {
            docflowFilter.ToQueryParameters().Should().BeEquivalentTo(expectedQueryParameters);
        }
    }
}