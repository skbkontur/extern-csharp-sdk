using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.DocumentFiles.Data;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Enums;

namespace Kontur.Extern.Api.Client.Models.DraftsBuilders.DocumentFiles
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DraftsBuilderDocumentFileMeta : IDraftsBuilderMeta<DraftsBuilderDocumentFileData>
    {
        /// <summary>
        /// Название файла
        /// </summary>
        ///[Required]
        public string FileName { get; set; } = null!;

        /// <summary>
        /// Тип DraftsBuilder
        /// </summary>
        //[Required]
        public DraftBuilderType BuilderType { get; set; }

        /// <summary>
        /// Дополнительная информация о файле
        /// </summary>
        public DraftsBuilderDocumentFileData BuilderData { get; set; } = null!;
    }
}