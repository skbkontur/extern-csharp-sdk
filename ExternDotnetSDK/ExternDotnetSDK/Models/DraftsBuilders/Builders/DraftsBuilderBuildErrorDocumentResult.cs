using System;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.DraftsBuilders.Builders
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DraftsBuilderBuildErrorDocumentResult
    {
        public Guid DocumentId { get; set; }
        public string ErrorMessage { get; set; }
    }
}