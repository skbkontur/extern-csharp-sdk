using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Models.Docflows.Enums
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
            }
        }
    }
}