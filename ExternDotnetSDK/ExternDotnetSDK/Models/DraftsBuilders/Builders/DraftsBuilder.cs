using System;
using KeApiOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiOpenSdk.Models.DraftsBuilders.Builders
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DraftsBuilder
    {
        public Guid Id { get; set; }
        public DraftsBuilderMeta Meta { get; set; }
        public DraftsBuilderStatus Status { get; set; }
    }
}