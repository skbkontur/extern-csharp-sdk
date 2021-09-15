using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fns
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class LetterDescription : DocflowDescription
    {
        /// <summary>
        /// Код конечной инспекции, куда направляется документ (в случае пересылки отчета через МРИ)
        /// </summary>
        public string FinalRecipient { get; set; } = null!;
        
        /// <summary>
        /// Код инспекции, куда направляется документ
        /// </summary>
        public string Recipient { get; set; } = null!;
        
        /// <summary>
        /// Тема письма
        /// </summary>
        public string Subject { get; set; } = null!;
        
        /// <summary>
        /// ИНН-КПП организации или ИНН индивидуального предпринимателя, от имени которых направляется письмо
        /// </summary>
        public string SenderInn { get; set; } = null!;
    
        /// <summary>
        /// Идентификатор связанного документооборота, к которому направляется письмо
        /// </summary>
        public Guid? RelatedDocflowId { get; set; }
        
        /// <summary>
        /// Идентификатор документа в связанном документообороте, к которому отправляется письмо
        /// </summary>
        public Guid? RelatedDocumentId { get; set; }
    }
}