using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents.Data;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Enums;

namespace Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DraftsBuilderDocumentMeta : IDraftsBuilderMeta<DraftsBuilderDocumentData>
    {
        /// <summary>
        /// Тип DraftsBuilder
        /// </summary>
        // [Required]
        public DraftBuilderType BuilderType { get; set; }

        /// <summary>
        /// Сведения о документе
        /// </summary>
        public DraftsBuilderDocumentData BuilderData { get; set; } = null!;
    }
}