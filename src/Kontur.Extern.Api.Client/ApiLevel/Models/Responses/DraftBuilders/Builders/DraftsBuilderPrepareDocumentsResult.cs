using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.DraftBuilders.Builders;

[PublicAPI]
public class DraftsBuilderPrepareDocumentsResult
{
    /// <summary>
    /// Документы, в которых были выявлены ошибки при сборке
    /// </summary>
    public DraftsBuilderPrepareErrorDocumentResult[] ErrorDraftsBuilderDocuments { get; set; } = null!;
}