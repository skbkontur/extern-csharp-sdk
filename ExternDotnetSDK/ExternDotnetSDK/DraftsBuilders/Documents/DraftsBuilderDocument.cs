using System;
using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.DraftsBuilders.Documents
{
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
    public class DraftsBuilderDocument
    {
        public Guid Id { get; set; }
        public Guid DraftsBuilderId { get; set; }
        public DraftsBuilderDocumentMeta Meta { get; set; }
    }
}