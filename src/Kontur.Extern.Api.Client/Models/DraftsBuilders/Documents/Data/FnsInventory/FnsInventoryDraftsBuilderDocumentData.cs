using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Exceptions;

namespace Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents.Data.FnsInventory
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class FnsInventoryDraftsBuilderDocumentData : DraftsBuilderDocumentData
    {
        public FnsInventoryDraftsBuilderDocumentData(
            string claimItemNumber, 
            FnsInventoryDraftsBuilderDocumentBackgroundProcessing? backgroundProcessing,
            string? labelForGrouping,
            string? scannedDocumentName,
            FnsInventoryDraftsBuilderDocumentType? type)
        {
            if (string.IsNullOrWhiteSpace(claimItemNumber))
                throw Errors.RequiredJsonPropertyIsMissed(nameof(claimItemNumber));

            ClaimItemNumber = claimItemNumber;
            LabelForGrouping = labelForGrouping;
            ScannedDocumentName = scannedDocumentName;
            Type = type;
            BackgroundProcessing = backgroundProcessing;
        }

        /// <summary>
        /// Пункт требования — номер пункта, под которым документ указан в требовании в виде 1.ХХ или 2.ХХ
        /// </summary>
        // todo: add value type to validate format
        public string ClaimItemNumber { get; }

        /// <summary>
        /// Метка группы документов для разделения по разным описям
        /// </summary>
        public string? LabelForGrouping { get; }
        
        /// <summary>
        /// Название отсканированного документа
        /// </summary>
        public string? ScannedDocumentName { get; }

        /// <summary>
        /// Тип документа
        /// </summary>
        public FnsInventoryDraftsBuilderDocumentType? Type { get; }
        
        /// <summary>
        /// Условия для немедленной обработки документа
        /// </summary>
        public FnsInventoryDraftsBuilderDocumentBackgroundProcessing? BackgroundProcessing { get; }
    }
}