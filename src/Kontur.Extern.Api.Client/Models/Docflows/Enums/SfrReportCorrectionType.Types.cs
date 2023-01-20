namespace Kontur.Extern.Api.Client.Models.Docflows.Enums
{
    partial struct SfrReportCorrectionType
    {
        /// <summary>
        /// Исходная
        /// </summary>
        public static readonly SfrReportCorrectionType Initial = "urn:sfr-report-correction-type:initial";

        /// <summary>
        /// Корректирующая
        /// </summary>
        public static readonly SfrReportCorrectionType Corrective = "urn:sfr-report-correction-type:corrective";

        /// <summary>
        /// Отменяющая
        /// </summary>
        public static readonly SfrReportCorrectionType Abrogative = "urn:sfr-report-correction-type:abrogative";

        /// <summary>
        /// Дополняющая
        /// </summary>
        public static readonly SfrReportCorrectionType Additional = "urn:sfr-report-correction-type:additional";

        /// <summary>
        /// Назначение пенсии
        /// </summary>
        public static readonly SfrReportCorrectionType PensionAssignment = "urn:sfr-report-correction-type:pension-assignment";

        /// <summary>
        /// Особый
        /// </summary>
        public static readonly SfrReportCorrectionType Special = "urn:sfr-report-correction-type:special";
    }
}