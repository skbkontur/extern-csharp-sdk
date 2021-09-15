using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.DraftBulders.Builders
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DraftsBuilderMetaRequest
    {
        /// <summary>
        /// Информация об отправителе
        /// </summary>
        //[Required]
        public SenderRequest Sender { get; set; } = null!;

        /// <summary>
        /// Информация о налогоплательщике
        /// </summary>
        //[Required]
        public AccountInfoRequest Payer { get; set; } = null!;

        /// <summary>
        /// Информация о получателе, контролирующий орган
        /// </summary>
        //[Required]
        public RecipientInfoRequest Recipient { get; set; } = null!;

        /// <summary>
        /// Тип DraftsBuilder. Нужно передавать полностью, например, urn:drafts-builder:fns534-inventory.
        /// Возможные варианты описаны в [документации](https://docs-ke.readthedocs.io/ru/latest/specification/%D1%82%D0%B8%D0%BF%D1%8B%20DraftsBuilder.html).
        /// </summary>
        //[Required]
        public Urn BuilderType { get; set; } = null!;

        /// <summary>
        /// Данные для указанного типа DraftsBuilder
        /// </summary>
        public DraftsBuilderData BuilderData { get; set; } = null!;
    }
}