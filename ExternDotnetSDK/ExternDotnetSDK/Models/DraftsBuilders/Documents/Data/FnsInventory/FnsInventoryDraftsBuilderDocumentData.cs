using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Models.DraftsBuilders.Documents.Data.FnsInventory
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class FnsInventoryDraftsBuilderDocumentData : DraftsBuilderDocumentData
    {
        /// <summary>
        /// Пункт требования — номер пункта, под которым документ указан в требовании в виде 1.ХХ или 2.ХХ
        /// </summary>
        //[Required]
        public string ClaimItemNumber { get; set; }
        
        /// <summary>
        /// Метка группы документов для разделения по разным описям
        /// </summary>
        public string LabelForGrouping { get; set; }
        
        /// <summary>
        /// Название отсканированного документа
        /// </summary>
        public string ScannedDocumentName { get; set; }

        /// <summary>
        /// Тип документа
        /// </summary>
        public FnsInventoryDraftsBuilderDocumentType? Type { get; set; }
        
        /// <summary>
        /// Условия для немедленной обработки документа
        /// </summary>
        public FnsInventoryDraftsBuilderDocumentBackgroundProcessing BackgroundProcessing { get; set; }
    }
}