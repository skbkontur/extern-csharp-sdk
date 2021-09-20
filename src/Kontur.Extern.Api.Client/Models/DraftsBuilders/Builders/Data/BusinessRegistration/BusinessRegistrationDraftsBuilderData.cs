using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data.BusinessRegistration
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class BusinessRegistrationDraftsBuilderData : DraftsBuilderData
    {
        [JsonConstructor]
        public BusinessRegistrationDraftsBuilderData(
            RegistrationInfo registrationInfo, 
            PaperDocumentsDeliveryType? paperDocumentsDeliveryType,
            string[]? additionalCertificates)
        {
            RegistrationInfo = registrationInfo ?? throw new ArgumentNullException(nameof(registrationInfo));
            PaperDocumentsDeliveryType = paperDocumentsDeliveryType;
            AdditionalCertificates = additionalCertificates;
        }

        /// <summary>
        /// Сведения для регистрации бизнеса
        /// </summary>
        public RegistrationInfo RegistrationInfo { get; }

        /// <summary>
        /// Признак наличия запроса о предоставлении документов в письменном (бумажном) виде.
        /// </summary>
        public PaperDocumentsDeliveryType? PaperDocumentsDeliveryType { get; }
        
        /// <summary>
        /// Список сертификатов подписантов, когда заявление подано от нескольких заявителей (для ЮЛ)
        /// </summary>
        public string[]? AdditionalCertificates { get; }
    }
}