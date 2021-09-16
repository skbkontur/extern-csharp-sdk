﻿using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Common.Time;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class FssReportDescription : DocflowDescription
    {
        /// <summary>
        /// Версия формы документа
        /// </summary>
        public FormVersion FormVersion { get; set; } = null!;
        
        /// <summary>
        /// Регистрационный номер
        /// </summary>
        public string RegistrationNumber { get; set; } = null!;
        
        /// <summary>
        /// Номер корректировки
        /// </summary>
        public int CorrectionNumber { get; set; }
        
        /// <summary>
        /// Идентификатор отчета 4-ФСС на портале ФСС
        /// </summary>
        public string FssId { get; set; } = null!;
        
        /// <summary>
        /// Описание стадии обработки, на которой находится отчет
        /// </summary>
        public string FssStageDescription { get; set; } = null!;
        
        /// <summary>
        /// Код ошибки
        /// </summary>
        public string FssStageErrorCode { get; set; } = null!;
        
        /// <summary>
        /// Описание ошибки
        /// </summary>
        public string FssStageErrorExtend { get; set; } = null!;
        
        /// <summary>
        /// Текущая стадия обработки отчета
        /// </summary>
        public string FssStageType { get; set; } = null!;
        
        /// <summary>
        /// Статус текущей стадии обработки отчета
        /// </summary>
        public string FssStageStatus { get; set; } = null!;
        
        /// <summary>
        /// Дата перехода отчета в текущую стадию обработки
        /// </summary>
        public DateOnly? FssStageDate { get; set; }
        
        /// <summary>
        /// Дата начала отчетного периода, за который сдается отчет
        /// </summary>
        public DateOnly? PeriodBegin { get; set; }
        
        /// <summary>
        /// Дата окончания отчетного периода, за который сдается отчет
        /// </summary>
        public DateOnly? PeriodEnd { get; set; }
    }
}