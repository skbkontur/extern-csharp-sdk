using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Common.Time;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Rosstat
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class StatReportDescription : DocflowDescription
    {
        /// <summary>
        /// Версия формы документа
        /// </summary>
        public FormVersion FormVersion { get; set; } = null!;

        /// <summary>
        /// Код ТОГС, куда направляется отчет
        /// </summary>
        public TogsCode Recipient { get; set; } = null!;

        /// <summary>
        /// Код ОКПО
        /// </summary>
        public Okpo Okpo { get; set; } = null!;

        /// <summary>
        /// Код по ОКУД
        /// </summary>
        public Okud Okud { get; set; } = null!;

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
        public string PeriodCode { get; set; } = null!;

        /// <summary>
        /// Номер корректировки
        /// </summary>
        public int CorrectionNumber { get; set; }
    }
}