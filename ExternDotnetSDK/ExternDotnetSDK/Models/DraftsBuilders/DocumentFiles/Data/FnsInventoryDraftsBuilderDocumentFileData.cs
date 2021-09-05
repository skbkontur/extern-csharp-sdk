using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Models.DraftsBuilders.DocumentFiles.Data
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class FnsInventoryDraftsBuilderDocumentFileData : DraftsBuilderDocumentFileData
    {
        /// <summary>
        /// Порядковый номер файла в многостраничном документе. Если документ одностраничный, то файл будет один и передавать в параметре 1 не обязательно.
        /// Пример использования параметра: "3" будет означать, что данный файл — третья страница в документе. 
        /// </summary>
        public int ScannedFileOrder { get; set; }
    }
}