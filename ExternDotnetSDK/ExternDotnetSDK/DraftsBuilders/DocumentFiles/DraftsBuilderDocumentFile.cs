using System;
using ExternDotnetSDK.Common;

namespace ExternDotnetSDK.DraftsBuilders.DocumentFiles
{
    public class DraftsBuilderDocumentFile
    {
        public Guid Id { get; set; }
        public Guid DraftsBuilderId { get; set; }
        public Guid DraftsBuilderDocumentId { get; set; }
        public Link ContentLink { get; set; }
        public Link SignatureContentLink { get; set; }
        public DraftsBuilderDocumentFileMeta Meta { get; set; }
    }
}