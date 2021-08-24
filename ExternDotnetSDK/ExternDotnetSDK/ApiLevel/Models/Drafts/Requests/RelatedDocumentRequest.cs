using System;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests
{
    public class RelatedDocumentRequest
    {
        public Guid RelatedDocflowId { get; set; }
        public Guid RelatedDocumentId { get; set; }
    }
}