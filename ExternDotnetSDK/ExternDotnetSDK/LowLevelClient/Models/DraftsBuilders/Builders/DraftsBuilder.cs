using System;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.DraftsBuilders.Builders
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DraftsBuilder
    {
        public Guid Id { get; set; }
        public DraftsBuilderMeta Meta { get; set; }
        public DraftsBuilderStatus Status { get; set; }
    }
}