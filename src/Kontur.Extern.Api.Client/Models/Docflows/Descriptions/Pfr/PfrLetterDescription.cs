using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Pfr
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class PfrLetterDescription : DocflowDescription
    {
        /// <summary>
        /// Регистрационный номер
        /// </summary>
        public PfrRegNumber RegistrationNumber { get; set; } = null!;
        
        /// <summary>
        /// Код УПФР, куда отправляется письмо
        /// </summary>
        public string FinalRecipient { get; set; } = null!;
        
        /// <summary>
        /// Тема письма
        /// </summary>
        public string Subject { get; set; } = null!;
        
        /// <summary>
        /// Тип отправленного письма
        /// </summary>
        public string FormType { get; set; } = null!;
        
        /// <summary>
        /// Идентификатор связанного документооборота, в ответ на который отправлено письмо
        /// </summary>
        public Guid? RelatedDocflowId { get; set; }
    }
}