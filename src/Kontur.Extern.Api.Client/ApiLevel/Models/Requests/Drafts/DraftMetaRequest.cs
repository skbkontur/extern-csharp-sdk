using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Model.Drafts;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DraftMetaRequest
    {
        /// <summary>
        /// Отправитель
        /// </summary>
        //[Required]
        public SenderRequest Sender { get; set; } = null!;

        /// <summary>
        /// Получатель
        /// </summary>
        //[Required]
        public RecipientInfoRequest Recipient { get; set; } = null!;

        /// <summary>
        /// Налогоплательщик. Организация, за которую отправляется отчет
        /// </summary>
        //[Required]
        public AccountInfoRequest Payer { get; set; } = null!;

        /// <summary>
        /// Связанный документооборот
        /// </summary>
        public RelatedDocument? RelatedDocument { get; set; }

        /// <summary>
        /// Дополнительная информация
        /// </summary>
        public AdditionalInfoRequest? AdditionalInfo { get; set; }

        /// <summary>
        /// Дополнительные опции создания черновика
        /// </summary>
        public DraftCreateOptionsRequest? DraftOptions { get; set; }
    }
}