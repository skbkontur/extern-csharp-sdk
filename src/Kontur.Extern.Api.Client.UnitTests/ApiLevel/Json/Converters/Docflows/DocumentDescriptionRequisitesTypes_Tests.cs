using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Json.Converters.Docflows;
using Kontur.Extern.Api.Client.Models.Docflows.Documents.Enums;
using Kontur.Extern.Api.Client.Models.Docflows.Documents.Requisites;
using Kontur.Extern.Api.Client.UnitTests.TestHelpers;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.UnitTests.ApiLevel.Json.Converters.Docflows
{
    [TestFixture]
    internal class DocumentDescriptionRequisitesTypes_Tests
    {
        [TestCaseSource(nameof(AllDocumentTypes))]
        public void Should_return_a_requisites_type_for_a_docflow_type(DocumentType documentType)
        {
            var requisiteType = DocumentDescriptionRequisitesTypes.GetRequisiteType(documentType);

            requisiteType.Should().NotBeNull();
        }

        [Test]
        public void Should_return_common_requisites_type_for_unknown_document_type()
        {
            var requisiteType = DocumentDescriptionRequisitesTypes.GetRequisiteType("urn:document:fss-stimulative-payment");

            requisiteType.Should().Be<CommonDocflowDocumentRequisites>();
        }

        [Test]
        public void Should_return_demands_requisites_for_demand_attachment()
        {
            var requisiteType = DocumentDescriptionRequisitesTypes.GetRequisiteType(DocumentType.Fns534Demand.Attachment);

            requisiteType.Should().Be<DemandAttachmentRequisites>();
        }

        [Test]
        public void Should_return_fss_sedo_requisites_for_demand_reply_result_document()
        {
            var requisiteType = DocumentDescriptionRequisitesTypes.GetRequisiteType(DocumentType.FssSedoDemandReply.ResultDocument);

            requisiteType.Should().Be<FssSedoDemandReplyResultDocumentRequisites>();
        }

        [Test]
        public void Should_return_pfr_requisites_for_pfr_report()
        {
            var requisiteType = DocumentDescriptionRequisitesTypes.GetRequisiteType(DocumentType.PfrReport.Report);

            requisiteType.Should().Be<PfrReportRequisites>();
        }

        [Test]
        public void Should_return_pfr_requisites_for_pfr_report_v2()
        {
            var requisiteType = DocumentDescriptionRequisitesTypes.GetRequisiteType(DocumentType.PfrReportV2.Report);

            requisiteType.Should().Be<PfrReportRequisites>();
        }

        [TestCaseSource(nameof(BusinessRegistrationDocumentTypes))]
        public void Should_return_business_registration_requisites_for_the_business_registration_documents(DocumentType documentType)
        {
            var requisiteType = DocumentDescriptionRequisitesTypes.GetRequisiteType(documentType);

            requisiteType.Should().Be<BusinessRegistrationDocflowDocumentRequisites>();
        }

        [TestCaseSource(nameof(NonSpecificRequisitesDocumentTypes))]
        public void Should_return_common_requisites_by_default(DocumentType documentType)
        {
            var requisiteType = DocumentDescriptionRequisitesTypes.GetRequisiteType(documentType);

            requisiteType.Should().Be<CommonDocflowDocumentRequisites>();
        }

        private static IEnumerable<DocumentType> NonSpecificRequisitesDocumentTypes =>
            AllDocumentTypes
                .Except(BusinessRegistrationDocumentTypes)
                .Except(new[]
                {
                    DocumentType.Fns534Demand.Attachment,
                    DocumentType.PfrReportV2.Report,
                    DocumentType.PfrReport.Report,
                    DocumentType.FssSedoDemandReply.ResultDocument
                });
        
        private static IEnumerable<DocumentType> BusinessRegistrationDocumentTypes => 
            AllDocumentTypes.Where(t => t.IsBelongTo(DocumentType.BusinessRegistration.NameSpace));
        
        private static IEnumerable<DocumentType> AllDocumentTypes => EnumLikeType.AllEnumValuesFromNestedTypesOfStruct<DocumentType>();
    }
}