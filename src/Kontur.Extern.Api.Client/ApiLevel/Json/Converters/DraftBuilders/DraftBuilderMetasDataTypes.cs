using System;
using System.Collections.Generic;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data.BusinessRegistration;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.DocumentFiles.Data;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents.Data;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents.Data.BusinessRegistration;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents.Data.FnsInventory;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Enums;

namespace Kontur.Extern.Api.Client.ApiLevel.Json.Converters.DraftBuilders
{
    internal static class DraftBuilderMetasDataTypes
    {
        private static readonly Dictionary<DraftBuilderType, Type> BuildersDataTypesMap = new()
        {
            [DraftBuilderType.Fns.Fns534.Inventory] = typeof (FnsInventoryDraftsBuilderData),
            [DraftBuilderType.Fns.BusinessRegistration.Registration] = typeof (BusinessRegistrationDraftsBuilderData),
            [DraftBuilderType.Pfr.Report] = typeof (PfrReportDraftsBuilderData)
        };
        
        private static readonly Dictionary<DraftBuilderType, Type> BuilderDocumentDataMap = new()
        {
            [DraftBuilderType.Fns.Fns534.Inventory] = typeof (FnsInventoryDraftsBuilderDocumentData),
            [DraftBuilderType.Fns.BusinessRegistration.Registration] = typeof (BusinessRegistrationDraftsBuilderDocumentData),
            [DraftBuilderType.Pfr.Report] = typeof (PfrReportDraftsBuilderDocumentData)
        };
        
        private static readonly Dictionary<DraftBuilderType, Type> BuilderDocumentFileDataMap = new()
        {
            [DraftBuilderType.Fns.Fns534.Inventory] = typeof (FnsInventoryDraftsBuilderDocumentFileData),
            [DraftBuilderType.Fns.BusinessRegistration.Registration] = typeof (BusinessRegistrationDraftsBuilderDocumentFileData),
            [DraftBuilderType.Pfr.Report] = typeof (PfrReportDraftsBuilderDocumentFileData)
        };
    
        public static Type? TryGetBuildersDataType(DraftBuilderType draftBuilderType) => 
            BuildersDataTypesMap.TryGetValue(draftBuilderType, out var dataType) ? dataType : null;
        
        public static Type? TryGetBuilderDocumentDataType(DraftBuilderType draftBuilderType) => 
            BuilderDocumentDataMap.TryGetValue(draftBuilderType, out var dataType) ? dataType : null;
        
        public static Type? TryGetBuilderDocumentFileDataType(DraftBuilderType draftBuilderType) => 
            BuilderDocumentFileDataMap.TryGetValue(draftBuilderType, out var dataType) ? dataType : null;
    }
}