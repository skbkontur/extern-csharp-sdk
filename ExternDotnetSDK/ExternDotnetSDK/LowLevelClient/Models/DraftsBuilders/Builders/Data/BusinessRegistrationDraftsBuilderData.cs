using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Kontur.Extern.Client.Models.DraftsBuilders.Info;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.DraftsBuilders.Builders.Data
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class BusinessRegistrationDraftsBuilderData : DraftsBuilderData
    {
        /// <summary>
        /// Сведения для регистрации бизнеса
        /// </summary>
        [Required]
        [DataMember]
        public RegistrationInfo RegistrationInfo { get; set; }

        /// <summary>
        /// Список сертификатов подписантов, когда заявление подано от нескольких заявителей (для ЮЛ)
        /// </summary>
        public IReadOnlyCollection<byte[]> AdditionalCertificates { get; set; }
    }
}