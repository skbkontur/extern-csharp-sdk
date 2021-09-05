using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.Models.Common;

namespace Kontur.Extern.Client.Models.DraftsBuilders
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public interface IDraftsBuilderMeta<TData>
    {
        /// <summary>
        /// Тип DraftsBuilder
        /// </summary>
        // [Required]
        Urn BuilderType { get; set; }
        
        /// <summary>
        /// Сведения о документе
        /// </summary>
        TData BuilderData { get; set; }
    }
}