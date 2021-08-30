using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions.Pfr
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class PfrLetterDescription : DocflowDescription
    {
        /// <summary>
        /// Регистрационный номер
        /// </summary>
        public string RegistrationNumber { get; set; }
        
        /// <summary>
        /// Код УПФР, куда отправляется письмо
        /// </summary>
        public string FinalRecipient { get; set; }
        
        /// <summary>
        /// Тема письма
        /// </summary>
        public string Subject { get; set; }
        
        /// <summary>
        /// Тип отправленного письма
        /// </summary>
        public string FormType { get; set; }
        
        /// <summary>
        /// Идентификатор связанного документооборота, в ответ на который отправлено письмо
        /// </summary>
        public Guid? RelatedDocflowId { get; set; }
    }
}