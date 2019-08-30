using System;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.DraftsBuilders.Builders
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DraftsBuilderBuildResult
    {
        public Guid[] DraftIds { get; set; }
        public DraftsBuilderBuildErrorDocumentResult[] ErrorDraftsBuilderDocuments { get; set; }
    }
}