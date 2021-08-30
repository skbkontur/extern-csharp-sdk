using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions.Pfr
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class PfrCuLetterDescription : DocflowDescription
    {
        /// <summary>
        ///  Код УПФР, откуда пришло письмо
        /// </summary>
        public string Cu { get; set; }
        
        /// <summary>
        /// Регистрационный номер
        /// </summary>
        public string RegistrationNumber { get; set; }
        
        /// <summary>
        /// Тема письма
        /// </summary>
        public string Subject { get; set; }
        
        /// <summary>
        /// Тип полученного письма
        /// </summary>
        public string FormType { get; set; }
        
        /// <summary>
        /// Идентификатор связанного документооборота, к которому направлено письмо
        /// </summary>
        public Guid? RelatedDocflowId { get; set; }
    }
}