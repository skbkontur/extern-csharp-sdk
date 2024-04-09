using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Common.Time;
using Kontur.Extern.Api.Client.Models.Common.Enums;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fns
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DemandDescription : DocflowDescription
    {
        /// <summary>
        /// Количество приложений
        /// </summary>
        public int AttachmentsCount { get; set; }

        /// <summary>
        /// Версия формы документа
        /// </summary>
        public FormVersion[] FormVersions { get; set; } = null!;

        /// <summary>
        /// ИНН-КПП организации или ИНН индивидуального предпринимателя, на которые пришло требование
        /// </summary>
        public string PayerInn { get; set; } = null!;

        /// <summary>
        /// Код инспекции, от которой пришло требование
        /// </summary>
        public string Cu { get; set; } = null!;

        /// <summary>
        /// Код инспекции, через которую прошло требование (в случае пересылки требования через МРИ)
        /// </summary>
        public string? TransitCu { get; set; }

        /// <summary>
        /// Имя файла отчета, к которому относится требование (в случае направления требования на декларацию)
        /// </summary>
        public string? SentOnReportFilename { get; set; }

        /// <summary>
        /// Статус квитанции
        /// </summary>
        public ReceiptStatus? ReceiptStatus { get; set; }

        /// <summary>
        /// Дата окончания срока отправки квитанции
        /// </summary>
        public DateOnly? ReceiptDeadlineDate { get; set; }

        /// <summary>
        /// Дата отправки квитанции
        /// </summary>
        public DateOnly? ReceiptDate { get; set; }

        /// <summary>
        /// Дата окончания срока ответа на требование
        /// </summary>
        public DateOnly? ReplyDeadlineDate { get; set; }

        /// <summary>
        /// Статус отправленного ответа на требование
        /// </summary>
        public DemandRepliesStatus? RepliesStatus { get; set; }
    }
}