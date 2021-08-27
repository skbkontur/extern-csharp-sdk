using System.Collections.Generic;
// ReSharper disable CommentTypo

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
        // [JsonProperty(Required = Required.Always)]
        public IReadOnlyCollection<ApplicantInfo> ApplicantInfos { get; set; }

        /// <summary>
        /// Информация об ИП
        /// </summary>
        public IpInfo IpInfo { get; set; }

        /// <summary>
        /// Информация о ЮЛ
        /// </summary>
        public UlInfo UlInfo { get; set; }
    }
}