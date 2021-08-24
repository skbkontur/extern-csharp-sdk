
// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions
{
    public class PfrAncillaryDescription : DocflowDescription
    {
        /// <summary>
        /// Поле устарело и больше не используется. Код УПФР, куда отправляется отчет
        /// </summary>
        public string Cu { get; set; }
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
    }
}