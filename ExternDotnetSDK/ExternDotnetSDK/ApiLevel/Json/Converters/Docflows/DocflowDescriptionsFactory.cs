#nullable enable
using System;
using System.Collections.Generic;
using Kontur.Extern.Client.ApiLevel.Models.Docflows;
using Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions;
using Kontur.Extern.Client.Model.Docflows;

namespace Kontur.Extern.Client.ApiLevel.Json.Converters.Docflows
{
    internal static class DocflowDescriptionsFactory
    {
        private static readonly Dictionary<DocflowType, Func<DocflowDescription>> DescriptionFactoriesMap = new()
        {
            [DocflowType.Fns.Fns534.Report] = () => new ReportDescription(),
            [DocflowType.Fns.Fns534.Demand] = () => new DemandDescription(),
            [DocflowType.Fns.Fns534.Submission] = () => new SubmissionDescription(),
            [DocflowType.Fns.Fns534.Inventory] = () => new SubmissionDescription(),
            [DocflowType.Fns.Fns534.Ion] = () => new IonDescription(),
            [DocflowType.Fns.Fns534.Letter] = () => new LetterDescription(),
            [DocflowType.Fns.Fns534.CuLetter] = () => new CuLetterDescription(),
            [DocflowType.Fns.Fns534.CuBroadcast] = () => new CuLetterBroadcastDescription(),
            [DocflowType.Fns.Fns534.Application] = () => new ApplicationDescription(),
            [DocflowType.Rosstat.Report] = () => new StatReportDescription(),
            [DocflowType.Rosstat.Letter] = () => new StatLetterDescription(),
            [DocflowType.Rosstat.CuLetter] = () => new StatCuLetterDescription(),
            [DocflowType.Rosstat.CuBroadcast] = () => new StatCuLetterBroadcastDescription(),
            [DocflowType.Pfr.Report] = () => new PfrReportDescription(),
            [DocflowType.Pfr.Ancillary] = () => new PfrAncillaryDescription(),
            [DocflowType.Pfr.Ios] = () => new PfrIosDescription(),
            [DocflowType.Pfr.Letter] = () => new PfrLetterDescription(),
            [DocflowType.Pfr.CuLetter] = () => new PfrCuLetterDescription(),
            [DocflowType.Fss.Report] = () => new FssReportDescription(),
            [DocflowType.Fss.SickReport] = () => new FssSickReportDescription(),
            [DocflowType.Fss.Sedo.ProviderSubscription] = () => new FssSedoDescription(),
            [DocflowType.Fss.Sedo.AbonentSubscription] = () => new FssSedoAbonentSubscriptionDescription(),
            [DocflowType.Fss.Sedo.Error] = () => new FssSedoDescription(),
            [DocflowType.Fss.Sedo.AbonentSubscriptionResult] = () => new FssSedoAbonentSubscriptionResultDescription(),
            [DocflowType.Fss.Sedo.PvsoNotification] = () => new FssSedoPvsoNotificationDescription(),
            [DocflowType.Fss.Sedo.SickReportChangeNotification] = () => new FssSedoDescription(),
            [DocflowType.Fss.OvedConfirmation] = () => new OvedConfirmationDescription(),
            [DocflowType.Cbrf.Report] = () => new CbrfReportDescription(),
            [DocflowType.Fns.BusinessRegistration] = () => new BusinessRegistrationDescription()
        };

        public static DocflowDescription? TryCreateEmptyDescription(DocflowType type) => 
            DescriptionFactoriesMap.TryGetValue(type, out var factory) ? factory() : null;
    }
}