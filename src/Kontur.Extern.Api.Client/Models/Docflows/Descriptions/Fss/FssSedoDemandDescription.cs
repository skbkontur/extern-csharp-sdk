using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss
{
    [PublicAPI]
    public class FssSedoDemandDescription: FssSedoDescription
    {
        /// <summary>
        /// ИНН организации, на которую пришло требование
        /// </summary>
        public string? PayerInn { get; set; }

        /// <summary>
        /// Версия формы документа
        /// </summary>
        public FormVersion? FormVersion { get; set; }
    }
}