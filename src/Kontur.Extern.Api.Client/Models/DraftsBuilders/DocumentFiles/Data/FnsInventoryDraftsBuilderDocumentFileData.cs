using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.DraftsBuilders.DocumentFiles.Data
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class FnsInventoryDraftsBuilderDocumentFileData : DraftsBuilderDocumentFileData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scannedFileOrder">
        /// Порядковый номер файла в многостраничном документе. Если документ одностраничный, то файл будет один и передавать в параметре 1 не обязательно. Пример использования параметра: "3" будет означать, что данный файл — третья страница в документе.
        /// </param>
        public FnsInventoryDraftsBuilderDocumentFileData(int scannedFileOrder) => 
            ScannedFileOrder = scannedFileOrder;

        /// <summary>
        /// Порядковый номер файла в многостраничном документе. Если документ одностраничный, то файл будет один и передавать в параметре 1 не обязательно.
        /// Пример использования параметра: "3" будет означать, что данный файл — третья страница в документе. 
        /// </summary>
        public int ScannedFileOrder { get; }
        
        /// <summary>
        /// Флаг, что прикладываемый файл зашифрован. Значение по умолчанию: false
        /// </summary>
        public bool IsFileEncrypted { get; set; }
    }
}