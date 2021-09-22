using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Models.Drafts.Meta;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Enums;

namespace Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DraftsBuilderMeta : IDraftsBuilderMeta<DraftsBuilderData>
    {
        public DraftsBuilderMeta(
            DraftBuilderType builderType, 
            DraftsBuilderData? builderData,
            Sender sender,
            AccountInfo payer,
            RecipientInfo recipient)
        {
            if (builderType.IsEmpty)
                throw Errors.ValueShouldNotBeEmpty(nameof(builderType));
            
            BuilderType = builderType;
            BuilderData = builderData;
            Sender = sender ?? throw Errors.JsonDoesNotContainProperty(nameof(sender));
            Payer = payer ?? throw Errors.JsonDoesNotContainProperty(nameof(payer));
            Recipient = recipient ?? throw Errors.JsonDoesNotContainProperty(nameof(recipient));
        }
        
        /// <summary>
        /// Отправитель
        /// </summary>
        public Sender Sender { get; }

        /// <summary>
        /// Налогоплательщик
        /// </summary>
        public AccountInfo Payer { get; }

        /// <summary>
        /// Получатель
        /// </summary>
        public RecipientInfo Recipient { get; }

        /// <summary>
        /// Тип DraftsBuilder
        /// </summary>
        public DraftBuilderType BuilderType { get; }

        /// <summary>
        /// Данные, специфичные для указанного типа DraftsBuilder
        /// </summary>
        public DraftsBuilderData? BuilderData { get; }
    }
}