#nullable enable
using System;
using System.Collections.Generic;
using Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Builders.Data;
using Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Builders.Data.BusinessRegistration;
using Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Documents.Data;
using Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Documents.Data.BusinessRegistration;
using Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Documents.Data.FnsInventory;
using Kontur.Extern.Client.Model.DraftBuilders;

namespace Kontur.Extern.Client.ApiLevel.Json.Converters.DraftBuilders
{
    internal static class DraftBuilderMetasDataTypes
    {
        private static readonly Dictionary<DraftBuilderType, Type> BuildersDataTypesMap = new()
        {
            [DraftBuilderType.Fns.Fns534.Inventory] = typeof (FnsInventoryDraftsBuilderData),
            [DraftBuilderType.Fns.BusinessRegistration.Registration] = typeof (BusinessRegistrationDraftsBuilderData),
            [DraftBuilderType.Fns.BusinessRegistration.RegistrationLegacy] = typeof (BusinessRegistrationDraftsBuilderData),
            [DraftBuilderType.Pfr.Report] = typeof (PfrReportDraftsBuilderData)
        };
        
        private static readonly Dictionary<DraftBuilderType, Type> BuilderDocumentDataMap = new()
        {
            [DraftBuilderType.Fns.Fns534.Inventory] = typeof (FnsInventoryDraftsBuilderDocumentData),
            [DraftBuilderType.Fns.BusinessRegistration.Registration] = typeof (BusinessRegistrationDraftsBuilderDocumentData),
            [DraftBuilderType.Fns.BusinessRegistration.RegistrationLegacy] = typeof (BusinessRegistrationDraftsBuilderDocumentData),
            [DraftBuilderType.Pfr.Report] = typeof (PfrReportDraftsBuilderDocumentData)
        };
    
        public static Type? TryGetBuildersDataType(DraftBuilderType draftBuilderType) => 
            BuildersDataTypesMap.TryGetValue(draftBuilderType, out var descriptionType) ? descriptionType : null;
        
        public static Type? TryGetBuilderDocumentDataType(DraftBuilderType draftBuilderType) => 
            BuilderDocumentDataMap.TryGetValue(draftBuilderType, out var descriptionType) ? descriptionType : null;
    }
}