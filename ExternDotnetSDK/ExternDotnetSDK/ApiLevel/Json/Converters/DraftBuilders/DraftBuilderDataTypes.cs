#nullable enable
using System;
using System.Collections.Generic;
using Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Builders.Data;
using Kontur.Extern.Client.Model.DraftBuilders;

namespace Kontur.Extern.Client.ApiLevel.Json.Converters.DraftBuilders
{
    internal static class DraftBuilderDataTypes
    {
        private static readonly Dictionary<DraftBuilderType, Type> RequisitesTypesMap = new()
        {
            [DraftBuilderType.Fns.Fns534.Inventory] = typeof (FnsInventoryDraftsBuilderData),
            [DraftBuilderType.Fns.BusinessRegistration.Registration] = typeof (BusinessRegistrationDraftsBuilderData),
            [DraftBuilderType.Fns.BusinessRegistration.RegistrationLegacy] = typeof (BusinessRegistrationDraftsBuilderData),
            [DraftBuilderType.Pfr.Report] = typeof (PfrReportDraftsBuilderData)
        };
    
        public static Type? TryGetBuilderDataType(DraftBuilderType draftBuilderType) => 
            RequisitesTypesMap.TryGetValue(draftBuilderType, out var descriptionType) ? descriptionType : null;
    }
}