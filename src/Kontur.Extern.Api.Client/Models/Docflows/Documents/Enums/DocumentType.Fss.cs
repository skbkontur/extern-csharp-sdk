using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.Models.Docflows.Documents.Enums
{
    public partial struct DocumentType
    {
        /// <summary>
        /// Расчёт 4-ФСС
        /// </summary>
        [PublicAPI]
        public static class FssReport
        {
            /// <summary>
            /// Исходный отчет с присоединенной подписью
            /// Имя (согласно нормативным документам): Файл Расчета
            /// </summary>
            public static readonly DocumentType Report = "urn:document:fss-report";

            /// <summary>
            /// Подтверждение спецоператора (не утверждено форматом, формирует Контур.Экстерн при отправке из веб-приложения, юридической силы не имеет)
            /// Имя (согласно нормативным документам): нет соответствия
            /// </summary>
            public static readonly DocumentType DateConfirmation = "urn:document:fss-report-date-confirmation";

            /// <summary>
            /// Сообщение об ошибке при проверке отчета, возможные типы указаны на Портале ФСС (ошибки на стадии криптографических проверок)
            /// Имя (согласно нормативным документам): нет соответствия
            /// </summary>
            public static readonly DocumentType Error = "urn:document:fss-report-error";

            /// <summary>
            /// Сообщение об ошибке при проверке отчета (ошибки форматно-логического контроля)
            /// Имя (согласно нормативным документам): Квитанция о получении Расчета с ошибками
            /// </summary>
            public static readonly DocumentType ErrorReceipt = "urn:document:fss-report-error-receipt";

            /// <summary>
            /// Квитанция
            /// Имя (согласно нормативным документам): Квитанция о получении Расчета
            /// </summary>
            public static readonly DocumentType Receipt = "urn:document:fss-report-receipt";
        }

        /// <summary>
        /// Подтверждение основного вида экономической деятельности
        /// </summary>
        [PublicAPI]
        public static class OvedConfirmation
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
        public static class FssSedoProviderSubscription
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
        public static class FssSedoAbonentSubscription
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
            public static readonly DocumentType Receipt = "urn:document:fss-sedo-abonent-subscription-receipt";
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
        public static class FssSedoAbonentSubscriptionResult
        {
            /// <summary>
            /// Запрос на получение результата подписки абонента
            /// </summary>
            public static readonly DocumentType StatusRequestMessage = "urn:document:fss-sedo-abonent-subscription-result-status-request-message";
            /// <summary>
            /// Результат подписки/отписки страхователя на оповещение об ЭЛН работника
            /// </summary>
            public static readonly DocumentType SubscribeStatusForSnils = "urn:document:fss-sedo-abonent-subscription-result-subscribe-status-for-snils";
            /// <summary>
            /// Результат подписки/отписки работника(-ов) страхователя на уведомления по ЭЛН
            /// </summary>
            public static readonly DocumentType SubscribeStatusForRegistrationNumber = "urn:document:fss-sedo-abonent-subscription-result-subscribe-status-for-registration-number";
            /// <summary>
            /// Ошибка обработки документа (асинхронная ошибка, приходит в urn:docflow:fss-sedo-error)
            /// </summary>
            [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
            public static readonly DocumentType ErrorMessage = "urn:document:fss-sedo-abonent-subscription-result-error-message";

            /// <summary>
            /// Ошибка взаимодействия с СЭДО (синхронная ошибка обработки запроса в СЭДО)
            /// </summary>
            [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
            public static readonly DocumentType ExchangeError = "urn:document:fss-sedo-abonent-subscription-result-exchange-error";
        }

        /// <summary>
        /// Извещение о прямых выплатах мер социального обеспечения
        /// </summary>
        [PublicAPI]
        public static class FssSedoPvsoNotification
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
            /// <summary>
            /// Квитанция о прочтении (используется в методе генерации ответного документа)
            /// </summary>
            public static readonly DocumentType ReadReceipt = "urn:document:fss-sedo-pvso-notification-receipt";
        }

        /// <summary>
        /// Уведомления об изменении статуса электронного больничного листа
        /// </summary>
        [PublicAPI]
        public static class FssSedoSickReportChangeNotification
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
            /// <summary>
            /// Квитанция о прочтении (используется в методе генерации ответного документа)
            /// </summary>
            public static readonly DocumentType ReadReceipt = "urn:document:fss-sedo-sick-report-change-notification-receipt";
        }

        /// <summary>
        /// Сообщения об ошибках в ДО ФСС через СЭДО
        /// </summary>
        [PublicAPI]
        public static class FssSedoError
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

        /// <summary>
        /// Запрос недостающих сведений для назначения пособия ФСС
        /// </summary>
        [PublicAPI]
        public static class FssSedoProactivePaymentsDemand
        {
            /// <summary>
            /// Запрос на получение документов
            /// </summary>
            public static readonly DocumentType RequestMessage = "urn:document:fss-sedo-proactive-payments-demand-request-message";
            /// <summary>
            /// Документ запроса
            /// </summary>
            public static readonly DocumentType DemandMessage = "urn:document:fss-sedo-proactive-payments-demand-demand-message";
            /// <summary>
            /// Ошибка обработки
            /// </summary>
            public static readonly DocumentType ErrorMessage = "urn:document:fss-sedo-proactive-payments-demand-error-message";
            /// <summary>
            /// Ошибка взаимодействия с СЭДО
            /// </summary>
            public static readonly DocumentType ExchangeError = "urn:document:fss-sedo-proactive-payments-demand-exchange-error";
            /// <summary>
            /// Квитанция о прочтении (используется в методе генерации ответного документа)
            /// </summary>
            public static readonly DocumentType ReadReceipt = "urn:document:fss-sedo-proactive-payments-demand-receipt";
        }

        /// <summary>
        /// Сведения для назначения пособия ФСС
        /// </summary>
        [PublicAPI]
        public static class FssSedoProactivePaymentsReply
        {
            /// <summary>
            /// Запрос на отправку сведений
            /// </summary>
            public static readonly DocumentType Request = "urn:document:fss-sedo-proactive-payments-reply-request";
            /// <summary>
            /// Результат приёма запроса порталом
            /// </summary>
            public static readonly DocumentType Receipt = "urn:document:fss-sedo-proactive-payments-reply-receipt";
            /// <summary>
            /// Результат обработки запроса
            /// </summary>
            public static readonly DocumentType ResponseResult = "urn:document:fss-sedo-proactive-payments-reply-response-result";
            /// <summary>
            /// Ошибка обработки
            /// </summary>
            public static readonly DocumentType ErrorMessage = "urn:document:fss-sedo-proactive-payments-reply-error-message";
            /// <summary>
            /// Ошибка взаимодействия с СЭДО
            /// </summary>
            public static readonly DocumentType ExchangeError = "urn:document:fss-sedo-proactive-payments-reply-exchange-error";
            /// <summary>
            /// Квитанция о прочтении (используется в методе генерации ответного документа)
            /// </summary>
            public static readonly DocumentType ReadReceipt = "urn:document:fss-sedo-proactive-payments-reply-read-receipt";
        }

        /// <summary>
        /// Результат обработки сведений для назначения пособия ФСС
        /// </summary>
        [PublicAPI]
        public static class FssSedoProactivePaymentsReplyResult
        {
            /// <summary>
            /// Запрос на получение документов
            /// </summary>
            public static readonly DocumentType StatusRequestMessage = "urn:document:fss-sedo-proactive-payments-reply-result-status-request-message";
            /// <summary>
            /// Результат обработки запроса
            /// </summary>
            public static readonly DocumentType ResponseResult = "urn:document:fss-sedo-proactive-payments-reply-result-response-result";
            /// <summary>
            /// Ошибка обработки
            /// </summary>
            public static readonly DocumentType ErrorMessage = "urn:document:fss-sedo-proactive-payments-reply-result-error-message";
            /// <summary>
            /// Ошибка взаимодействия с СЭДО
            /// </summary>
            public static readonly DocumentType ExchangeError = "urn:document:fss-sedo-proactive-payments-reply-result-exchange-error";
        }

        /// <summary>
        /// Выплата пособия ФСС
        /// </summary>
        [PublicAPI]
        public static class FssSedoProactivePaymentsBenefit
        {
            /// <summary>
            /// Запрос на получение документов
            /// </summary>
            public static readonly DocumentType RequestMessage = "urn:document:fss-sedo-proactive-payments-benefit-request-message";
            /// <summary>
            /// Документ с выплатами
            /// </summary>
            public static readonly DocumentType BenefitMessage = "urn:document:fss-sedo-proactive-payments-benefit-benefit-message";
            /// <summary>
            /// Ошибка обработки
            /// </summary>
            public static readonly DocumentType ErrorMessage = "urn:document:fss-sedo-proactive-payments-benefit-error-message";
            /// <summary>
            /// Ошибка взаимодействия с СЭДО
            /// </summary>
            public static readonly DocumentType ExchangeError = "urn:document:fss-sedo-proactive-payments-benefit-exchange-error";
            /// <summary>
            /// Квитанция о прочтении (используется в методе генерации ответного документа)
            /// </summary>
            public static readonly DocumentType ReadReceipt = "urn:document:fss-sedo-proactive-payments-benefit-receipt";
        }

        /// <summary>
        /// Инициация выплат пособия ФСС
        /// </summary>
        [PublicAPI]
        public static class FssSedoBenefitPaymentInitiation
        {
            /// <summary>
            /// Запрос на отправку сообщения "Инициация выплат пособия"
            /// </summary>
            public static readonly DocumentType Request = "urn:document:fss-sedo-benefit-payment-initiation-request";
            /// <summary>
            /// Результат отправки соощения на портал
            /// </summary>
            public static readonly DocumentType ReceptionResult = "urn:document:fss-sedo-benefit-payment-initiation-reception-result";
            /// <summary>
            /// Сообщение "Результат обработки сообщения инициации выплат ФСС"
            /// </summary>
            public static readonly DocumentType ResultDocument = "urn:document:fss-sedo-benefit-payment-initiation-result-document";
            /// <summary>
            /// Ошибка обработки
            /// </summary>
            public static readonly DocumentType ErrorMessage = "urn:document:fss-sedo-benefit-payment-initiation-error-message";
            /// <summary>
            /// Ошибка взаимодействия с порталом СЭДО
            /// </summary>
            public static readonly DocumentType ExchangeError = "urn:document:fss-sedo-benefit-payment-initiation-exchange-error";
            /// <summary>
            /// Квитанция о прочтении (используется в методе генерации ответного документа)
            /// </summary>
            public static readonly DocumentType ReadReceipt = "urn:document:fss-sedo-benefit-payment-initiation-read-receipt";
        }

        /// <summary>
        /// Результат инициации выплат пособия ФСС
        /// </summary>
        [PublicAPI]
        public static class FssSedoBenefitPaymentInitiationResult
        {
            /// <summary>
            /// Запрос на получение "Результата обработки сообщения инициации выплат ФСС"
            /// </summary>
            public static readonly DocumentType RequestMessage = "urn:document:fss-sedo-benefit-payment-initiation-result-request-message";
            /// <summary>
            /// Сообщение "Результат обработки сообщения инициации выплат ФСС"
            /// </summary>
            public static readonly DocumentType StatusDocument = "urn:document:fss-sedo-benefit-payment-initiation-result-status-document";
            /// <summary>
            /// Ошибка обработки
            /// </summary>
            public static readonly DocumentType ErrorMessage = "urn:document:fss-sedo-benefit-payment-initiation-result-error-message";
            /// <summary>
            /// Ошибка взаимодействия с порталом СЭДО
            /// </summary>
            public static readonly DocumentType ExchangeError = "urn:document:fss-sedo-benefit-payment-initiation-result-exchange-error";
        }

        /// <summary>
        /// Регистрация сведений о застрахованном лице ФСС
        /// </summary>
        [PublicAPI]
        public static class FssSedoInsuredPersonRegistration
        {
            /// <summary>
            /// Запрос на регистрацию сведений
            /// </summary>
            public static readonly DocumentType Request = "urn:document:fss-sedo-insured-person-registration-request";
            /// <summary>
            /// Результат приёма запроса порталом
            /// </summary>
            public static readonly DocumentType Receipt = "urn:document:fss-sedo-insured-person-registration-receipt";
            /// <summary>
            /// Результат регистрации сведений
            /// </summary>
            public static readonly DocumentType ResponseResult = "urn:document:fss-sedo-insured-person-registration-response-result";
            /// <summary>
            /// Ошибка обработки
            /// </summary>
            public static readonly DocumentType ErrorMessage = "urn:document:fss-sedo-insured-person-registration-error-message";
            /// <summary>
            /// Ошибка взаимодействия с СЭДО
            /// </summary>
            public static readonly DocumentType ExchangeError = "urn:document:fss-sedo-insured-person-registration-exchange-error";
            /// <summary>
            /// Квитанция о прочтении (используется в методе генерации ответного документа)
            /// </summary>
            public static readonly DocumentType ReadReceipt = "urn:document:fss-sedo-insured-person-registration-read-receipt";
        }

        /// <summary>
        /// Результат регистрации сведений о застрахованном лице ФСС
        /// </summary>
        [PublicAPI]
        public static class FssSedoInsuredPersonRegistrationResult
        {
            /// <summary>
            /// Запрос на получение документов
            /// </summary>
            public static readonly DocumentType StatusRequestMessage = "urn:document:fss-sedo-insured-person-registration-result-status-request-message";
            /// <summary>
            /// Результат регистрации сведений
            /// </summary>
            public static readonly DocumentType ResponseResult = "urn:document:fss-sedo-insured-person-registration-result-response-result";
            /// <summary>
            /// Ошибка обработки
            /// </summary>
            public static readonly DocumentType ErrorMessage = "urn:document:fss-sedo-insured-person-registration-result-error-message";
            /// <summary>
            /// Ошибка взаимодействия с СЭДО
            /// </summary>
            public static readonly DocumentType ExchangeError = "urn:document:fss-sedo-insured-person-registration-result-exchange-error";
        }

        /// <summary>
        /// Информация о несоответствии сведений о застрахованном лице ФСС
        /// </summary>
        [PublicAPI]
        public static class FssSedoInsuredPersonMismatch
        {
            /// <summary>
            /// Запрос на получение документов
            /// </summary>
            public static readonly DocumentType RequestMessage = "urn:document:fss-sedo-insured-person-mismatch-request-message";
            /// <summary>
            /// Документ с информацией
            /// </summary>
            public static readonly DocumentType MismatchMessage = "urn:document:fss-sedo-insured-person-mismatch-mismatch-message";
            /// <summary>
            /// Ошибка обработки
            /// </summary>
            public static readonly DocumentType ErrorMessage = "urn:document:fss-sedo-insured-person-mismatch-error-message";
            /// <summary>
            /// Ошибка взаимодействия с СЭДО
            /// </summary>
            public static readonly DocumentType ExchangeError = "urn:document:fss-sedo-insured-person-mismatch-exchange-error";
            /// <summary>
            /// Квитанция о прочтении (используется в методе генерации ответного документа)
            /// </summary>
            public static readonly DocumentType ReadReceipt = "urn:document:fss-sedo-insured-person-mismatch-receipt";
        }

        /// <summary>
        /// Запрос регистрации/отзыва доверенности ФСС
        /// </summary>
        [PublicAPI]
        public static class FssWarrantManagement
        {
            /// <summary>
            /// SOAP-сообщение запроса на отправку "Создание/отзыв доверенности"
            /// </summary>
            public static readonly DocumentType RequestMessage = "urn:document:fss-warrant-management-request-message";
            /// <summary>
            /// Исходный документ "Создание/отзыв доверенности" до момента создания SOAP-запроса
            /// </summary>
            public static readonly DocumentType RequestDocument = "urn:document:fss-warrant-management-request-document";
            /// <summary>
            /// Ошибка взаимодействия с порталом СЭДО
            /// </summary>
            public static readonly DocumentType ExchangeError = "urn:document:fss-warrant-management-exchange-error";
            /// <summary>
            /// Результат отправки сообщения на портал
            /// </summary>
            public static readonly DocumentType ReceptionResult = "urn:document:fss-warrant-management-reception-result";
            /// <summary>
            /// Ошибка обработки
            /// </summary>
            public static readonly DocumentType ErrorMessage = "urn:document:fss-warrant-management-error-message";
            /// <summary>
            /// Сообщение "Результат создания/отзыва доверенности"
            /// </summary>
            public static readonly DocumentType ResponseMessage = "urn:document:fss-warrant-management-response-message";
        }

        /// <summary>
        /// Результат создания/отзыва доверенности ФСС
        /// </summary>
        [PublicAPI]
        public static class FssWarrantManagementResult
        {
            /// <summary>
            /// Запрос на получение "Результат создания/отзыва доверенности"
            /// </summary>
            public static readonly DocumentType RequestMessage = "urn:document:fss-warrant-management-result-request-message";
            /// <summary>
            /// Сообщение "Результат создания/отзыва доверенности"
            /// </summary>
            public static readonly DocumentType StatusDocument = "urn:document:fss-warrant-management-result-status-document";
            /// <summary>
            /// Ошибка обработки
            /// </summary>
            public static readonly DocumentType ErrorMessage = "urn:document:fss-warrant-management-result-error-message";
            /// <summary>
            /// Ошибка взаимодействия с порталом СЭДО
            /// </summary>
            public static readonly DocumentType ExchangeError = "urn:document:fss-warrant-management-result-exchange-error";
        }

        /// <summary>
        /// Справка о расчетах ФСС
        /// </summary>
        [PublicAPI]
        public static class FssSedoBillingInformation
        {
            /// <summary>
            /// Запрос на получение документов
            /// </summary>
            public static readonly DocumentType RequestMessage = "urn:document:fss-sedo-billing-information-request-message";
            /// <summary>
            /// Документ "Справка о расчетах ФСС"
            /// </summary>
            public static readonly DocumentType BillingInformationMessage = "urn:document:fss-sedo-billing-information-billing-information-message";
            /// <summary>
            /// Ошибка взаимодействия с СЭДО
            /// </summary>
            public static readonly DocumentType ExchangeError = "urn:document:fss-sedo-billing-information-exchange-error";
        }

        /// <summary>
        /// Запрос на формирование справки о расчетах ФСС
        /// </summary>
        [PublicAPI]
        public static class FssSedoBillingInformationDemand
        {
            /// <summary>
            /// Запрос на отправку сообщения "Запрос справки о расчетах ФСС"
            /// </summary>
            public static readonly DocumentType Request = "urn:document:fss-sedo-billing-information-demand-request";
            /// <summary>
            /// Результат отправки сообщения на портал
            /// </summary>
            public static readonly DocumentType ReceptionResult = "urn:document:fss-sedo-billing-information-demand-reception-result";
            /// <summary>
            /// Сообщение "Результат обработки запроса справки о расчетах ФСС"
            /// </summary>
            public static readonly DocumentType ResultDocument = "urn:document:fss-sedo-billing-information-demand-result-document";
            /// <summary>
            /// Ошибка взаимодействия с СЭДО
            /// </summary>
            public static readonly DocumentType ExchangeError = "urn:document:fss-sedo-billing-information-demand-exchange-error";
            /// <summary>
            /// Ошибка обработки
            /// </summary>
            public static readonly DocumentType ErrorMessage = "urn:document:fss-sedo-billing-information-demand-error-message";
            /// <summary>
            /// Документ "Справка о расчетах ФСС"
            /// </summary>
            public static readonly DocumentType BillingInformationMessage = "urn:document:fss-sedo-billing-information-demand-billing-information-message";
        }

        /// <summary>
        /// Результат обработки запроса справки о расчетах ФСС
        /// </summary>
        [PublicAPI]
        public static class FssSedoBillingInformationDemandResult
        {
            /// <summary>
            /// Запрос на получение "Результата обработки запроса справки о расчетах ФСС"
            /// </summary>
            public static readonly DocumentType RequestMessage = "urn:document:fss-sedo-billing-information-demand-result-request-message";
            /// <summary>
            /// Сообщение "Результат обработки запроса справки о расчетах ФСС"
            /// </summary>
            public static readonly DocumentType StatusDocument = "urn:document:fss-sedo-billing-information-demand-result-status-document";
            /// <summary>
            /// Ошибка взаимодействия с порталом СЭДО
            /// </summary>
            public static readonly DocumentType ExchangeError = "urn:document:fss-sedo-billing-information-demand-result-exchange-error";
        }

        /// <summary>
        /// Требование ФСС
        /// </summary>
        [PublicAPI]
        public static class FssSedoDemand
        {
            /// <summary>
            /// Запрос на получение документов
            /// </summary>
            public static readonly DocumentType RequestMessage = "urn:document:fss-sedo-demand-request-message";
            /// <summary>
            /// Документ с требованием
            /// </summary>
            public static readonly DocumentType DemandMessage = "urn:document:fss-sedo-demand-demand-message";
            /// <summary>
            /// Ошибка взаимодействия с СЭДО
            /// </summary>
            public static readonly DocumentType ExchangeError = "urn:document:fss-sedo-demand-exchange-error";
            /// <summary>
            /// Подтверждение прочтения сообщения
            /// </summary>
            public static readonly DocumentType ReceiptNotificationMessage = "urn:document:fss-sedo-demand-receipt-notification-message";
            /// <summary>
            /// Результат подтверждения прочтения сообщения
            /// </summary>
            public static readonly DocumentType ReceiptNotificationReceiptMessage = "urn:document:fss-sedo-demand-receipt-notification-receipt-message";
            /// <summary>
            /// Ошибка
            /// </summary>
            public static readonly DocumentType ErrorMessage = "urn:document:fss-sedo-demand-error-message";
        }

        /// <summary>
        /// Ответ на требование ФСС
        /// </summary>
        [PublicAPI]
        public static class FssSedoDemandReply
        {
            /// <summary>
            /// Запрос на отправку сообщения "Ответ на требование ФСС"
            /// </summary>
            public static readonly DocumentType Request = "urn:document:fss-sedo-demand-reply-request";
            /// <summary>
            /// Результат отправки сообщения на портал
            /// </summary>
            public static readonly DocumentType ReceptionResult = "urn:document:fss-sedo-demand-reply-reception-result";
            /// <summary>
            /// Сообщение "Результат обработки ответа на требование ФСС"
            /// </summary>
            public static readonly DocumentType ResultDocument = "urn:document:fss-sedo-demand-reply-result-document";
            /// <summary>
            /// Ошибка взаимодействия с СЭДО
            /// </summary>
            public static readonly DocumentType ExchangeError = "urn:document:fss-sedo-demand-reply-exchange-error";
            /// <summary>
            /// Ошибка обработки
            /// </summary>
            public static readonly DocumentType ErrorMessage = "urn:document:fss-sedo-demand-reply-error-message";
            /// <summary>
            /// Приложение
            /// </summary>
            public static readonly DocumentType Attachment = "urn:document:fss-sedo-demand-reply-attachment";
        }

        /// <summary>
        /// Результат ответа на требование ФСС
        /// </summary>
        [PublicAPI]
        public static class FssSedoDemandReplyResult
        {
            /// <summary>
            /// Запрос на получение "Результата обработки ответа на требование ФСС"
            /// </summary>
            public static readonly DocumentType RequestMessage = "urn:document:fss-sedo-demand-reply-result-request-message";
            /// <summary>
            /// Сообщение "Результат обработки ответа на требование ФСС"
            /// </summary>
            public static readonly DocumentType StatusDocument = "urn:document:fss-sedo-demand-reply-result-status-document";
            /// <summary>
            /// Ошибка взаимодействия с порталом СЭДО
            /// </summary>
            public static readonly DocumentType ExchangeError = "urn:document:fss-sedo-demand-reply-result-exchange-error";
        }

        /// <summary>
        /// Уведомление о прекращении отпуска по уходу за ребенком до полутора лет
        /// </summary>
        [PublicAPI]
        public static class FssSedoBabyCareVacationCloseNotice
        {
            /// <summary>
            /// Запрос на отправку сообщения "Уведомление о прекращении отпуска по уходу за ребенком до полутора лет"
            /// </summary>
            public static readonly DocumentType Request = "urn:document:fss-sedo-baby-care-vacation-close-notice-request";
            /// <summary>
            /// Квитанция о прочтении (используется в методе генерации ответного документа)
            /// </summary>
            public static readonly DocumentType ReadReceipt = "urn:document:fss-sedo-baby-care-vacation-close-notice-read-receipt";
            /// <summary>
            /// Результат отправки сообщения
            /// </summary>
            public static readonly DocumentType ReceptionResult = "urn:document:fss-sedo-baby-care-vacation-close-notice-reception-result";
            /// <summary>
            /// Сообщение "Уведомление о прекращении отпуска по уходу за ребенком до полутора лет"
            /// </summary>
            public static readonly DocumentType ResultDocument = "urn:document:fss-sedo-baby-care-vacation-close-notice-result-document";
            /// <summary>
            /// Ошибка обработки
            /// </summary>
            public static readonly DocumentType ErrorMessage = "urn:document:fss-sedo-baby-care-vacation-close-notice-error-message";
            /// <summary>
            /// Ошибка взаимодействия с порталом СЭДО
            /// </summary>
            public static readonly DocumentType ExchangeError = "urn:document:fss-sedo-baby-care-vacation-close-notice-exchange-error";
        }

        /// <summary>
        /// Уведомление о статусе выплаты пособия
        /// </summary>
        [PublicAPI]
        public static class FssSedoBenefitPaymentStatusNotice
        {
            /// <summary>
            /// Запрос на получение документов
            /// </summary>
            public static readonly DocumentType RequestMessage = "urn:document:fss-sedo-benefit-payment-status-notice-request-message";
            /// <summary>
            /// Документ "Уведомление о статусе выплаты пособия"
            /// </summary>
            public static readonly DocumentType BenefitStatusNoticeMessage = "urn:document:fss-sedo-benefit-payment-status-notice-benefit-status-notice-message";
            /// <summary>
            /// Ошибка обработки
            /// </summary>
            public static readonly DocumentType ErrorMessage = "urn:document:fss-sedo-benefit-payment-status-notice-error-message";
            /// <summary>
            /// Ошибка взаимодействия с СЭДО
            /// </summary>
            public static readonly DocumentType ExchangeError = "urn:document:fss-sedo-benefit-payment-status-notice-exchange-error";
        }

    }
}