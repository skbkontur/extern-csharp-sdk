using System;
using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.DraftsBuilders.Builders
{
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
    public class DraftsBuilder
    {
        public Guid Id { get; set; }
        public DraftsBuilderMeta Meta { get; set; }
        public DraftsBuilderStatus Status { get; set; }
    }
}