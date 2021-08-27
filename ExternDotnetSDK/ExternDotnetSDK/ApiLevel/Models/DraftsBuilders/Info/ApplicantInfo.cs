using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Info
{
    public class ApplicantInfo
    {
        /// <summary>
        /// ИНН
        /// </summary>
        // [JsonProperty(Required = Required.Always)]
        public string Inn { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        // [JsonProperty(Required = Required.Always)]
        public Fio Fio { get; set; }

        /// <summary>
        /// E-mail
        /// </summary>
        public string Email { get; set; }
    }
}