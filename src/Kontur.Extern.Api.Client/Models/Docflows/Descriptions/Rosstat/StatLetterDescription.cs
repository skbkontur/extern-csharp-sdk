using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Rosstat
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class StatLetterDescription : DocflowDescription
    {
        /// <summary>
        /// Код ТОГС, куда направляется письмо
        /// </summary>
        public TogsCode Recipient { get; set; } = null!;
        
        /// <summary>
        /// Тема письма
        /// </summary>
        public string Subject { get; set; } = null!;
        
        /// <summary>
        /// Идентификатор связанного документооборота, на который отправлен ответ
        /// </summary>
        public Guid? RelatedDocflowId { get; set; }
        
        /// <summary>
        ///  Код ОКПО
        /// </summary>
        public Okpo Okpo { get; set; } = null!;
    }
}