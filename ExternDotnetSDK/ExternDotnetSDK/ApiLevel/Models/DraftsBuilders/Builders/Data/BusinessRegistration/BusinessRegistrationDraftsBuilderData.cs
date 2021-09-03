using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Builders.Data.BusinessRegistration
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class BusinessRegistrationDraftsBuilderData : DraftsBuilderData
    {
        /// <summary>
        /// Сведения для регистрации бизнеса
        /// </summary>
        //[Required]
        public RegistrationInfo RegistrationInfo { get; set; }
        
        /// <summary>
        /// Признак наличия запроса о предоставлении документов в письменном (бумажном) виде.
        /// </summary>
        public PaperDocumentsDeliveryType? PaperDocumentsDeliveryType { get; set; }
        
        /// <summary>
        /// Список сертификатов подписантов, когда заявление подано от нескольких заявителей (для ЮЛ)
        /// </summary>
        public string[] AdditionalCertificates { get; set; }
    }
}