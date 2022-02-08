using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Enums
{
    [SuppressMessage("ReSharper", "CommentTypo")]
    public partial struct DocflowType
    {
        /// <summary>
        /// ФСС
        /// </summary>
        [PublicAPI]
        public static class Fss
        {
            /// <summary>
            /// Расчет по форме 4-ФСС
            /// </summary>
            public static readonly DocflowType Report = "urn:docflow:fss-report";

            /// <summary>
            /// Подтверждение основного вида экономической деятельности
            /// </summary>
            public static readonly DocflowType OvedConfirmation = "urn:docflow:oved-confirmation";

            /// <summary>
            /// Листки нетрудоспособности ФСС
            /// </summary>
            public static readonly DocflowType SickReport = "urn:docflow:fss-sick-report";

            /// <summary>
            /// Социальный Электронный Документооборот 
            /// </summary>
            [PublicAPI]
            public static class Sedo
            {
                /// <summary>
                /// Уведомления об изменении статуса электронного больничного листа
                /// </summary>
                public static readonly DocflowType SickReportChangeNotification = "urn:docflow:fss-sedo-sick-report-change-notification";

                /// <summary>
                /// Извещение о прямых выплатах мер социального обеспечения
                /// </summary>
                public static readonly DocflowType PvsoNotification = "urn:docflow:fss-sedo-pvso-notification";

                /// <summary>
                /// Сообщения об ошибках в документообортах ФСС через СЭДО
                /// </summary>
                public static readonly DocflowType Error = "urn:docflow:fss-sedo-error";

                /// <summary>
                /// Подписка страхователя на документооборот с ФСС
                /// </summary>
                public static readonly DocflowType AbonentSubscription = "urn:docflow:fss-sedo-abonent-subscription";

                /// <summary>
                /// Подписка оператора на документооборот с ФСС по абоненту
                /// </summary>
                public static readonly DocflowType ProviderSubscription = "urn:docflow:fss-sedo-provider-subscription";

                /// <summary>
                /// Результат приема подписки страхователя
                /// </summary>
                public static readonly DocflowType AbonentSubscriptionResult = "urn:docflow:fss-sedo-abonent-subscription-result";

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
    }
}