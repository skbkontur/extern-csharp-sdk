using System;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.DraftsBuilders.Builders
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DraftsBuilderBuildErrorDocumentResult
    {
        public Guid DocumentId { get; set; }
        public string ErrorMessage { get; set; }
    }
}