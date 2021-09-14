using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents.Data;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.DraftBulders.Documents
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