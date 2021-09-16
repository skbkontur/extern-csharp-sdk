﻿using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Drafts.Meta;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Enums;

namespace Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DraftsBuilderMeta : IDraftsBuilderMeta<DraftsBuilderData>
    {
        /// <summary>
        /// Отправитель
        /// </summary>
        //[Required]
        public Sender Sender { get; set; } = null!;

        /// <summary>
        /// Налогоплательщик
        /// </summary>
        //[Required]
        public AccountInfo Payer { get; set; } = null!;

        /// <summary>
        /// Получатель
        /// </summary>
        //[Required]
        public RecipientInfo Recipient { get; set; } = null!;

        /// <summary>
        /// Тип DraftsBuilder
        /// </summary>
        //[Required]
        public DraftBuilderType BuilderType { get; set; }

        /// <summary>
        /// Данные, специфичные для указанного типа DraftsBuilder
        /// </summary>
        public DraftsBuilderData BuilderData { get; set; } = null!;
    }
}