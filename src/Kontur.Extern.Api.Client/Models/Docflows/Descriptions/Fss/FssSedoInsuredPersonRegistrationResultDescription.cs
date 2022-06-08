using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss
{
    [PublicAPI]
    public class FssSedoInsuredPersonRegistrationResultDescription : FssSedoResultDescription
    {
        /// <summary>
        /// СНИЛС
        /// </summary>
        public string? Snils { get; set; }
        
        /// <summary>
        /// Версия формы документа
        /// </summary>
        public FormVersion? FormVersion { get; set; }
    }
}