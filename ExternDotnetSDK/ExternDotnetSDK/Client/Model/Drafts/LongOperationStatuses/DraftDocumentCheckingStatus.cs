using System;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Check;

namespace Kontur.Extern.Client.Model.Drafts.LongOperationStatuses
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