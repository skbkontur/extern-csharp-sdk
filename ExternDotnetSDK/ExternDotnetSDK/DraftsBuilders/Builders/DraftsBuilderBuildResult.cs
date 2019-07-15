using System;

namespace ExternDotnetSDK.DraftsBuilders.Builders
{
    public class DraftsBuilderBuildResult
    {
        public Guid[] DraftIds { get; set; }
        public DraftsBuilderBuildErrorDocumentResult[] ErrorDraftsBuilderDocuments { get; set; }
    }
}