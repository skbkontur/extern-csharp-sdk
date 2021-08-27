using System.Collections.Generic;
using Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Info;
// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Builders.Data
{
    public class BusinessRegistrationDraftsBuilderData : DraftsBuilderData
    {
        /// <summary>
        /// Сведения для регистрации бизнеса
        /// </summary>
        // [JsonProperty(Required = Required.Always)]
        public RegistrationInfo RegistrationInfo { get; set; }

        /// <summary>
        /// Список сертификатов подписантов, когда заявление подано от нескольких заявителей (для ЮЛ)
        /// </summary>
        public IReadOnlyCollection<byte[]> AdditionalCertificates { get; set; }
    }
}