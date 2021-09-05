using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Responses.DraftBuilders.Builders
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
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