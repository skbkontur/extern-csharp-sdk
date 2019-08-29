using System;
using KeApiClientOpenSdk.Models.Common;
using KeApiClientOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiClientOpenSdk.Models.DraftsBuilders.DocumentFiles
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