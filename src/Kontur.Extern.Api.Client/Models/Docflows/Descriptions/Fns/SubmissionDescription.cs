﻿using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Common.Time;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fns
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class SubmissionDescription : DocflowDescription
    {
        /// <summary>
        /// Версия формы документа
        /// </summary>
        public FormVersion FormVersion { get; set; } = null!;
        
        /// <summary>
        /// ИНН-КПП организации или ИНН индивидуального предпринимателя, за которых отправляется представление
        /// </summary>
        public string PayerInn { get; set; } = null!;
        
        /// <summary>
        /// Код инспекции, куда направляется документ
        /// </summary>
        public string Recipient { get; set; } = null!;
        
        /// <summary>
        /// Код конечной инспекции, куда направляется документ (в случае пересылки отчета через МРИ)
        /// </summary>
        public string? FinalRecipient { get; set; }
        
        /// <summary>
        /// ОГРН
        /// </summary>
        public string? Ogrn { get; set; }
        
        /// <summary>
        /// ИНН из представления
        /// </summary>
        public string? ReportInn { get; set; }
        
        /// <summary>
        ///  КПП из представления
        /// </summary>
        public Kpp? ReportKpp { get; set; }
        
        /// <summary>
        /// Дата начала отчетного периода, за который сдается документ
        /// </summary>
        public DateOnly PeriodBegin { get; set; }
        
        /// <summary>
        /// Дата конца отчетного периода, за который сдается документ
        /// </summary>
        public DateOnly PeriodEnd { get; set; }
        
        /// <summary>
        /// Идентификатор связанного документооборота, в ответ на который отправляется представление
        /// </summary>
        public Guid? RelatedDocflowId { get; set; }
        
        /// <summary>
        ///  Идентификатор документа в связанном документообороте, в ответ на который отправляется представление
        /// </summary>
        public Guid? RelatedDocumentId { get; set; }
        
        /// <summary>
        /// ОКТМО
        /// </summary>
        public string? Oktmo { get; set; }
    }
}