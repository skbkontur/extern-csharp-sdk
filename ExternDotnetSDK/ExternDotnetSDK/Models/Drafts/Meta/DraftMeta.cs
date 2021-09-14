using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Drafts.Meta
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DraftMeta
    {
        /// <summary>
        /// Отправитель
        /// </summary>
       // [Required]
        public Sender Sender { get; set; }

        /// <summary>
        /// Получатель
        /// </summary>
        //[Required]
        public RecipientInfo Recipient { get; set; }

        /// <summary>
        /// Налогоплательщик. Организация, за которую отправляется отчет
        /// </summary>
        //[Required]
        public AccountInfo Payer { get; set; }

        /// <summary>
        /// Связанный документооборот
        /// </summary>
        public RelatedDocument RelatedDocument { get; set; }

        /// <summary>
        /// Дополнительная информация
        /// </summary>
        public AdditionalInfo AdditionalInfo { get; set; }
    }
}