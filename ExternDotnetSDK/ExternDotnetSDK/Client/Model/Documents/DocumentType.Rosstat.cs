// ReSharper disable CommentTypo
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Model.Documents
{
    public partial struct DocumentType
    {
        /// <summary>
        /// Росстат
        /// </summary>
        [PublicAPI]
        public static class Rosstat
        {
            /// <summary>
            /// Отчет в Росстат
            /// </summary>
            [PublicAPI]
            public static class Report
            {
                /// <summary>
                /// Отчет, направляемый в ТОГС (установленный формат)	
                /// Имя (согласно нормативным документам): Отчет
                /// </summary>
                public static readonly DocumentType StatReport = "urn:document:stat-report";
                /// <summary>
                /// Служебный документ, в котором передается описание отчета (установленный формат)	
                /// Имя (согласно нормативным документам): ОписаниеОтчета
                /// Формирует оператор ЭДО при отправке документооборота.
                /// </summary>
                public static readonly DocumentType StatReportDescription = "urn:document:stat-report-description";
                /// <summary>
                /// Подтверждение даты отправки документа (установленный формат)	
                /// Имя (согласно нормативным документам): ПодтверждениеОператора
                /// Формирует оператор ЭДО при отправке документооборота.
                /// </summary>
                public static readonly DocumentType StatReportDateConfirmation = "urn:document:stat-report-date-confirmation";
                /// <summary>
                /// Неформализованное приложение к отчету	
                /// Имя (согласно нормативным документам): ПриложениеПисьма
                /// </summary>
                public static readonly DocumentType StatReportAttachment = "urn:document:stat-report-attachment";
                /// <summary>
                /// Извещение о получении документа его получателем (установленный формат)	
                /// Имя (согласно нормативным документам): ИзвещениеОПолучении
                /// </summary>
                public static readonly DocumentType StatReportReceipt = "urn:document:stat-report-receipt";
                /// <summary>
                /// Уведомление о принятии отчета ТОГСом	
                /// Имя (согласно нормативным документам): УведомлениеОПриемеВОбработку
                /// </summary>
                public static readonly DocumentType StatReportProtocolV2Positive = "urn:document:stat-report-protocol-v2-positive";
                /// <summary>
                /// Уведомление об отказе в принятии отчета ТОГСом	
                /// Имя (согласно нормативным документам): УведомлениеОНесоответствииФормату
                /// </summary>
                public static readonly DocumentType StatReportProtocolV2Negative = "urn:document:stat-report-protocol-v2-negative";
                /// <summary>
                /// Уведомление о наличии несоответствий в предоставленном отчете и необходимости его повторной отправки	
                /// Имя (согласно нормативным документам): УведомлениеОбУточнении
                /// </summary>
                public static readonly DocumentType StatReportProtocolV2Precise = "urn:document:stat-report-protocol-v2-precise";
                /// <summary>
                /// Уведомление о невозможности принятия отчета по причине его сдачи другим способом отчетности	
                /// Имя (согласно нормативным документам): уведомлениеОбОтклонении
                /// </summary>
                public static readonly DocumentType StatReportProtocolV2Reject = "urn:document:stat-report-protocol-v2-reject";
            }

            /// <summary>
            /// Письмо в Росстат
            /// </summary>
            [PublicAPI]
            public static class LetterToRosstat
            {
                /// <summary>
                /// Неформализованный текст письма	
                /// Имя (согласно нормативным документам): Письмо
                /// </summary>
                public static readonly DocumentType Text = "urn:document:stat-letter";
                /// <summary>
                /// Служебный документ, в котором передается описание письма (установленный формат)	
                /// Имя (согласно нормативным документам): ОписаниеПисьма
                /// Формирует оператор ЭДО при отправке документооборота.
                /// </summary>
                public static readonly DocumentType Description = "urn:document:stat-letter-description";
                /// <summary>
                /// Неформализованное приложение к письму	
                /// Имя (согласно нормативным документам): ПриложениеПисьма
                /// </summary>
                public static readonly DocumentType Attachment = "urn:document:stat-letter-attachment";
                /// <summary>
                /// Извещение о получении письма его получателем (установленный формат)	
                /// Имя (согласно нормативным документам): ИзвещениеОПолучении
                /// </summary>
                public static readonly DocumentType Receipt = "urn:document:stat-letter-receipt";
                /// <summary>
                /// Подтверждение даты отправки письма (установленный формат)	
                /// Имя (согласно нормативным документам): ПодтверждениеОператора
                /// Формирует оператор ЭДО при отправке документооборота.
                /// </summary>
                public static readonly DocumentType Confirmation = "urn:document:stat-letter-confirmation";
            }

            /// <summary>
            /// Письмо из Росстата
            /// </summary>
            [PublicAPI]
            public static class LetterFromRosstat
            {
                /// <summary>
                /// Неформализованный текст письма	
                /// Имя (согласно нормативным документам): Письмо
                /// </summary>
                public static readonly DocumentType Letter = "urn:document:stat-cu-letter";
                /// <summary>
                /// Неформализованное приложение к письму	
                /// Имя (согласно нормативным документам): ПриложениеПисьма
                /// </summary>
                public static readonly DocumentType LetterAttachment = "urn:document:stat-cu-letter-attachment";
                /// <summary>
                /// Извещение о получении письма его получателем (установленный формат)	
                /// Имя (согласно нормативным документам): ИзвещениеОПолучении
                /// </summary>
                public static readonly DocumentType LetterReceipt = "urn:document:stat-cu-letter-receipt";
                /// <summary>
                /// Подтверждение даты получения письма (установленный формат)	
                /// Имя (согласно нормативным документам): ПодтверждениеОператора
                /// </summary>
                public static readonly DocumentType LetterConfirmation = "urn:document:stat-cu-letter-confirmation";
                /// <summary>
                /// Служебный документ, в котором передается описание письма (установленный формат)	
                /// Имя (согласно нормативным документам): ОписаниеПисьма
                /// Формирует оператор ЭДО при отправке документооборота.
                /// </summary>
                public static readonly DocumentType LetterDescription = "urn:document:stat-cu-letter-description";
            }

            /// <summary>
            /// Массовая рассылка из Росстата
            /// </summary>
            [PublicAPI]
            public static class Broadcast
            {
                /// <summary>
                /// Неформализованный текст информационной рассылки	
                /// Имя (согласно нормативным документам): Рассылка
                /// </summary>
                public static readonly DocumentType BroadcastText = "urn:document:stat-cu-broadcast";
                /// <summary>
                /// Служебный документ, в котором передается описание рассылки (установленный формат)	
                /// Имя (согласно нормативным документам): ОписаниеПисьма
                /// </summary>
                public static readonly DocumentType BroadcastDescription = "urn:document:stat-cu-broadcast-description";
                /// <summary>
                /// Неформализованное приложение к рассылке	
                /// Имя (согласно нормативным документам): ПриложениеПисьма
                /// </summary>
                public static readonly DocumentType BroadcastAttachment = "urn:document:stat-cu-broadcast-attachment";
                /// <summary>
                /// Подтверждение даты получения рассылки (установленный формат)	
                /// Имя (согласно нормативным документам): ПодтверждениеОператора
                /// </summary>
                public static readonly DocumentType BroadcastConfirmation = "urn:document:stat-cu-broadcast-confirmation";
            }
        }
    }
}