using System;
using System.Collections.Generic;
using Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Cbrf;
using Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fns;
using Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fns.BusinessRegistration;
using Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss;
using Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Oved;
using Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Pfr;
using Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Rosstat;
using Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Sfr;
using Kontur.Extern.Api.Client.Models.Docflows.Enums;

namespace Kontur.Extern.Api.Client.ApiLevel.Json.Converters.Docflows
{
    public static class DocflowDescriptionTypes
    {
        private static readonly Dictionary<DocflowType, Type> DescriptionTypesMap = new()
        {
            [DocflowType.Fns534Report] = typeof(ReportDescription),
            [DocflowType.Fns534Demand] = typeof(DemandDescription),
            [DocflowType.Fns534Submission] = typeof(SubmissionDescription),
            [DocflowType.Fns534Inventory] = typeof(SubmissionDescription),
            [DocflowType.Fns534Ion] = typeof(IonDescription),
            [DocflowType.Fns534Letter] = typeof(LetterDescription),
            [DocflowType.Fns534CuLetter] = typeof(CuLetterDescription),
            [DocflowType.Fns534CuBroadcast] = typeof(CuLetterBroadcastDescription),
            [DocflowType.Fns534Application] = typeof(ApplicationDescription),
            [DocflowType.StatReport] = typeof(StatReportDescription),
            [DocflowType.StatLetter] = typeof(StatLetterDescription),
            [DocflowType.StatCuLetter] = typeof(StatCuLetterDescription),
            [DocflowType.StatCuBroadcast] = typeof(StatCuLetterBroadcastDescription),
            [DocflowType.PfrReport] = typeof(PfrReportDescription),
            [DocflowType.SfrReport] = typeof(SfrReportDescription),
            [DocflowType.PfrAncillary] = typeof(PfrAncillaryDescription),
            [DocflowType.PfrIos] = typeof(PfrIosDescription),
            [DocflowType.PfrLetter] = typeof(PfrLetterDescription),
            [DocflowType.PfrCuLetter] = typeof(PfrCuLetterDescription),
            [DocflowType.FssReport] = typeof(FssReportDescription),
            [DocflowType.FssSickReport] = typeof(FssSickReportDescription),
            [DocflowType.FssSedoProviderSubscription] = typeof(FssSedoDescription),
            [DocflowType.FssSedoAbonentSubscription] = typeof(FssSedoAbonentSubscriptionDescription),
            [DocflowType.FssSedoAbonentSubscriptionResult] = typeof(FssSedoAbonentSubscriptionResultDescription),
            [DocflowType.FssSedoPvsoNotification] = typeof(FssSedoPvsoNotificationDescription),
            [DocflowType.FssSedoSickReportChangeNotification] = typeof(FssSedoSickReportChangeNotificationDescription),
            [DocflowType.FssSedoInsuredPersonRegistration] = typeof(FssSedoInsuredPersonRegistrationDescription),
            [DocflowType.FssSedoInsuredPersonRegistrationResult] = typeof(FssSedoInsuredPersonRegistrationResultDescription),
            [DocflowType.FssSedoProactivePaymentsDemand] = typeof(FssSedoProactivePaymentsDemandDescription),
            [DocflowType.FssSedoProactivePaymentsReply] = typeof(FssSedoProactivePaymentsReplyDescription),
            [DocflowType.FssSedoProactivePaymentsReplyResult] = typeof(FssSedoProactivePaymentsReplyResultDescription),
            [DocflowType.FssSedoProactivePaymentsBenefit] = typeof(FssSedoProactivePaymentsBenefitDescription),
            [DocflowType.FssSedoInsuredPersonMismatch] = typeof(FssSedoInsuredPersonMismatchDescription),
            [DocflowType.FssSedoBenefitPaymentInitiation] = typeof(FssSedoBenefitPaymentInitiationDescription),
            [DocflowType.FssSedoBenefitPaymentInitiationResult] = typeof(FssSedoBenefitPaymentInitiationResultDescription),
            [DocflowType.FssSedoDemand] = typeof(FssSedoDemandDescription),
            [DocflowType.FssSedoDemandReply] = typeof(FssSedoDemandReplyDescription),
            [DocflowType.FssSedoBillingInformationDemand] = typeof(FssSedoBillingInformationDemandDescription),
            [DocflowType.FssSedoBillingInformation] = typeof(FssSedoBillingInformationDescription),
            [DocflowType.FssSedoBabyCareVacationCloseNotice] = typeof(FssSedoBabyCareVacationCloseNoticeDescription),
            [DocflowType.FssSedoDisabilityChildrenDemand] = typeof(FssSedoDisabilityChildrenDemandDescription),
            [DocflowType.FssSedoBenefitPaymentStatusNotice] = typeof(FssSedoBenefitPaymentStatusNoticeDescription),
            [DocflowType.FssSedoProactiveExpireNotice] = typeof(FssSedoProactiveExpireNoticeDescription),
            [DocflowType.FssSedoError] = typeof(FssSedoErrorDescription),
            [DocflowType.FssSedoEmployeeSalaryInformation] = typeof(FssSedoEmployeeSalaryInformationDescription),
            [DocflowType.FssSedoAppealReply] = typeof(FssSedoAppealReplyDescription),
            [DocflowType.FssSedoAppeal] = typeof(FssSedoAppealDescription),
            [DocflowType.FssSedoOvedConfirmation] = typeof(FssSedoOvedConfirmationDescription),
            [DocflowType.FssSedoJudicialRestriction] = typeof(FssSedoJudicialRestrictionDescription),
            [DocflowType.FssSedoJudicialRestrictionReply] = typeof(FssSedoJudicialRestrictionReplyDescription),
            [DocflowType.FssSedoOvedConfirmation] = typeof(FssSedoOvedConfirmationDescription),
            [DocflowType.OvedConfirmation] = typeof(OvedConfirmationDescription),
            [DocflowType.CbrfReport] = typeof(CbrfReportDescription),
            [DocflowType.BusinessRegistration] = typeof(BusinessRegistrationDescription),
            [DocflowType.FssSedoBroadcastMessage] = typeof(FssSedoBroadcastMessageDescription),
            [DocflowType.FssSedoPaymentDetailsDemand] = typeof(FssSedoPaymentDetailsDemandDescription),
            [DocflowType.FssSedoPaymentDetailsDemandReply] = typeof(FssSedoPaymentDetailsDemandReplyDescription),
            [DocflowType.FssSedoAdditionalVacationStatement] = typeof(FssSedoAdditionalVacationStatementDescription),
            [DocflowType.FssSedoAdditionalVacationStatementNeedDoc] = typeof(FssSedoAdditionalVacationStatementNeedDocDescription),
            [DocflowType.FssSedoAdditionalVacationStatementDocs] = typeof(FssSedoAdditionalVacationStatementDocsDescription),
            [DocflowType.FssWarrantManagement] = typeof(FssWarrantManagementDescription)
        };

        public static Type? TryGetDescriptionType(DocflowType docflowType) =>
            DescriptionTypesMap.TryGetValue(docflowType, out var descriptionType) ? descriptionType : null;
    }
}