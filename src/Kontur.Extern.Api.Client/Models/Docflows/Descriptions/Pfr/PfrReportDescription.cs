using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Common.Time;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Models.Docflows.Enums;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Pfr
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class PfrReportDescription : DocflowDescription
    {
        /// <summary>
        /// Регистрационный номер
        /// </summary>
        public PfrRegNumber RegistrationNumber { get; set; } = null!;

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

        /// <summary>
        /// Дополнительный статус документооборота
        /// </summary>
        public DocflowExtendedStatus ExtendedStatus { get; set; }
    }
}