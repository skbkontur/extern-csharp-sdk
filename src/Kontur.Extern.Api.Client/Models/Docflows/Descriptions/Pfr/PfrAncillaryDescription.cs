using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Pfr
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class PfrAncillaryDescription : DocflowDescription
    {
        /// <summary>
        /// Регистрационный номер
        /// </summary>
        public string RegistrationNumber { get; set; } = null!;
        
        /// <summary>
        /// Код УПФР, куда отправляется отчет
        /// </summary>
        public string FinalRecipient { get; set; } = null!;
        
        /// <summary>
        /// Версия формы документа
        /// </summary>
        public FormVersion FormVersion { get; set; } = null!;
    }
}