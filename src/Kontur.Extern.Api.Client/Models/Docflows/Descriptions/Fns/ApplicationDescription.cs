using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fns
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class ApplicationDescription : DocflowDescription
    {
        /// <summary>
        /// Версия формы документа
        /// </summary>
        public FormVersion FormVersion { get; set; } = null!;
        
        /// <summary>
        /// ИНН-КПП организации или ИНН индивидуального предпринимателя, за которых сдается заявление
        /// </summary>
        public string PayerInn { get; set; } = null!;
        
        /// <summary>
        /// Код инспекции, куда направляется документ
        /// </summary>
        public string Recipient { get; set; } = null!;
        
        /// <summary>
        /// Код конечной инспекции, куда направляется документ (в случае пересылки отчета через МРИ)
        /// </summary>
        public string FinalRecipient { get; set; } = null!;
        
        /// <summary>
        /// Номер заявления
        /// </summary>
        public string DocumentNumber { get; set; } = null!;
        
        /// <summary>
        /// ИНН из заявления
        /// </summary>
        public string ReportInn { get; set; } = null!;
        
        /// <summary>
        /// КПП из заявления
        /// </summary>
        public Kpp ReportKpp { get; set; } = null!;
        
        /// <summary>
        /// ОКТМО
        /// </summary>
        public string Oktmo { get; set; } = null!;
    }
}