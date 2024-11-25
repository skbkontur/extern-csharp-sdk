using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Enums;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.DraftBuilders.Builders
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DraftsBuilderMetaRequest
    {
        public DraftsBuilderMetaRequest(
            SenderRequest sender,
            AccountInfoRequest payer,
            RecipientInfoRequest recipient,
            DraftBuilderType builderType,
            DraftsBuilderData? builderData,
            DraftCreateOptionsRequest? draftOptions = null)
        {
            Sender = sender ?? throw new ArgumentNullException(nameof(sender));
            Payer = payer ?? throw new ArgumentNullException(nameof(payer));
            Recipient = recipient ?? throw new ArgumentNullException(nameof(recipient));
            BuilderType = builderType;
            BuilderData = builderData;
            DraftOptions = draftOptions;
        }
        
        /// <summary>
        /// Информация об отправителе
        /// </summary>
        public SenderRequest Sender { get; }

        /// <summary>
        /// Информация о налогоплательщике
        /// </summary>
        public AccountInfoRequest Payer { get; }

        /// <summary>
        /// Информация о получателе, контролирующий орган
        /// </summary>
        public RecipientInfoRequest Recipient { get; }

        /// <summary>
        /// Тип DraftsBuilder. Нужно передавать полностью, например, urn:drafts-builder:fns534-inventory.
        /// Возможные варианты описаны в [документации](https://docs-ke.readthedocs.io/ru/latest/specification/%D1%82%D0%B8%D0%BF%D1%8B%20DraftsBuilder.html).
        /// </summary>
        public DraftBuilderType BuilderType { get; }

        /// <summary>
        /// Данные для указанного типа DraftsBuilder
        /// </summary>
        public DraftsBuilderData? BuilderData { get; }

        /// <summary>
        /// Дополнительные опции создания черновика
        /// </summary>
        public DraftCreateOptionsRequest? DraftOptions { get; set; }

        public DraftsBuilderMetaRequest ChangeBuilderType(DraftBuilderType builderType, DraftsBuilderData? data) => 
            new(Sender, Payer, Recipient, builderType, data, DraftOptions);
    }
}