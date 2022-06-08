using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Common;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss
{
    [PublicAPI]
    public class FssSedoProactivePaymentsDemandDescription : FssSedoDescription
    {
        /// <summary>
        /// Номер процесса социальной поддержки
        /// </summary>
        public string? SocialAssistNumber { get; set; }

        /// <summary>
        /// СНИЛС
        /// </summary>
        public string? Snils { get; set; }

        /// <summary>
        /// Номер ЭЛН
        /// </summary>
        public string? SickListNumber { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        [JsonPropertyName("fio")]
        public PersonFullName PersonFullName { get; set; } = null!;
        
        /// <summary>
        /// Версия формы документа
        /// </summary>
        public FormVersion? FormVersion { get; set; }
    }
}