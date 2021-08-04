using JetBrains.Annotations;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.Model.Documents
{
    public partial struct DocumentType
    {
        /// <summary>
        /// ФСС
        /// </summary>
        [PublicAPI]
        public static class Fss
        {
            /// <summary>
            /// Расчёт 4-ФСС
            /// </summary>
            [PublicAPI]
            public static class Calculation4Fss
            {
                /// <summary>
                /// Исходный отчет с присоединенной подписью
                /// Имя (согласно нормативным документам): Файл Расчета
                /// </summary>
                public static readonly DocumentType FssReport = "urn:document:fss-report";

                /// <summary>
                /// Подтверждение спецоператора (не утверждено форматом, формирует Контур.Экстерн при отправке из веб-приложения, юридической силы не имеет)
                /// Имя (согласно нормативным документам): нет соответствия
                /// </summary>
                public static readonly DocumentType FssReportDateConfirmation = "urn:document:fss-report-date-confirmation";

                /// <summary>
                /// Сообщение об ошибке при проверке отчета, возможные типы указаны на Портале ФСС (ошибки на стадии криптографических проверок)
                /// Имя (согласно нормативным документам): нет соответствия
                /// </summary>
                public static readonly DocumentType FssReportError = "urn:document:fss-report-error";

                /// <summary>
                /// Сообщение об ошибке при проверке отчета (ошибки форматно-логического контроля)
                /// Имя (согласно нормативным документам): Квитанция о получении Расчета с ошибками
                /// </summary>
                public static readonly DocumentType FssReportErrorReceipt = "urn:document:fss-report-error-receipt";

                /// <summary>
                /// Квитанция
                /// Имя (согласно нормативным документам): Квитанция о получении Расчета
                /// </summary>
                public static readonly DocumentType FssReportReceipt = "urn:document:fss-report-receipt";
            }

            /// <summary>
            /// Подтверждение основного вида экономической деятельности
            /// </summary>
            [PublicAPI]
            public static class Confirmation
            {
                /// <summary>
                /// xml-файл отчета (не зашифрован и не сжат)
                /// </summary>
                public static readonly DocumentType Request = "urn:document:oved-confirmation-request";

                /// <summary>
                /// SOAP-сообщение с отчетом
                /// </summary>
                public static readonly DocumentType RequestSoapMessage = "urn:document:oved-confirmation-request-soap-message";

                /// <summary>
                /// SOAP-сообщение с запросом на обновление статуса отправленного отчета
                /// </summary>
                public static readonly DocumentType ProcessingResultRequestSoapMessage = "urn:document:oved-confirmation-processing-result-request-soap-message";

                /// <summary>
                /// Результат приема
                /// </summary>
                public static readonly DocumentType RequestResult = "urn:document:oved-confirmation-request-result";

                /// <summary>
                /// Результат обработки
                /// </summary>
                public static readonly DocumentType ProcessingResult = "urn:document:oved-confirmation-processing-result";
            }

            /// <summary>
            /// Подписка оператора на документооборот с ФСС по абоненту
            /// </summary>
            [PublicAPI]
            public static class ProviderSubscription
            {
                /// <summary>
                /// Подписка оператора на страхователя для получения документов из СЭДО
                /// </summary>
                public static readonly DocumentType SubscribeRequestForRegistrationNumber = "urn:document:fss-sedo-provider-subscription-subscribe-request-for-registration-number";
                /// <summary>
                /// Отписка оператора на страхователя для прекращения получения документов из СЭДО
                /// </summary>
                public static readonly DocumentType UnsubscribeRequestForRegistrationNumber = "urn:document:fss-sedo-provider-subscription-unsubscribe-request-for-registration-number";
                /// <summary>
                /// Результат приёма запроса порталом
                /// </summary>
                public static readonly DocumentType Receipt = "urn:document:fss-sedo-provider-subscription-receipt";
                /// <summary>
                /// Результат подписки оператора на страхователя
                /// </summary>
                public static readonly DocumentType SubscribeResult = "urn:document:fss-sedo-provider-subscription-subscribe-result";
                /// <summary>
                /// Ошибка обработки документа (асинхронная ошибка, приходит в urn:docflow:fss-sedo-error)
                /// </summary>
                public static readonly DocumentType ErrorMessage = "urn:document:fss-sedo-provider-subscription-error-message";
                /// <summary>
                /// Ошибка взаимодействия с СЭДО (синхронная ошибка обработки запроса в СЭДО)
                /// </summary>
                public static readonly DocumentType ExchangeError = "urn:document:fss-sedo-provider-subscription-exchange-error";
            }

            /// <summary>
            /// Подписка страхователя на документооборот с ФСС
            /// </summary>
            [PublicAPI]
            public static class AbonSubscription
            {
                /// <summary>
                /// Подписка работника(-ов) страхователя на оповещение по изменению статуса ЭЛН
                /// </summary>
                public static readonly DocumentType SubscribeRequestForSnils = "urn:document:fss-sedo-abonent-subscription-subscribe-request-for-snils";
                /// <summary>
                /// Подписка страхователя по регистрационному номеру для получения документов из СЭДО
                /// </summary>
                public static readonly DocumentType SubscribeRequestForRegistrationNumber = "urn:document:fss-sedo-abonent-subscription-subscribe-request-for-registration-number";
                /// <summary>
                /// Отписка работника(-ов) страхователя на оповещение по изменению статуса ЭЛН
                /// </summary>
                public static readonly DocumentType UnsubscribeRequestForSnils = "urn:document:fss-sedo-abonent-subscription-unsubscribe-request-for-snils";
                /// <summary>
                /// Отписка страхователя для прекращения получения документов из СЭДО
                /// </summary>
                public static readonly DocumentType UnsubscribeRequestForRegistrationNumber = "urn:document:fss-sedo-abonent-subscription-unsubscribe-request-for-registration-number";
                /// <summary>
                /// Результат приёма запроса порталом
                /// </summary>
                public static readonly DocumentType SubscriptionReceipt = "urn:document:fss-sedo-abonent-subscription-receipt";
                /// <summary>
                /// Результат подписки/отписки страхователя на оповещение об ЭЛН работника
                /// </summary>
                public static readonly DocumentType SubscribeResultForSnils = "urn:document:fss-sedo-abonent-subscription-subscribe-result-for-snils";
                /// <summary>
                /// Результат подписки страхователя по регистрационному номеру
                /// </summary>
                public static readonly DocumentType SubscribeResultForRegistrationNumber = "urn:document:fss-sedo-abonent-subscription-subscribe-result-for-registration-number";
                /// <summary>
                /// Ошибка обработки документа (асинхронная ошибка, приходит в urn:docflow:fss-sedo-error)
                /// </summary>
                public static readonly DocumentType ErrorMessage = "urn:document:fss-sedo-abonent-subscription-error-message";
                /// <summary>
                /// Ошибка взаимодействия с СЭДО (синхронная ошибка обработки запроса в СЭДО)
                /// </summary>
                public static readonly DocumentType ExchangeError = "urn:document:fss-sedo-abonent-subscription-exchange-error";
            }

            /// <summary>
            /// Результат приема подписки страхователя
            /// </summary>
            [PublicAPI]
            public static class AbonSubscriptionResult
            {
                /// <summary>
                /// Запрос на получение результата подписки абонента
                /// </summary>
                public static readonly DocumentType ResultStatusRequestMessage = "urn:document:fss-sedo-abonent-subscription-result-status-request-message";
                /// <summary>
                /// Результат подписки/отписки страхователя на оповещение об ЭЛН работника
                /// </summary>
                public static readonly DocumentType ResultSubscribeStatusForSnils = "urn:document:fss-sedo-abonent-subscription-result-subscribe-status-for-snils";
                /// <summary>
                /// Результат подписки/отписки работника(-ов) страхователя на уведомления по ЭЛН
                /// </summary>
                public static readonly DocumentType ResultSubscribeStatusForRegistrationNumber = "urn:document:fss-sedo-abonent-subscription-result-subscribe-status-for-registration-number";
                /// <summary>
                /// Ошибка обработки документа (асинхронная ошибка, приходит в urn:docflow:fss-sedo-error)
                /// </summary>
                public static readonly DocumentType ResultErrorMessage = "urn:document:fss-sedo-abonent-subscription-result-error-message";
                /// <summary>
                /// Ошибка взаимодействия с СЭДО (синхронная ошибка обработки запроса в СЭДО)
                /// </summary>
                public static readonly DocumentType ResultExchangeError = "urn:document:fss-sedo-abonent-subscription-result-exchange-error";
            }

            /// <summary>
            /// Извещение о прямых выплатах мер социального обеспечения
            /// </summary>
            [PublicAPI]
            public static class PvsoNotification
            {
                /// <summary>
                /// Запрос на получение контента извещения ПВСО
                /// </summary>
                public static readonly DocumentType RequestMessage = "urn:document:fss-sedo-pvso-notification-request-message";
                /// <summary>
                /// Извещение ПВСО
                /// </summary>
                public static readonly DocumentType NotificationMessage = "urn:document:fss-sedo-pvso-notification-notification-message";
                /// <summary>
                /// Подтверждение прочтения сообщения страхователем
                /// </summary>
                public static readonly DocumentType ReceiptNotificationMessage = "urn:document:fss-sedo-pvso-notification-receipt-notification-message";
                /// <summary>
                /// Результат подтверждения прочтения сообщения от СЭДО
                /// </summary>
                public static readonly DocumentType ReceiptNotificationReceiptMessage = "urn:document:fss-sedo-pvso-notification-receipt-notification-receipt-message";
                /// <summary>
                /// Ошибка обработки документа (асинхронная ошибка, приходит в urn:docflow:fss-sedo-error)
                /// </summary>
                public static readonly DocumentType ErrorMessage = "urn:document:fss-sedo-pvso-notification-error-message";
                /// <summary>
                /// Ошибка взаимодействия с СЭДО (синхронная ошибка обработки запроса в СЭДО)
                /// </summary>
                public static readonly DocumentType ExchangeError = "urn:document:fss-sedo-pvso-notification-exchange-error";
            }

            /// <summary>
            /// Уведомления об изменении статуса электронного больничного листа
            /// </summary>
            [PublicAPI]
            public static class SickReportChangeNotification
            {
                /// <summary>
                /// Запрос на получение контента уведомления об изменении статуса ЭЛН
                /// </summary>
                public static readonly DocumentType RequestMessage = "urn:document:fss-sedo-sick-report-change-notification-request-message";
                /// <summary>
                /// Уведомление об изменении статуса ЭЛН
                /// </summary>
                public static readonly DocumentType NotificationMessage = "urn:document:fss-sedo-sick-report-change-notification-notification-message";
                /// <summary>
                /// Подтверждение прочтения сообщения страхователем
                /// </summary>
                public static readonly DocumentType ReceiptNotificationMessage = "urn:document:fss-sedo-sick-report-change-notification-receipt-notification-message";
                /// <summary>
                /// Результат подтверждения прочтения сообщения от СЭДО
                /// </summary>
                public static readonly DocumentType ReceiptNotificationReceiptMessage = "urn:document:fss-sedo-sick-report-change-notification-receipt-notification-receipt-message";
                /// <summary>
                /// Ошибка обработки документа (асинхронная ошибка, приходит в urn:docflow:fss-sedo-error)
                /// </summary>
                public static readonly DocumentType ErrorMessage = "urn:document:fss-sedo-sick-report-change-notification-error-message";
                /// <summary>
                /// Ошибка взаимодействия с СЭДО (синхронная ошибка обработки запроса в СЭДО)
                /// </summary>
                public static readonly DocumentType ExchangeError = "urn:document:fss-sedo-sick-report-change-notification-exchange-error";
            }

            /// <summary>
            /// Сообщения об ошибках в ДО ФСС через СЭДО
            /// </summary>
            [PublicAPI]
            public static class SedoErrors
            {
                /// <summary>
                /// Запрос на получение сообщения об ошибке
                /// </summary>
                public static readonly DocumentType RequestMessage = "urn:document:fss-sedo-error-request-message";
                /// <summary>
                /// Ошибка взаимодействия с СЭДО (синхронная ошибка обработки запроса в СЭДО)
                /// </summary>
                public static readonly DocumentType ExchangeError = "urn:document:fss-sedo-error-exchange-error";
            }
        }
    }
}