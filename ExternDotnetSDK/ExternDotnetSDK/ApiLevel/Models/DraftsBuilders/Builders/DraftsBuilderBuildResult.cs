using System;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Builders
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class DraftsBuilderBuildResult
    {
        /// <summary>
        /// Идентификаторы черновиков, сформированных в результате сборки DraftsBuilder
        /// </summary>
        public Guid[] DraftIds { get; set; }

        /// <summary>
        /// Документы, в которых были выявлены ошибки при сборке
        /// </summary>
        public DraftsBuilderBuildErrorDocumentResult[] ErrorDraftsBuilderDocuments { get; set; }
    }
}