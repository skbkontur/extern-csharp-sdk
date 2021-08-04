// ReSharper disable CommentTypo
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Model.Documents
{
    public partial struct DocumentType
    {
        /// <summary>
        /// ПФР
        /// </summary>
        [PublicAPI]
        public static class Pfr
        {
            /// <summary>
            /// Сведения ПФР
            /// </summary>
            [PublicAPI]
            public static class PfrReport
            {
                /// <summary>
                /// Пачка или пачки отчетности (установленный формат)	
                /// Имя (согласно нормативным документам): Тип пачки
                /// </summary>
                public static readonly DocumentType Report = "urn:document:pfr-report";

                /// <summary>
                /// Описание передаваемых сведений (установленный формат)	
                /// Имя (согласно нормативным документам): ОписаниеСведений
                /// </summary>
                public static readonly DocumentType Description = "urn:document:pfr-report-description";

                /// <summary>
                /// Неформализованное приложение к сведениям	
                /// Имя (согласно нормативным документам): СведенияПриложение
                /// </summary>
                public static readonly DocumentType Attachment = "urn:document:pfr-report-attachment";

                /// <summary>
                /// Уведомление о доставке сведений в орган ПФР (установленный формат)	
                /// Имя (согласно нормативным документам): ПодтверждениеПолучения
                /// </summary>
                public static readonly DocumentType Acknowledgement = "urn:document:pfr-report-acknowledgement";

                /// <summary>
                /// Протокол входного контроля, содержит информацию о том, приняты отправленные сведения или нет (установленный формат)	
                /// Имя (согласно нормативным документам): Протокол
                /// </summary>
                public static readonly DocumentType Protocol = "urn:document:pfr-report-protocol";

                /// <summary>
                /// Приложение к протоколу (обычно это результаты проверки проверочных программ ПФР)	
                /// Имя (согласно нормативным документам): ПротоколПриложение
                /// </summary>
                public static readonly DocumentType ProtocolAppendix = "urn:document:pfr-report-protocol-appendix";

                /// <summary>
                /// Описание ошибки, возникшей в ходе ДО	
                /// Имя (согласно нормативным документам): описаниеОшибки
                /// </summary>
                public static readonly DocumentType ErrorDescription = "urn:document:pfr-report-error-description";
            }

            /// <summary>
            /// Сведения ПФР (для ЭДОК)
            /// </summary>
            [PublicAPI]
            public static class PfrReportV2
            {
                /// <summary>
                /// Отчет с подписью формата XMLDSig для отправки в ЭДОК	
                /// Имя (согласно нормативным документам): Отчетность
                /// </summary>
                public static readonly DocumentType Report = "urn:document:pfr-report-report-v2";

                /// <summary>
                /// Неформализованное приложение к пакету	
                /// Имя (согласно нормативным документам): Приложение
                /// </summary>
                public static readonly DocumentType Attachment = PfrReport.Attachment;

                /// <summary>
                /// Подтверждение доставки пакета в ЭДОК	
                /// Имя (согласно нормативным документам): Уведомление о доставке
                /// </summary>
                public static readonly DocumentType AcknowledgementV2 = "urn:document:pfr-report-acknowledgement-v2";

                /// <summary>
                /// Отказ в приеме пакета по технологическим причинам	
                /// Имя (согласно нормативным документам): Уведомление об отказе в приеме пакета
                /// </summary>
                public static readonly DocumentType DeclineNotice = "urn:document:pfr-report-decline-notice";

                /// <summary>
                /// Результат успешно пройденных проверок	
                /// Имя (согласно нормативным документам): Унифицированный протокол проверок
                /// </summary>
                public static readonly DocumentType ProtocolCheck = "urn:document:pfr-report-protocol-check";

                /// <summary>
                /// Результат выявленных ошибок при проверке отчетности	
                /// Имя (согласно нормативным документам): Уведомление об устранении ошибок и (или) несоответствий
                /// </summary>
                public static readonly DocumentType ProtocolRefinementNotice = "urn:document:pfr-report-protocol-refinement-notice";

                /// <summary>
                /// Приложение к протоколу (к Positive либо Suppositive)	
                /// Имя (согласно нормативным документам): Приложение
                /// </summary>
                public static readonly DocumentType ProtocolAppendix = PfrReport.ProtocolAppendix;

                /// <summary>
                /// Подтверждение получения уведомления об устранении ошибок и (или) несоответствий оператором	
                /// Имя (согласно нормативным документам): Уведомление о доставке
                /// </summary>
                public static readonly DocumentType ProtocolRefinementNoticeReceipt = "urn:document:pfr-report-protocol-refinement-notice-receipt";
            }

            /// <summary>
            /// Письмо в ПФР
            /// </summary>
            [PublicAPI]
            public static class PfrLetter
            {
                /// <summary>
                /// Текст письма	
                /// Имя (согласно нормативным документам): Письмо
                /// </summary>
                public static readonly DocumentType Letter = "urn:document:pfr-letter";
                /// <summary>
                /// Описание передаваемого письма (установленный формат)	
                /// Имя (согласно нормативным документам): ОписаниеПисьма
                /// </summary>
                public static readonly DocumentType Description = "urn:document:pfr-letter-description";
                /// <summary>
                /// Произвольное приложение к письму	
                /// Имя (согласно нормативным документам): ПисьмоПриложение
                /// </summary>
                public static readonly DocumentType Attachment = "urn:document:pfr-letter-attachment";
                /// <summary>
                /// Транспортная информация о передаваемом письме (установленный формат)	
                /// Имя (согласно нормативным документам): ТранспортнаяИнформация
                /// </summary>
                public static readonly DocumentType TransportInfo = "urn:document:pfr-letter-transport-info";
                /// <summary>
                /// Документ, подтверждающий получение письма и приложений к нему УПФР	
                /// Имя (согласно нормативным документам): ПодтверждениеПолучения
                /// </summary>
                public static readonly DocumentType LetterAcknowledgement = "urn:document:pfr-letter-letter-acknowledgement";
                /// <summary>
                /// Описание ошибки, возникшей в ходе ДО	
                /// Имя (согласно нормативным документам): описаниеОшибки
                /// </summary>
                public static readonly DocumentType ErrorDescription = "urn:document:pfr-letter-error-description";
            }

            /// <summary>
            /// Письмо из ПФР
            /// </summary>
            [PublicAPI]
            public static class CuLetter
            {
                /// <summary>
                /// Текст письма	
                /// Имя (согласно нормативным документам): Письмо
                /// </summary>
                public static readonly DocumentType Letter = "urn:document:pfr-cu-letter";
                /// <summary>
                /// Описание передаваемого письма (установленный формат)	
                /// Имя (согласно нормативным документам): ОписаниеПисьма
                /// </summary>
                public static readonly DocumentType Description = "urn:document:pfr-cu-letter-description";
                /// <summary>
                /// Произвольное приложение к письму	
                /// Имя (согласно нормативным документам): ПисьмоПриложение
                /// </summary>
                public static readonly DocumentType Attachment = "urn:document:pfr-cu-letter-attachment";
                /// <summary>
                /// Транспортная информация о передаваемом письме (установленный формат)	
                /// Имя (согласно нормативным документам): ТранспортнаяИнформация
                /// </summary>
                public static readonly DocumentType TransportInfo = "urn:document:pfr-cu-letter-transport-info";
                /// <summary>
                /// Документ, подтверждающий получение письма и приложений к нему абонентом	
                /// Имя (согласно нормативным документам): ПодтверждениеПолучения
                /// </summary>
                public static readonly DocumentType LetterAcknowledgement = "urn:document:pfr-cu-letter-letter-acknowledgement";
                /// <summary>
                /// Описание ошибки, возникшей в ходе ДО	
                /// Имя (согласно нормативным документам): описаниеОшибки
                /// </summary>
                public static readonly DocumentType ErrorDescription = "urn:document:pfr-cu-letter-error-description";
            }

            /// <summary>
            /// Уточнение платежей
            /// </summary>
            [PublicAPI]
            public static class Ios
            {
                /// <summary>
                /// Документ с запросом информации по платежам в орган ПФР (установленный формат)	
                /// Имя (согласно нормативным документам): Запрос
                /// </summary>
                public static readonly DocumentType Request = "urn:document:pfr-ios-request";
                /// <summary>
                /// Описание передаваемого запроса (установленный формат)	
                /// Имя (согласно нормативным документам): ОписаниеЗапроса
                /// </summary>
                public static readonly DocumentType Description = "urn:document:pfr-ios-description";
                /// <summary>
                /// Подтвержение получения запроса УПФР	
                /// Имя (согласно нормативным документам): ПодтверждениеПолучения
                /// </summary>
                public static readonly DocumentType RequestAcknowledgement = "urn:document:pfr-ios-request-acknowledgement";
                /// <summary>
                /// Ответ на запрос информации по платежам в орган ПФР, в котором указано, удалось предоставить запрашиваемую информацию или нет	
                /// Имя (согласно нормативным документам): Ответ
                /// </summary>
                public static readonly DocumentType Response = "urn:document:pfr-ios-response";
                /// <summary>
                /// Приложение к документу ответ: если ответ положительный, то в нем содержится информация о платежах в орган ПФР; если ответ отрицательный, то в нем содержится описание причины, по которой не удалось предоставить запрашиваемую информацию	
                /// Имя (согласно нормативным документам): ОтветПриложение
                /// </summary>
                public static readonly DocumentType ResponseAttachment = "urn:document:pfr-ios-response-attachment";
                /// <summary>
                /// Описание ошибки, возникшей в ходе ДО	
                /// Имя (согласно нормативным документам): описаниеОшибки
                /// </summary>
                public static readonly DocumentType ErrorDescription = "urn:document:pfr-ios-error-description";
            }

            /// <summary>
            /// Служебный документ ПФР
            /// </summary>
            [PublicAPI]
            public static class AncillaryDocument
            {
                /// <summary>
                /// Служебный документ	
                /// Имя (согласно нормативным документам): Служебный документ
                /// </summary>
                public static readonly DocumentType Document = "urn:document:pfr-ancillary-document";
                /// <summary>
                /// Не формализованное приложение в пакете	
                /// Имя (согласно нормативным документам): Приложение
                /// </summary>
                public static readonly DocumentType Attachment = "urn:document:pfr-ancillary-attachment";
                /// <summary>
                /// Подтверждение доставки пакета в ЭДОК	
                /// Имя (согласно нормативным документам): Уведомление о доставке
                /// </summary>
                public static readonly DocumentType DeliveryNotice = "urn:document:pfr-ancillary-delivery-notice";
                /// <summary>
                /// Отказ в приеме пакета	
                /// Имя (согласно нормативным документам): Уведомление об отказе в приеме пакета
                /// </summary>
                public static readonly DocumentType RejectionNotice = "urn:document:pfr-ancillary-rejection-notice";
                /// <summary>
                /// Положительный результат рассмотрения	
                /// Имя (согласно нормативным документам): Уведомление о результате рассмотрения
                /// </summary>
                public static readonly DocumentType ReceptionResultPositive = "urn:document:pfr-ancillary-reception-result-positive";
                /// <summary>
                /// Результат рассмотрения с перечнем выявленных ошибок в предоставленном документе	
                /// Имя (согласно нормативным документам): Уведомление о результате рассмотрения
                /// </summary>
                public static readonly DocumentType ReceptionResultNegative = "urn:document:pfr-ancillary-reception-result-negative";
                /// <summary>
                /// Подтверждение получения результата рассмотрения оператором	
                /// Имя (согласно нормативным документам): Уведомление о доставке
                /// </summary>
                public static readonly DocumentType ReceptionResultReceipt = "urn:document:pfr-ancillary-reception-result-receipt";
            }
        }
    }
}