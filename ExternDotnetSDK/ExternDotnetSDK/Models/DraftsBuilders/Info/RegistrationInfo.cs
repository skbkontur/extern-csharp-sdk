using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.DraftsBuilders.Info
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class RegistrationInfo
    {
        /// <summary>
        /// Код заявления по справочнику СФРД
        /// </summary>
        private ApplicationCode ApplicationCode { get; set; }

        /// <summary>
        /// Информация о заявителе
        /// </summary>
        [Required]
        [DataMember]
        public IReadOnlyCollection<ApplicantInfo> ApplicantInfos { get; set; }

        /// <summary>
        /// Информация об ИП
        /// </summary>
        [DataMember]
        public IpInfo IpInfo { get; set; }

        /// <summary>
        /// Информация о ЮЛ
        /// </summary>
        [DataMember]
        public UlInfo UlInfo { get; set; }
    }
}