using System;
using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.DraftsBuilders.Builders
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DraftsBuilderBuildErrorDocumentResult
    {
        public Guid DocumentId { get; set; }
        public string ErrorMessage { get; set; }
    }
}