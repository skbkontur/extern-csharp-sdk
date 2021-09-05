using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.Common.Time;

namespace Kontur.Extern.Client.Models.Docflows.Descriptions.Fns
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class ReportDescription : DocflowDescription
    {
        /// <summary>
        /// Версия формы документа
        /// </summary>
        public FormVersion FormVersion { get; set; }
        
        /// <summary>
        /// Код инспекции, куда направляется документ
        /// </summary>
        public string Recipient { get; set; }
        
        /// <summary>
        /// Код конечной инспекции, куда направляется документ (в случае пересылки отчета через МРИ)
        /// </summary>
        public string FinalRecipient { get; set; }
        
        /// <summary>
        /// Номер корректировки
        /// </summary>
        public int CorrectionNumber { get; set; }
        
        /// <summary>
        /// Дата начала отчетного периода, за который сдается документ
        /// </summary>
        public DateOnly PeriodBegin { get; set; }
        
        /// <summary>
        /// Дата окончания отчетного периода, за который сдается документ
        /// </summary>
        public DateOnly PeriodEnd { get; set; }
        
        /// <summary>
        /// Код отчетного периода
        /// </summary>
        public int PeriodCode { get; set; }
        
        /// <summary>
        /// ИНН-КПП организации или ИНН индивидуального предпринимателя, за которых отправляется отчетность
        /// </summary>
        public string PayerInn { get; set; }
        
        /// <summary>
        /// ИНН из отчета
        /// </summary>
        public string ReportInn { get; set; }
        
        /// <summary>
        /// КПП из отчета
        /// </summary>
        public string ReportKpp { get; set; }
        
        /// <summary>
        /// Идентификатор связанного документооборота, в ответ на который отправляется отчет
        /// </summary>
        public Guid? RelatedDocflowId { get; set; }
        
        /// <summary>
        /// Идентификатор документа в связанном документообороте (требование, письмо или отчет)
        /// </summary>
        public Guid? RelatedDocumentId { get; set; }
        
        /// <summary>
        /// ОКТМО
        /// </summary>
        public string Oktmo { get; set; }
        
        /// <summary>
        /// Код формы реорганизации
        /// </summary>
        public string ReorganizationForm { get; set; }
        
        /// <summary>
        /// ИНН реорганизованной организации из отчета
        /// </summary>
        public string ReorganizationInn { get; set; }
        
        /// <summary>
        /// КПП реорганизованной организации из отчета
        /// </summary>
        public string ReorganizationKpp { get; set; }
    }
}