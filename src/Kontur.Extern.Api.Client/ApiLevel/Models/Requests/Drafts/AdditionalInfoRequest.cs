using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Drafts.Enums;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class AdditionalInfoRequest
    {
        /// <summary>
        /// Тема письма
        /// </summary>
        public string? Subject { get; set; }

        /// <summary>
        /// Сертификаты, используемые для подписания
        /// </summary>
        public string[]? AdditionalCertificates { get; set; }

        /// <summary>
        /// Тип исходящего письма в ПФР
        /// </summary>
        public PfrLetterType PfrLetterType { get; set; }
    }
}