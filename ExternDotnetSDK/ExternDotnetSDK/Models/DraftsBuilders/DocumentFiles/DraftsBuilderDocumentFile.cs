using System;
using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.DraftsBuilders.DocumentFiles
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
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