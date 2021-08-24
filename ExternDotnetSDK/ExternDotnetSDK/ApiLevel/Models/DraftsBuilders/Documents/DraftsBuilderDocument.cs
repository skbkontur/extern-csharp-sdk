using System;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Documents
{
    public class DraftsBuilderDocument
    {
        public Guid Id { get; set; }
        public Guid DraftsBuilderId { get; set; }
        public DraftsBuilderDocumentMeta Meta { get; set; }
    }
}