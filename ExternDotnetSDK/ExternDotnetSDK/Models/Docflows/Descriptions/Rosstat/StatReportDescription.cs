using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.Common.Time;

namespace Kontur.Extern.Client.Models.Docflows.Descriptions.Rosstat
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class StatReportDescription : DocflowDescription
    {
        /// <summary>
        /// Версия формы документа
        /// </summary>
        public FormVersion FormVersion { get; set; }
        
        /// <summary>
        /// Код ТОГС, куда направляется отчет
        /// </summary>
        public string Recipient { get; set; }
        
        /// <summary>
        /// Код ОКПО
        /// </summary>
        public string Okpo { get; set; }
        
        /// <summary>
        /// Код по ОКУД
        /// </summary>
        public string Okud { get; set; }
        
        /// <summary> 
        /// Дата начала отчетного периода, за который сдается документ
        /// </summary>
        public DateOnly PeriodBegin { get; set; }
        
        /// <summary>
        /// Дата конца отчетного периода, за который сдается документ
        /// </summary>
        public DateOnly PeriodEnd { get; set; }
        
        /// <summary>
        /// Код отчетного периода
        /// </summary>
        public string PeriodCode { get; set; }
        
        /// <summary>
        /// Номер корректировки
        /// </summary>
        public int CorrectionNumber { get; set; }
    }
}