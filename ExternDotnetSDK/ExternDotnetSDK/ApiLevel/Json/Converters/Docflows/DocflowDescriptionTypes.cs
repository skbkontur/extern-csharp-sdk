#nullable enable
using System;
using System.Collections.Generic;
using Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions;
using Kontur.Extern.Client.Model.Docflows;

namespace Kontur.Extern.Client.ApiLevel.Json.Converters.Docflows
{
    internal static class DocflowDescriptionTypes
    {
        private static readonly Dictionary<DocflowType, Type> DescriptionTypesMap = new()
        {
            [DocflowType.Fns.Fns534.Report] = typeof(ReportDescription),
            [DocflowType.Fns.Fns534.Demand] = typeof(DemandDescription),
            [DocflowType.Fns.Fns534.Submission] = typeof(SubmissionDescription),
            [DocflowType.Fns.Fns534.Inventory] = typeof(SubmissionDescription),
            [DocflowType.Fns.Fns534.Ion] = typeof(IonDescription),
            [DocflowType.Fns.Fns534.Letter] = typeof(LetterDescription),
            [DocflowType.Fns.Fns534.CuLetter] = typeof(CuLetterDescription),
            [DocflowType.Fns.Fns534.CuBroadcast] = typeof(CuLetterBroadcastDescription),
            [DocflowType.Fns.Fns534.Application] = typeof(ApplicationDescription),
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
            [DocflowType.Fss.Sedo.ProviderSubscription] = typeof(FssSedoDescription),
            [DocflowType.Fss.Sedo.AbonentSubscription] = typeof(FssSedoAbonentSubscriptionDescription),
            [DocflowType.Fss.Sedo.Error] = typeof(FssSedoDescription),
            [DocflowType.Fss.Sedo.AbonentSubscriptionResult] = typeof(FssSedoAbonentSubscriptionResultDescription),
            [DocflowType.Fss.Sedo.PvsoNotification] = typeof(FssSedoPvsoNotificationDescription),
            [DocflowType.Fss.Sedo.SickReportChangeNotification] = typeof(FssSedoDescription),
            [DocflowType.Fss.OvedConfirmation] = typeof(OvedConfirmationDescription),
            [DocflowType.Cbrf.Report] = typeof(CbrfReportDescription),
            [DocflowType.Fns.BusinessRegistration] = typeof(BusinessRegistrationDescription)
        };

        public static Type? TryGetDescriptionType(DocflowType docflowType) => 
            DescriptionTypesMap.TryGetValue(docflowType, out var descriptionType) ? descriptionType : null;
    }
}