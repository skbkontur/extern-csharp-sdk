using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Info
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class ApplicantInfo
    {
        /// <summary>
        /// ИНН
        /// </summary>
        [Required]
        [DataMember]
        public string Inn { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        [Required]
        [DataMember]
        public Fio Fio { get; set; }

        /// <summary>
        /// E-mail
        /// </summary>
        [DataMember]
        public string Email { get; set; }
    }
}