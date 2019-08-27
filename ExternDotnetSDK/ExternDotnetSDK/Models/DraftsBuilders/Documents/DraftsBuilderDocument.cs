using System;
using KeApiOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiOpenSdk.Models.DraftsBuilders.Documents
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DraftsBuilderDocument
    {
        public Guid Id { get; set; }
        public Guid DraftsBuilderId { get; set; }
        public DraftsBuilderDocumentMeta Meta { get; set; }
    }
}