using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.DocumentFiles.Data;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.DocumentFiles
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DraftsBuilderDocumentFileMeta : IDraftsBuilderMeta<DraftsBuilderDocumentFileData>
    {
        /// <summary>
        /// Название файла
        /// </summary>
        ///[Required]
        public string FileName { get; set; }
        
        /// <summary>
        /// Тип DraftsBuilder
        /// </summary>
        //[Required]
        public Urn BuilderType { get; set; }
        
        /// <summary>
        /// Дополнительная информация о файле
        /// </summary>
        public DraftsBuilderDocumentFileData BuilderData { get; set; }
    }
}