using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Enums;

namespace Kontur.Extern.Api.Client.Models.DraftsBuilders
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public interface IDraftsBuilderMeta<TData>
    {
        /// <summary>
        /// Тип DraftsBuilder
        /// </summary>
        DraftBuilderType BuilderType { get; }
        
        /// <summary>
        /// Сведения о документе
        /// </summary>
        TData? BuilderData { get; }
    }
}