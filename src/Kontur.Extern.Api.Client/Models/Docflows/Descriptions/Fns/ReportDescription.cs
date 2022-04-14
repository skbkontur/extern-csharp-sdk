using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Common.Time;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fns
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class ReportDescription : DocflowDescription
    {
        /// <summary>
        /// Версия формы документа
        /// </summary>
        public FormVersion FormVersion { get; set; } = null!;
        
        /// <summary>
        /// Код инспекции, куда направляется документ
        /// </summary>
        public string Recipient { get; set; } = null!;

        /// <summary>
        /// Код конечной инспекции, куда направляется документ (в случае пересылки отчета через МРИ)
        /// </summary>
        public string FinalRecipient { get; set; } = null!;
        
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
        public string PeriodCode { get; set; } = null!;
        
        /// <summary>
        /// ИНН-КПП организации или ИНН индивидуального предпринимателя, за которых отправляется отчетность
        /// </summary>
        public string PayerInn { get; set; } = null!;
        
        /// <summary>
        /// ИНН из отчета
        /// </summary>
        public string ReportInn { get; set; } = null!;
        
        /// <summary>
        /// КПП из отчета
        /// </summary>
        public Kpp ReportKpp { get; set; } = null!;
        
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
        public string? Oktmo { get; set; }
        
        /// <summary>
        /// Код формы реорганизации
        /// </summary>
        public string? ReorganizationForm { get; set; }
        
        /// <summary>
        /// ИНН реорганизованной организации из отчета
        /// </summary>
        public string? ReorganizationInn { get; set; }
        
        /// <summary>
        /// КПП реорганизованной организации из отчета
        /// </summary>
        public string? ReorganizationKpp { get; set; }
    }
}