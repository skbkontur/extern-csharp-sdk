using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.DraftsBuilders.Documents.Data;

namespace Kontur.Extern.Client.Models.DraftsBuilders.Documents
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DraftsBuilderDocumentMeta : IDraftsBuilderMeta<DraftsBuilderDocumentData>
    {
        /// <summary>
        /// Тип DraftsBuilder
        /// </summary>
        // [Required]
        public Urn BuilderType { get; set; }
        
        /// <summary>
        /// Сведения о документе
        /// </summary>
        public DraftsBuilderDocumentData BuilderData { get; set; }
    }
}