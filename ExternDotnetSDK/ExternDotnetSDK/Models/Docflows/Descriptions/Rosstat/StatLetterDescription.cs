using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Rosstat
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class StatLetterDescription : DocflowDescription
    {
        /// <summary>
        /// Код ТОГС, куда направляется письмо
        /// </summary>
        public string Recipient { get; set; }
        /// <summary>
        /// Тема письма
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Идентификатор связанного документооборота, на который отправлен ответ
        /// </summary>
        public Guid? RelatedDocflowId { get; set; }
        /// <summary>
        ///  Код ОКПО
        /// </summary>
        public string Okpo { get; set; }
    }
}