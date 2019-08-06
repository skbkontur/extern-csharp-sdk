using System;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.DraftsBuilders.Documents
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DraftsBuilderDocument
    {
        public Guid Id { get; set; }
        public Guid DraftsBuilderId { get; set; }
        public DraftsBuilderDocumentMeta Meta { get; set; }
    }
}