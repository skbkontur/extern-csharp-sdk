using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DraftMetaRequest
    {
        /// <summary>
        /// Отправитель
        /// </summary>
        //[Required]
        public SenderRequest Sender { get; set; }

        /// <summary>
        /// Получатель
        /// </summary>
        //[Required]
        public RecipientInfoRequest Recipient { get; set; }

        /// <summary>
        /// Налогоплательщик. Организация, за которую отправляется отчет
        /// </summary>
        //[Required]
        public AccountInfoRequest Payer { get; set; }

        /// <summary>
        /// Связанный документооборот
        /// </summary>
        public RelatedDocumentRequest RelatedDocument { get; set; }

        /// <summary>
        /// Дополнительная информация
        /// </summary>
        public AdditionalInfoRequest AdditionalInfo { get; set; }
    }
}