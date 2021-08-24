using System;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions
{
    public class CbrfReportDescription : DocflowDescription
    {
        /// <summary>
        /// Версия формы документа
        /// </summary>
        public FormVersion FormVersion { get; set; }
        /// <summary>
        /// ОГРН
        /// </summary>
        public string Ogrn { get; set; }
        /// <summary>
        /// Дата начала отчетного периода, за который сдается отчет
        /// </summary>
        public DateTime? PeriodBegin { get; set; }
        /// <summary>
        /// Дата окончания отчетного периода, за который сдается отчет
        /// </summary>
        public DateTime? PeriodEnd { get; set; }
    }
}