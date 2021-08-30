using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions.Pfr
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class PfrReportDescription : DocflowDescription
    {
        /// <summary>
        /// Регистрационный номер
        /// </summary>
        public string RegistrationNumber { get; set; }
        
        /// <summary>
        /// Код УПФР, куда отправляется отчет
        /// </summary>
        public string FinalRecipient { get; set; }
        
        /// <summary>
        /// Версия формы документа
        /// </summary>
        public FormVersion FormVersion { get; set; }
        
        /// <summary>
        /// Дата начала отчетного периода, за который сдается документ
        /// </summary>
        public DateTime? PeriodBegin { get; set; }
        
        /// <summary>
        /// Дата конца отчетного периода, за который сдается документ
        /// </summary>
        public DateTime? PeriodEnd { get; set; }
    }
}