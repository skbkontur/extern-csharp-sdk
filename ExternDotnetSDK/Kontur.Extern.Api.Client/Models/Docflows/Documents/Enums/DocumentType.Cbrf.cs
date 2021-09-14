// ReSharper disable CommentTypo
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Documents.Enums
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
                public static readonly DocumentType CbrfReportV2 = "urn:document:cbrf-report-v2";

                /// <summary>
                /// Результат приема отчета
                /// </summary>
                public static readonly DocumentType CbrfReportEsodReceipt = "urn:document:cbrf-report-esod-receipt";

                /// <summary>
                /// Технологический документ
                /// </summary>
                public static readonly DocumentType CbrfReportStatusInfo = "urn:document:cbrf-report-status-info";

                /// <summary>
                /// Результат обработки отчета
                /// </summary>
                public static readonly DocumentType CbrfReportProcessingStatus = "urn:document:cbrf-report-processing-status";

                /// <summary>
                /// Протокол с перечнем ошибок, возникшими при проверке отчета
                /// </summary>
                public static readonly DocumentType CbrfReportProcessingErrorProtocol = "urn:document:cbrf-report-processing-error-protocol";
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
                public static readonly DocumentType CbrfReport = "urn:document:cbrf-report";

                /// <summary>
                /// Сообщение о приеме
                /// </summary>
                public static readonly DocumentType CbrfReportReceptionResult = "urn:document:cbrf-report-reception-result";

                /// <summary>
                /// Результат обработки
                /// </summary>
                public static readonly DocumentType CbrfReportProcessingResult = "urn:document:cbrf-report-processing-result";
            }
        }
    }
}