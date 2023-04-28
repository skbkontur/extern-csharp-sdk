using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Common.Time;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class FssSedoPvsoNotificationDescription : FssSedoDescription
    {
        /// <summary>
        /// Время окончания актуальности уведомления
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Версия формы документа
        /// </summary>
        public FormVersion? FormVersion { get; set; }

        /// <summary>
        /// Крайний срок ответа
        /// </summary>
        public DateOnly? ReplyDeadlineDate { get; set; }
    }
}