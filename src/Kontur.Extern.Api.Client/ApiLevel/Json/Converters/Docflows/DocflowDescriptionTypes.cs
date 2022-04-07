using System;
using System.Collections.Generic;
using Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Cbrf;
using Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fns;
using Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fns.BusinessRegistration;
using Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss;
using Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Oved;
using Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Pfr;
using Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Rosstat;
using Kontur.Extern.Api.Client.Models.Docflows.Enums;

namespace Kontur.Extern.Api.Client.ApiLevel.Json.Converters.Docflows
{
    public static class DocflowDescriptionTypes
    {
        private static readonly Dictionary<DocflowType, Type> DescriptionTypesMap = new()
        {
            [DocflowType.Fns.Fns534Report] = typeof(ReportDescription),
            [DocflowType.Fns.Fns534Demand] = typeof(DemandDescription),
            [DocflowType.Fns.Fns534Submission] = typeof(SubmissionDescription),
            [DocflowType.Fns.Fns534Inventory] = typeof(SubmissionDescription),
            [DocflowType.Fns.Fns534Ion] = typeof(IonDescription),
            [DocflowType.Fns.Fns534Letter] = typeof(LetterDescription),
            [DocflowType.Fns.Fns534CuLetter] = typeof(CuLetterDescription),
            [DocflowType.Fns.Fns534CuBroadcast] = typeof(CuLetterBroadcastDescription),
            [DocflowType.Fns.Fns534Application] = typeof(ApplicationDescription),
            [DocflowType.Rosstat.Report] = typeof(StatReportDescription),
            [DocflowType.Rosstat.Letter] = typeof(StatLetterDescription),
            [DocflowType.Rosstat.CuLetter] = typeof(StatCuLetterDescription),
            [DocflowType.Rosstat.CuBroadcast] = typeof(StatCuLetterBroadcastDescription),
            [DocflowType.Pfr.Report] = typeof(PfrReportDescription),
            [DocflowType.Pfr.Ancillary] = typeof(PfrAncillaryDescription),
            [DocflowType.Pfr.Ios] = typeof(PfrIosDescription),
            [DocflowType.Pfr.Letter] = typeof(PfrLetterDescription),
            [DocflowType.Pfr.CuLetter] = typeof(PfrCuLetterDescription),
            [DocflowType.Fss.Report] = typeof(FssReportDescription),
            [DocflowType.Fss.SickReport] = typeof(FssSickReportDescription),
            [DocflowType.Fss.SedoProviderSubscription] = typeof(FssSedoDescription),
            [DocflowType.Fss.SedoAbonentSubscription] = typeof(FssSedoAbonentSubscriptionDescription),
            [DocflowType.Fss.SedoError] = typeof(FssSedoDescription),
            [DocflowType.Fss.SedoAbonentSubscriptionResult] = typeof(FssSedoAbonentSubscriptionResultDescription),
            [DocflowType.Fss.SedoPvsoNotification] = typeof(FssSedoPvsoNotificationDescription),
            [DocflowType.Fss.SedoSickReportChangeNotification] = typeof(FssSedoSickReportChangeNotificationDescription),
            [DocflowType.Fss.SedoInsuredPersonRegistration] = typeof(FssSedoInsuredPersonRegistrationDescription),
            [DocflowType.Fss.SedoInsuredPersonRegistrationResult] = typeof(FssSedoInsuredPersonRegistrationResultDescription),
            [DocflowType.Fss.SedoProactivePaymentsDemand] = typeof(FssSedoProactivePaymentsDemandDescription),
            [DocflowType.Fss.SedoProactivePaymentsReply] = typeof(FssSedoProactivePaymentsReplyDescription),
            [DocflowType.Fss.SedoProactivePaymentsReplyResult] = typeof(FssSedoProactivePaymentsReplyResultDescription),
            [DocflowType.Fss.SedoProactivePaymentsBenefit] = typeof(FssSedoProactivePaymentsBenefitDescription),
            [DocflowType.Fss.SedoInsuredPersonMismatch] = typeof(FssSedoInsuredPersonMismatchDescription),
            [DocflowType.Fss.OvedConfirmation] = typeof(OvedConfirmationDescription),
            [DocflowType.Cbrf.Report] = typeof(CbrfReportDescription),
            [DocflowType.Fns.BusinessRegistration] = typeof(BusinessRegistrationDescription)
        };

        public static Type? TryGetDescriptionType(DocflowType docflowType) => 
            DescriptionTypesMap.TryGetValue(docflowType, out var descriptionType) ? descriptionType : null;
    }
}