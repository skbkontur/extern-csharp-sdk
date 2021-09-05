using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Models.Docflows.Descriptions.Pfr
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class PfrIosDescription : DocflowDescription
    {
        /// <summary>
        /// Регистрационный номер
        /// </summary>
        public string RegistrationNumber { get; set; }
        /// <summary>
        /// Код УПФР, куда отправляется запрос
        /// </summary>
        public string FinalRecipient { get; set; }
        /// <summary>
        /// Тип отправляемого запроса
        /// </summary>
        public string FormType { get; set; }
        /// <summary>
        /// Версия формы документа
        /// </summary>
        public FormVersion FormVersion { get; set; }
    }
}