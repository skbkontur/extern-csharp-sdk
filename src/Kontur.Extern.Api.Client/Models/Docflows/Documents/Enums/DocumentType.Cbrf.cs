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
                public static readonly DocumentType ReportV2 = "urn:document:cbrf-report-report-v2";

                /// <summary>
                /// Результат приема отчета
                /// </summary>
                public static readonly DocumentType EsodReceipt = "urn:document:cbrf-report-esod-receipt";

                /// <summary>
                /// Технологический документ
                /// </summary>
                public static readonly DocumentType StatusInfo = "urn:document:cbrf-report-status-info";

                /// <summary>
                /// Результат обработки отчета
                /// </summary>
                public static readonly DocumentType ProcessingStatus = "urn:document:cbrf-report-processing-status";

                /// <summary>
                /// Протокол с перечнем ошибок, возникшими при проверке отчета
                /// </summary>
                public static readonly DocumentType ProcessingErrorProtocol = "urn:document:cbrf-report-processing-error-protocol";
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
                public static readonly DocumentType Report = "urn:document:cbrf-report";

                /// <summary>
                /// Сообщение о приеме
                /// </summary>
                public static readonly DocumentType ReceptionResult = "urn:document:cbrf-report-reception-result";

                /// <summary>
                /// Результат обработки
                /// </summary>
                public static readonly DocumentType ProcessingResult = "urn:document:cbrf-report-processing-result";
            }
        }
    }
}