using System;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Drafts.Check;

namespace Kontur.Extern.Api.Client.Model.Drafts.LongOperationStatuses
{
    public class DraftDocumentCheckingStatus
    {
        public DraftDocumentCheckingStatus(Guid documentId, CheckError[] errors)
        {
            DocumentId = documentId;
            Errors = errors ?? throw new ArgumentNullException(nameof(errors));
        }

        public Guid DocumentId { get; }
        public CheckError[] Errors { get; }
    }
}