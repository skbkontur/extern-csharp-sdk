using System.Diagnostics.CodeAnalysis;

namespace Kontur.Extern.Api.Client.Models.Docflows.Enums
{
    [SuppressMessage("ReSharper", "CommentTypo")]
    public partial struct DocflowType
    {
        /// <summary>
        /// Расчет по форме 4-ФСС
        /// </summary>
        public static readonly DocflowType FssReport = "urn:docflow:fss-report";

        /// <summary>
        /// Подтверждение основного вида экономической деятельности
        /// </summary>
        public static readonly DocflowType OvedConfirmation = "urn:docflow:oved-confirmation";

        /// <summary>
        /// Листки нетрудоспособности ФСС
        /// </summary>
        public static readonly DocflowType FssSickReport = "urn:docflow:fss-sick-report";

        /// <summary>
        /// Уведомления об изменении статуса электронного больничного листа
        /// </summary>
        public static readonly DocflowType FssSedoSickReportChangeNotification = "urn:docflow:fss-sedo-sick-report-change-notification";

        /// <summary>
        /// Извещение о прямых выплатах мер социального обеспечения
        /// </summary>
        public static readonly DocflowType FssSedoPvsoNotification = "urn:docflow:fss-sedo-pvso-notification";

        /// <summary>
        /// Сообщения об ошибках в документообортах ФСС через СЭДО
        /// </summary>
        public static readonly DocflowType FssSedoError = "urn:docflow:fss-sedo-error";

        /// <summary>
        /// Подписка страхователя на документооборот с ФСС
        /// </summary>
        public static readonly DocflowType FssSedoAbonentSubscription = "urn:docflow:fss-sedo-abonent-subscription";

        /// <summary>
        /// Подписка оператора на документооборот с ФСС по абоненту
        /// </summary>
        public static readonly DocflowType FssSedoProviderSubscription = "urn:docflow:fss-sedo-provider-subscription";

        /// <summary>
        /// Результат приема подписки страхователя
        /// </summary>
        public static readonly DocflowType FssSedoAbonentSubscriptionResult = "urn:docflow:fss-sedo-abonent-subscription-result";

        /// <summary>
        /// Запрос недостающих сведений для назначения пособия ФСС
        /// </summary>
        public static readonly DocflowType FssSedoProactivePaymentsDemand = "urn:docflow:fss-sedo-proactive-payments-demand";

        /// <summary>
        /// Сведения для назначения пособия ФСС
        /// </summary>
        public static readonly DocflowType FssSedoProactivePaymentsReply = "urn:docflow:fss-sedo-proactive-payments-reply";

        /// <summary>
        /// Результат обработки сведений для назначения пособия ФСС
        /// </summary>
        public static readonly DocflowType FssSedoProactivePaymentsReplyResult = "urn:docflow:fss-sedo-proactive-payments-reply-result";

        /// <summary>
        /// Выплата пособия ФСС
        /// </summary>
        public static readonly DocflowType FssSedoProactivePaymentsBenefit = "urn:docflow:fss-sedo-proactive-payments-benefit";

        /// <summary>
        /// Регистрация сведений о застрахованном лице ФСС
        /// </summary>
        public static readonly DocflowType FssSedoInsuredPersonRegistration = "urn:docflow:fss-sedo-insured-person-registration";

        /// <summary>
        /// Результат регистрации сведений о застрахованном лице ФСС
        /// </summary>
        public static readonly DocflowType FssSedoInsuredPersonRegistrationResult = "urn:docflow:fss-sedo-insured-person-registration-result";

        /// <summary>
        /// Информация о несоответствии сведений о застрахованном лице ФСС
        /// </summary>
        public static readonly DocflowType FssSedoInsuredPersonMismatch = "urn:docflow:fss-sedo-insured-person-mismatch";

        /// <summary>
        /// Инициация выплат пособия ФСС
        /// </summary>
        public static readonly DocflowType FssSedoBenefitPaymentInitiation = "urn:docflow:fss-sedo-benefit-payment-initiation";

        /// <summary>
        /// Результат инициации выплат пособия ФСС
        /// </summary>
        public static readonly DocflowType FssSedoBenefitPaymentInitiationResult = "urn:docflow:fss-sedo-benefit-payment-initiation-result";

        /// <summary>
        /// Требование ФСС
        /// </summary>
        public static readonly DocflowType FssSedoDemand = "urn:docflow:fss-sedo-demand";

        /// <summary>
        /// Ответ на требование ФСС
        /// </summary>
        public static readonly DocflowType FssSedoDemandReply = "urn:docflow:fss-sedo-demand-reply";

        /// <summary>
        /// Запрос на формирование справки о расчетах ФСС
        /// </summary>
        public static readonly DocflowType FssSedoBillingInformationDemand = "urn:docflow:fss-sedo-billing-information-demand";

        /// <summary>
        /// Справка о расчетах ФСС
        /// </summary>
        public static readonly DocflowType FssSedoBillingInformation = "urn:docflow:fss-sedo-billing-information";

        /// <summary>
        /// Уведомление о прекращении отпуска по уходу за ребенком до полутора лет
        /// </summary>
        public static readonly DocflowType FssSedoBabyCareVacationCloseNotice = "urn:docflow:fss-sedo-baby-care-vacation-close-notice";

        /// <summary>
        /// Заявление о возмещении расходов на оплату дополнительных выходных дней для ухода за детьми-инвалидами
        /// </summary>
        public static readonly DocflowType FssSedoDisabilityChildrenDemand = "urn:docflow:fss-sedo-disability-children-demand";
        /// <summary>
        /// Уведомление о статусе выплаты пособия
        /// </summary>
        public static readonly DocflowType FssSedoBenefitPaymentStatusNotice = "urn:docflow:fss-sedo-benefit-payment-status-notice";

        /// <summary>
        /// Уведомление о непоступлении ответа на запрос в срок
        /// </summary>
        public static readonly DocflowType FssSedoProactiveExpireNotice = "urn:docflow:fss-sedo-proactive-expire-notice";

        /// <summary>
        /// Сведения о зарплате сотрудника
        /// </summary>
        public static readonly DocflowType FssSedoEmployeeSalaryInformation = "urn:docflow:fss-sedo-employee-salary-information";

        /// <summary>
        /// Ответ страхователя на обращение СФР
        /// </summary>
        public static readonly DocflowType FssSedoAppealReply = "urn:docflow:fss-sedo-appeal-reply";

        /// <summary>
        /// Обращение СФР к страхователю
        /// </summary>
        public static readonly DocflowType FssSedoAppeal = "urn:docflow:fss-sedo-appeal";

        /// <summary>
        /// Подтверждение основного вида экономической деятельности на СЭДО
        /// </summary>
        public static readonly DocflowType FssSedoOvedConfirmation = "urn:docflow:fss-sedo-oved-confirmation";
    }
}