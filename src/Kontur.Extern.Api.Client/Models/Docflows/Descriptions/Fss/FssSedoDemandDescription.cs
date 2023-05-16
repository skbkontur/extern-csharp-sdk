using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Common.Time;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss
{
    [PublicAPI]
    public class FssSedoDemandDescription: FssSedoDescription
    {
        /// <summary>
        /// ИНН организации, на которую пришло требование
        /// </summary>
        public string? PayerInn { get; set; }

        /// <summary>
        /// Версия формы документа
        /// </summary>
        public FormVersion? FormVersion { get; set; }

        /// <summary>
        /// Дедлайн отправки квитанции на требование ФСС
        /// </summary>
        public DateOnly? ReceiptDeadlineDate { get; set; }

        /// <summary>
        /// Дата отправки квитанции на требование ФСС
        /// </summary>
        public DateOnly? ReceiptDate { get; set; }

        /// <summary>
        /// Крайний срок ответа на требование
        /// </summary>
        public DateOnly? ReplyDeadlineDate { get; set; }
    }
}