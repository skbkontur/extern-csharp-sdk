using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fns
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class ApplicationDescription : DocflowDescription
    {
        /// <summary>
        /// Версия формы документа
        /// </summary>
        public FormVersion FormVersion { get; set; }
        
        /// <summary>
        /// ИНН-КПП организации или ИНН индивидуального предпринимателя, за которых сдается заявление
        /// </summary>
        public string PayerInn { get; set; }
        
        /// <summary>
        /// Код инспекции, куда направляется документ
        /// </summary>
        public string Recipient { get; set; }
        
        /// <summary>
        /// Код конечной инспекции, куда направляется документ (в случае пересылки отчета через МРИ)
        /// </summary>
        public string FinalRecipient { get; set; }
        
        /// <summary>
        /// Номер заявления
        /// </summary>
        public string DocumentNumber { get; set; }
        
        /// <summary>
        /// ИНН из заявления
        /// </summary>
        public string ReportInn { get; set; }
        
        /// <summary>
        /// КПП из заявления
        /// </summary>
        public string ReportKpp { get; set; }
        
        /// <summary>
        /// ОКТМО
        /// </summary>
        public string Oktmo { get; set; }
    }
}