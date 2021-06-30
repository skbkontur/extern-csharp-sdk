using System;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Documents
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DraftsBuilderDocument
    {
        public Guid Id { get; set; }
        public Guid DraftsBuilderId { get; set; }
        public DraftsBuilderDocumentMeta Meta { get; set; }
    }
}