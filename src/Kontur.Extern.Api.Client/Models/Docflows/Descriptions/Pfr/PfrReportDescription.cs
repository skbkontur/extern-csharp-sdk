﻿using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Common.Time;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Pfr
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class PfrReportDescription : DocflowDescription
    {
        /// <summary>
        /// Регистрационный номер
        /// </summary>
        public string RegistrationNumber { get; set; } = null!;

        /// <summary>
        /// Код УПФР, куда отправляется отчет
        /// </summary>
        public string FinalRecipient { get; set; } = null!;

        /// <summary>
        /// Версия формы документа
        /// </summary>
        public FormVersion FormVersion { get; set; } = null!;

        /// <summary>
        /// Дата начала отчетного периода, за который сдается документ
        /// </summary>
        public DateOnly? PeriodBegin { get; set; }
        
        /// <summary>
        /// Дата конца отчетного периода, за который сдается документ
        /// </summary>
        public DateOnly? PeriodEnd { get; set; }
    }
}