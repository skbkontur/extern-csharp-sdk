using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss
{
    [PublicAPI]
    public class FssSedoSickReportChangeNotificationDescription : FssSedoDescription
    {
        /// <summary>
        /// СНИЛС
        /// </summary>
        public string? Snils { get; set; }

        /// <summary>
        /// Номер ЭЛН
        /// </summary>
        public string? SickListNumber { get; set; }

        /// <summary>
        /// Статус ЭЛН
        /// </summary>
        public string? SickListStatus { get; set; }
    }
}