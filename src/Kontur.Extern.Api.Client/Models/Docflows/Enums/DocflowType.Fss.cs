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
    }
}