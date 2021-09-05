using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.Models.DraftsBuilders.Documents.Data;

namespace Kontur.Extern.Client.ApiLevel.Models.Requests.DraftBulders.Documents
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DraftsBuilderDocumentMetaRequest
    {
        /// <summary>
        /// Данные для создания и редактирования документа DraftsBuilder
        /// </summary>
        public DraftsBuilderDocumentData BuilderData { get; set; }
    }
}