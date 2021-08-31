#nullable enable
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.Common.Time;

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Documents.Requisites
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DemandAttachmentRequisites : DocflowDocumentRequisites
    {
        /// <summary>
        /// Номер требования
        /// </summary>
        //[Required]
        public string DemandNumber { get; set; }

        /// <summary>
        /// КНД распознанного поручения
        /// </summary>
        public string? DemandKnd { get; set; }

        /// <summary>
        /// Дата из требования
        /// </summary>
        public DateOnly? DemandDate { get; set; }

        /// <summary>
        /// Дедлайн отправки квитанции на требование
        /// </summary>
        public DateOnly? ReceiptDeadlineDate { get; set; }

        /// <summary>
        /// Дедлайн ответа на требование
        /// </summary>
        public DateOnly? ReplyDeadlineDate { get; set; }

        /// <summary>
        /// Список ИНН, содержащихся в требовании
        /// </summary>
        public string[] DemandInnList { get; set; }
    }
}