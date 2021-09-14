using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fns
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class CuLetterDescription : DocflowDescription
    {
        /// <summary>
        /// Код инспекции, откуда пришло письмо
        /// </summary>
        public string Cu { get; set; }
        
        /// <summary>
        /// Тема письма
        /// </summary>
        public string Subject { get; set; }
        
        /// <summary>
        /// ИНН-КПП организации или ИНН индивидуального предпринимателя, которым направлен документ
        /// </summary>
        public string RecipientInn { get; set; }
        
        /// <summary>
        ///  Идентификатор связанного документооборота, к которому направлено письмо
        /// </summary>
        public Guid? RelatedDocflowId { get; set; }
        
        /// <summary>
        /// Идентификатор документа в связанном документообороте, к которому направлено письмо
        /// </summary>
        public Guid? RelatedDocumentId { get; set; }
    }
}