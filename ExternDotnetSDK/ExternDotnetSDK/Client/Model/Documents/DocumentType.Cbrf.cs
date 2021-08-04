// ReSharper disable CommentTypo
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Model.Documents
{
    public partial struct DocumentType
    {
        /// <summary>
        /// ЦБ РФ
        /// </summary>
        [PublicAPI]
        public static class Cbrf
        {
            /// <summary>
            /// Отчетность в ЦБ РФ (документообороты после 01.01.2020)
            /// </summary>
            [PublicAPI]
            public static class ReportAfter2020
            {
                /// <summary>
                /// Отчет
                /// </summary>
                public static DocumentType CbrfReportV2 = "urn:document:cbrf-report-v2";

                /// <summary>
                /// Результат приема отчета
                /// </summary>
                public static DocumentType cbrfReportEsodReceipt = "urn:document:cbrf-report-esod-receipt";

                /// <summary>
                /// Технологический документ
                /// </summary>
                public static DocumentType cbrfReportStatusInfo = "urn:document:cbrf-report-status-info";

                /// <summary>
                /// Результат обработки отчета
                /// </summary>
                public static DocumentType cbrfReportProcessingStatus = "urn:document:cbrf-report-processing-status";

                /// <summary>
                /// Протокол с перечнем ошибок, возникшими при проверке отчета
                /// </summary>
                public static DocumentType cbrfReportProcessingErrorProtocol = "urn:document:cbrf-report-processing-error-protocol";
            }

            /// <summary>
            /// Отчетность в ЦБ РФ (документообороты до 01.01.2020)
            /// </summary>
            [PublicAPI]
            public static class ReportBefore2020
            {
                /// <summary>
                /// Отчет
                /// </summary>
                public static DocumentType CbrfReport = "urn:document:cbrf-report";

                /// <summary>
                /// Сообщение о приеме
                /// </summary>
                public static DocumentType CbrfReportReceptionResult = "urn:document:cbrf-report-reception-result";

                /// <summary>
                /// Результат обработки
                /// </summary>
                public static DocumentType CbrfReportProcessingResult = "urn:document:cbrf-report-processing-result";
            }
        }
    }
}