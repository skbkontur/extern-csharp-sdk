using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Info
{
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