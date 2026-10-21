using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Common.Enums;

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
        public PfrLetterType? PfrLetterType { get; set; }

        /// <summary>
        /// Идентификатор машиночитаемой доверенности
        /// </summary>
        public Guid? MachineReadableWarrantId { get; set; }
        
        /// <summary>
        /// Комментарий
        /// </summary>
        public DraftComment? Comment { get; set; }
    }
    
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DraftComment
    {
        /// <summary>
        /// Текст комментария
        /// </summary>
        public string Text { get; set; } = null!;

        /// <summary>
        /// Цвет комментария для веб-интерфейса Контур.Экстерна
        /// </summary>
        public DraftCommentColor Color { get; set; }
    }
    
    /// <summary>
    /// https://git.skbkontur.ru/keweb/drafts/-/blob/master/src/drafts.shared/Dtos.cs?ref_type=heads#L135
    /// </summary>
    [PublicAPI]
    public enum DraftCommentColor
    {
        Gray,
        Red,
        Yellow,
        Green,
        Blue,
        Violet
    }
}