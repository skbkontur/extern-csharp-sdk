using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Common.Time;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Cbrf
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
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
        public DateOnly? PeriodBegin { get; set; }
        
        /// <summary>
        /// Дата окончания отчетного периода, за который сдается отчет
        /// </summary>
        public DateOnly? PeriodEnd { get; set; }
    }
}