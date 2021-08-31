using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Meta
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class AdditionalInfo
    {
        /// <summary>
        /// Тема письма
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Сертификаты, используемые для подписания
        /// </summary>
        public string[] AdditionalCertificates { get; set; }

        /// <summary>
        /// DraftsBuilderId, из которого был создан черновик
        /// </summary>
        public Guid? OriginalDraftsBuilderId { get; set; }

        /// <summary>
        /// Тип исходящего письма в ПФР
        /// </summary>
        public Urn PfrLetterType { get; set; }
    }
}