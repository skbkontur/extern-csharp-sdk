#nullable enable
using System;
using System.Collections.Generic;
using Kontur.Extern.Client.ApiLevel.Models.Documents.Requisites;
using Kontur.Extern.Client.Model.Documents;

namespace Kontur.Extern.Client.ApiLevel.Json.Converters.Docflows
{
    internal static class DocumentDescriptionRequisitesTypes
    {
        private static readonly Dictionary<DocumentType, Type> RequisitesTypesMap = new()
        {
            [DocumentType.Fns.Fns534Demand.Attachment] = typeof(DemandAttachmentRequisites),
            [DocumentType.Pfr.PfrReport.Report] = typeof(PfrReportRequisites),
            [DocumentType.Pfr.PfrReportV2.Report] = typeof(PfrReportRequisites)
        };

        public static Type GetRequisiteType(DocumentType documentType)
        {
            return RequisitesTypesMap.TryGetValue(documentType, out var descriptionType)
                ? descriptionType
                : documentType.IsBelongTo(DocumentType.Fns.BusinessRegistration.NameSpace)
                    ? typeof (BusinessRegistrationDocflowDocumentRequisites)
                    : typeof (CommonDocflowDocumentRequisites);
        }
    }
}