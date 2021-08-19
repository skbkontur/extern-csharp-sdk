#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Check;

namespace Kontur.Extern.Client.Model.Drafts.LongOperationStatuses
{
    public class DraftCheckingStatus
    {
        public static DraftCheckingStatus From(CheckResult result)
        {
            if (result is null)
                throw new ArgumentNullException(nameof(result));

            return From(result.Data);
        }
        
        public static DraftCheckingStatus From(CheckResultData checkResultData)
        {
            if (checkResultData is null)
                throw new ArgumentNullException(nameof(checkResultData));

            return new DraftCheckingStatus(
                GetCommonErrors(checkResultData.CommonErrors),
                GetDocumentsErrors(checkResultData.DocumentsErrors)
            );

            static DraftDocumentCheckingStatus[]? GetDocumentsErrors(Dictionary<Guid, CheckError[]>? dataDocumentsErrors)
            {
                return dataDocumentsErrors is null or {Count: 0} 
                    ? null 
                    : dataDocumentsErrors.Where(x => x.Value is {Length: > 0}).Select(x => new DraftDocumentCheckingStatus(x.Key, x.Value)).ToArray();
            }

            CheckError[]? GetCommonErrors(CheckError[]? commonErrors) => 
                commonErrors is null or {Length: 0} ? null : commonErrors.Where(x => x != null).ToArray();
        }

        private DraftCheckingStatus(CheckError[]? dataCommonErrors, DraftDocumentCheckingStatus[]? documentsErrors)
        {
            CommonErrors = dataCommonErrors ?? Array.Empty<CheckError>();
            DocumentsErrors = documentsErrors ?? Array.Empty<DraftDocumentCheckingStatus>();
        }

        public bool IsSuccessful => CommonErrors.Length == 0 && DocumentsErrors.Length == 0;
        
        public CheckError[] CommonErrors { get; }
        public DraftDocumentCheckingStatus[] DocumentsErrors { get; }
    }
}