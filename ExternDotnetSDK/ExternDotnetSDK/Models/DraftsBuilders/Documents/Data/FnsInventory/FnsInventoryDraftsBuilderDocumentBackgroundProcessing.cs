using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents.Data.FnsInventory
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class FnsInventoryDraftsBuilderDocumentBackgroundProcessing
    {
        /// <summary>
        /// Количество файлов в документе. Когда будет добавлено данное количество файлов, документ начнет обрабатываться и будет заблокирован для изменений. Подробнее в [документации](https://docs-ke.readthedocs.io/ru/latest/builder/%D0%BC%D0%B5%D1%82%D0%BE%D0%B4%D1%8B%20%D0%B1%D0%B8%D0%BB%D0%B4%D0%B5%D1%80%D0%B0.html#rst-markup-createdocdb)
        /// </summary>
        //[Required]
        public int TotalFileCount { get; set; }
    }
}