using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Documents.Enums
{
    public partial struct DocumentType
    {
        /// <summary>
        /// Сведения ПФР
        /// </summary>
        [PublicAPI]
        public static class SfrReport
        {
            /// <summary>
            /// Отчет
            /// </summary>
            public static readonly DocumentType ReportDocument = "urn:document:sfr-report-report-document";

            /// <summary>
            /// Описание пакета
            /// Имя (согласно нормативным документам): ОСП
            /// </summary>
            public static readonly DocumentType PackageDescription = "urn:document:sfr-report-package-description";

            /// <summary>
            /// Приложение
            /// </summary>
            public static readonly DocumentType Attachment = "urn:document:sfr-report-attachment";

            /// <summary>
            /// Уведомление о получении
            /// Имя (согласно нормативным документам): УОД
            /// </summary>
            public static readonly DocumentType Acknowledgement = "urn:document:sfr-report-acknowledgement";

            /// <summary>
            /// Протокол проверки
            /// Имя (согласно нормативным документам): УППО
            /// </summary>
            public static readonly DocumentType ProtocolCheck = "urn:document:sfr-report-protocol-check";

            /// <summary>
            /// Уведомление об устранении ошибок и (или) несоответствий
            /// Имя (согласно нормативным документам): УУОН-ПУ
            /// </summary>
            public static readonly DocumentType ProtocolRefinementNotice = "urn:document:sfr-report-protocol-refinement-notice";

            /// <summary>
            /// Уведомление об отказе в приеме пакета
            /// Имя (согласно нормативным документам): УОПП
            /// </summary>
            public static readonly DocumentType DeclineNotice = "urn:document:sfr-report-decline-notice";

            /// <summary>
            /// Уведомление о невозможности доставки документа
            /// Имя (согласно нормативным документам): УОНД
            /// </summary>
            public static readonly DocumentType ImpossibleDeliveryNotice = "urn:document:sfr-report-impossible-delivery-notice";

            /// <summary>
            /// Уведомление о доставке: 1) уведомления об устранении ошибок и (или) несоответствий; 2) протокола проверок
            /// Имя (согласно нормативным документам): УОД
            /// </summary>
            public static readonly DocumentType ProtocolRefinementNoticeReceipt = "urn:document:sfr-report-protocol-refinement-notice-receipt";
        }
    }
}