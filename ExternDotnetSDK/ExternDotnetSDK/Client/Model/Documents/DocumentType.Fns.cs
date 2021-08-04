using JetBrains.Annotations;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.Model.Documents
{
    public partial struct DocumentType
    {
        /// <summary>
        /// Типы документов ФНС 
        /// </summary>
        [PublicAPI]
        public static class Fns
        {
            /// <summary>
            /// Сообщение об ошибке от КО
            /// Имя (согласно нормативным документам): СообщениеОбОшибке
            ///</summary>
            public static readonly DocumentType Error = "urn:document:error";
            
            /// <summary>
            /// Описание действий для абонента для решения причины ошибки
            ///</summary>
            public static readonly DocumentType ErrorDescriptionForAbonent = "urn:document:error-description-for-abonent";
            
            /// <summary>
            /// Декларация и 2-НДФЛ
            /// </summary>
            [PublicAPI]
            public static class Fns534Report
            {
                /// <summary>                
                /// Налоговая декларация или бухгалтерская отчетность (установленный формат).
                /// Декларация
                /// </summary>
                public static readonly DocumentType Report = "urn:document:fns534-report";

                /// <summary>
                /// Служебный документ, в котором передается описание декларации (установленный формат).
                /// Имя (согласно нормативным документам): Описание
                /// Формирует оператор ЭДО при отправке документооборота.
                /// </summary>
                public static readonly DocumentType Description = "urn:document:fns534-report-description";

                /// Информационное сообщение о доверенности уполномоченного лица (установленный формат).
                /// Имя (согласно нормативным документам): Доверенность
                public static readonly DocumentType Warrant = "urn:document:fns534-report-warrant";

                /// Подтверждение даты отправки документа (установленный формат).
                /// Имя (согласно нормативным документам): ПодтверждениеДатыОтправки
                /// Формирует оператор ЭДО при отправке документооборота.
                public static readonly DocumentType DateConfirmation = "urn:document:fns534-report-date-confirmation";

                /// <summary>
                /// Квитанция о приеме налоговой декларации (установленный формат).
                /// Имя (согласно нормативным документам): КвитанцияОПриеме
                /// </summary>
                public static readonly DocumentType AcceptanceResultPositive = "urn:document:fns534-report-acceptance-result-positive";

                /// <summary>
                /// Уведомление об отказе в приеме налоговой декларации (установленный формат).
                /// Имя (согласно нормативным документам): УведомлениеОбОтказе
                /// </summary>
                public static readonly DocumentType ReportAcceptanceResultNegative = "urn:document:fns534-report-acceptance-result-negative";

                /// <summary>
                /// Извещение о вводе декларации в информационную систему налогового органа (установленный формат).
                /// Имя (согласно нормативным документам): ИзвещениеОВводе
                /// </summary>
                public static readonly DocumentType ProcessingResultOk = "urn:document:fns534-report-processing-result-ok";

                /// <summary>
                /// Уведомление о необходимости внесения уточнений (установленный формат).
                /// Имя (согласно нормативным документам): УведомлениеОбУточнении
                /// </summary>
                public static readonly DocumentType ProcessingResultPrecise = "urn:document:fns534-report-processing-result-precise";

                /// <summary>
                /// Извещение о получении документа его получателем (установленный формат).
                /// Имя (согласно нормативным документам): ИзвещениеОПолучении
                /// </summary>
                public static readonly DocumentType Receipt = "urn:document:fns534-report-receipt";

                /// <summary>
                /// Протокол приема сведений о доходах физических лиц от налоговых агентов (установленный формат).
                /// Имя (согласно нормативным документам): ПротоколПриема2НДФЛ
                /// </summary>
                public static readonly DocumentType AcceptanceResult2NdflProtocol = "urn:document:fns534-report-acceptance-result-2ndfl-protocol";

                /// <summary>
                /// Реестр сведений о доходах физических лиц (установленный формат).
                /// Имя (согласно нормативным документам): РеестрПринятыхДокументов
                /// </summary>
                public static readonly DocumentType AcceptanceResult2NdflRegistry = "urn:document:fns534-report-acceptance-result-2ndfl-registry";

                /// <summary>
                /// Неформализованное приложение или формализованное приложение (установленный формат).
                /// Имя (согласно нормативным документам): Приложение
                /// </summary>
                public static readonly DocumentType Attachment = "urn:document:fns534-report-attachment";
            }

            /// <summary>
            /// ИОН
            /// </summary>
            [PublicAPI]
            public static class Fns534Ion
            {
                /// <summary>
                /// Запрос на предоставление информации (установленный формат).
                /// Имя (согласно нормативным документам): Запрос
                /// </summary>
                public static readonly DocumentType Request = "urn:document:fns534-ion-request";
                /// <summary>
                /// Подтверждение даты отправки (установленный формат).
                /// Имя (согласно нормативным документам): ПодтверждениеДатыОтправки
                /// Формирует оператор ЭДО при отправке документооборота.
                /// </summary>
                public static readonly DocumentType DateConfirmation = "urn:document:fns534-ion-date-confirmation";
                /// <summary>
                /// Служебный документ, в котором передается описание запроса (установленный формат).
                /// Имя (согласно нормативным документам): Описание
                /// Формирует оператор ЭДО при отправке документооборота.
                /// </summary>
                public static readonly DocumentType Description = "urn:document:fns534-ion-description";
                /// <summary>
                /// Квитанция о приеме (установленный формат).
                /// Имя (согласно нормативным документам): КвитанцияОПриеме
                /// </summary>
                public static readonly DocumentType AcceptanceResultPositive = "urn:document:fns534-ion-acceptance-result-positive";
                /// <summary>
                /// Уведомление, содержащее отказ абоненту в предоставлении запрошенных сведений с описанием выявленных ошибок и причин отказа в приеме (установленный формат).
                /// Имя (согласно нормативным документам): УведомлениеОбОтказе
                /// </summary>
                public static readonly DocumentType AcceptanceResultNegative = "urn:document:fns534-ion-acceptance-result-negative";
                /// <summary>
                /// Документ, содержащий запрошенный абонентом документ (установленный формат) и/или Уведомление об отказе, как ответ об отсутствии данных по запрашиваемому абоненту (установленный формат).
                /// Имя (согласно нормативным документам): Ответ
                /// </summary>
                public static readonly DocumentType Response = "urn:document:fns534-ion-response";
                /// <summary>
                /// Информационное сообщение о доверенности уполномоченного лица (установленный формат).
                /// Имя (согласно нормативным документам): Доверенность
                /// </summary>
                public static readonly DocumentType Warrant = "urn:document:fns534-ion-warrant";
                /// <summary>
                /// Извещение о получении документа его получателем (установленный формат).
                /// Имя (согласно нормативным документам): ИзвещениеОПолучении
                /// </summary>
                public static readonly DocumentType Receipt = "urn:document:fns534-ion-receipt";
            }

            /// <summary>
            /// Письмо в ФНС
            /// </summary>
            [PublicAPI]
            public static class Fns534Letter
            {
                /// <summary>
                /// Текст обращения (установленный формат).	
                /// Имя (согласно нормативным документам): Обращение
                /// </summary>
                public static readonly DocumentType Letter = "urn:document:fns534-letter";
                /// <summary>
                /// Служебный документ, в котором передается описание (установленный формат).	
                /// Имя (согласно нормативным документам): Описание
                /// Формирует оператор ЭДО при отправке документооборота.
                /// </summary>
                public static readonly DocumentType Description = "urn:document:fns534-letter-description";
                /// <summary>
                /// Неформализованное приложение к письму.	
                /// Имя (согласно нормативным документам): Приложение
                /// </summary>
                public static readonly DocumentType Attachment = "urn:document:fns534-letter-attachment";
                /// <summary>
                /// Подтверждение даты отправки документа (установленный формат).	
                /// Имя (согласно нормативным документам): ПодтверждениеДатыОтправки
                /// Формирует оператор ЭДО при отправке документооборота.
                /// </summary>
                public static readonly DocumentType DateConfirmation = "urn:document:fns534-letter-date-confirmation";
                /// <summary>
                /// Уведомление об отказе в приеме обращения (установленный формат).	
                /// Имя (согласно нормативным документам): УведомлениеОбОтказе
                /// </summary>
                public static readonly DocumentType DeclineNotice = "urn:document:fns534-letter-decline-notice";
                /// <summary>
                /// Извещение о получении документа его получателем (установленный формат).	
                /// Имя (согласно нормативным документам): ИзвещениеОПолучении
                /// </summary>
                public static readonly DocumentType Receipt = "urn:document:fns534-letter-receipt";
                /// <summary>
                /// Информационное сообщение о доверенности уполномоченного лица (установленный формат).	
                /// Имя (согласно нормативным документам): Доверенность
                /// </summary>
                public static readonly DocumentType Warrant = "urn:document:fns534-letter-warrant";
            }

            /// <summary>
            /// Письмо из ФНС
            /// </summary>
            [PublicAPI]
            public static class Fns534CuLetter
            {
                /// <summary>
                /// Текст письма (установленный формат).	
                /// Имя (согласно нормативным документам): Письмо
                /// </summary>
                public static readonly DocumentType Letter = "urn:document:fns534-cu-letter";

                /// <summary>
                /// Служебный документ, в котором передается описание (установленный формат).	
                /// Имя (согласно нормативным документам): Описание
                /// </summary>
                public static readonly DocumentType Description = "urn:document:fns534-cu-letter-description";

                /// <summary>
                /// Неформализованное приложение к письму.	
                /// Имя (согласно нормативным документам): Приложение
                /// </summary>
                public static readonly DocumentType Attachment = "urn:document:fns534-cu-letter-attachment";

                /// <summary>
                /// Подтверждение даты отправки документа (установленный формат).	
                /// Имя (согласно нормативным документам): ПодтверждениеДатыОтправки
                /// Формирует оператор ЭДО при отправке документооборота.
                /// </summary>
                public static readonly DocumentType DateConfirmation = "urn:document:fns534-cu-letter-date-confirmation";

                /// <summary>
                /// Извещение о получении документа его получателем (установленный формат).	
                /// Имя (согласно нормативным документам): ИзвещениеОПолучении
                /// </summary>
                public static readonly DocumentType Receipt = "urn:document:fns534-cu-letter-receipt";
            }

            /// <summary>
            /// Представление
            /// </summary>
            [PublicAPI]
            public static class Fns534Submission
            {
                /// <summary>
                /// Документ (установленный формат).	
                /// Имя (согласно нормативным документам): Представление
                ///</summary>
                public static readonly DocumentType Message = "urn:document:fns534-submission-message";

                /// <summary>
                /// Служебный документ, в котором передается описание документа представление (установленный формат).	
                /// Имя (согласно нормативным документам): Описание
                /// Формирует оператор ЭДО при отправке документооборота.
                ///</summary>
                public static readonly DocumentType Description = "urn:document:fns534-submission-description";

                /// <summary>
                /// Документы, которые могут идти в составе с основным документом.	
                /// Имя (согласно нормативным документам): Приложение
                ///</summary>
                public static readonly DocumentType Attachment = "urn:document:fns534-submission-attachment";

                /// <summary>
                /// Информационное сообщение о доверенности уполномоченного лица (установленный формат).	
                /// Имя (согласно нормативным документам): Доверенность
                ///</summary>
                public static readonly DocumentType Warrant = "urn:document:fns534-submission-warrant";

                /// <summary>
                /// Подтверждение даты отправки документа (установленный формат).	
                /// Имя (согласно нормативным документам): ПодтверждениеДатыОтправки
                /// Формирует оператор ЭДО при отправке документооборота.
                ///</summary>
                public static readonly DocumentType DateConfirmation = "urn:document:fns534-submission-date-confirmation";

                /// <summary>
                /// Извещение о получении документа его получателем (установленный формат).	
                /// Имя (согласно нормативным документам): ИзвещениеОПолучении
                ///</summary>
                public static readonly DocumentType Receipt = "urn:document:fns534-submission-receipt";

                /// <summary>
                /// Квитанция о приеме налоговой декларации (установленный формат).	
                /// Имя (согласно нормативным документам): КвитанцияОПриеме
                ///</summary>
                public static readonly DocumentType AcceptanceResultPositive = "urn:document:fns534-submission-acceptance-result-positive";

                /// <summary>
                /// Уведомление об отказе в приеме налоговой декларации (установленный формат).	
                /// Имя (согласно нормативным документам): УведомлениеОбОтказе
                ///</summary>
                public static readonly DocumentType AcceptanceResultNegative = "urn:document:fns534-submission-acceptance-result-negative";
            }

            /// <summary>
            /// Документ (Требование)
            /// </summary>
            [PublicAPI]
            public static class Fns534Demand
            {
                /// <summary>
                /// Файл-описание к требованиям (установленный формат).	
                /// Имя (согласно нормативным документам): Документ
                /// </summary>
                public static readonly DocumentType Demand = "urn:document:fns534-demand";

                /// <summary>
                /// Служебный документ, в котором передается описание документа (установленный формат).	
                /// Имя (согласно нормативным документам): Описание
                /// </summary>
                public static readonly DocumentType Description = "urn:document:fns534-demand-description";

                /// <summary>
                /// Требование.	
                /// Имя (согласно нормативным документам): Приложение
                /// </summary>
                public static readonly DocumentType Attachment = "urn:document:fns534-demand-attachment";

                /// <summary>
                /// Подтверждение даты отправки документа (установленный формат).	
                /// Имя (согласно нормативным документам): ПодтверждениеДатыОтправки
                /// Формирует оператор ЭДО при отправке документооборота.
                /// </summary>
                public static readonly DocumentType DateConfirmation = "urn:document:fns534-demand-date-confirmation";

                /// <summary>
                /// Извещение о получении документа его получателем (установленный формат).	
                /// Имя (согласно нормативным документам): ИзвещениеОПолучении
                /// </summary>
                public static readonly DocumentType Receipt = "urn:document:fns534-demand-receipt";

                /// <summary>
                /// Уведомление об отказе в приеме документа (установленный формат).	
                /// Имя (согласно нормативным документам): УведомлениеОбОтказе
                /// </summary>
                public static readonly DocumentType AcceptanceResultNegative = "urn:document:fns534-demand-acceptance-result-negative";

                /// <summary>
                /// Квитанция о приеме документа (установленный формат).	
                /// Имя (согласно нормативным документам): КвитанцияОПриеме
                /// </summary>
                public static readonly DocumentType AcceptanceResultPositive = "urn:document:fns534-demand-acceptance-result-positive";
            }

            /// <summary>
            /// Опись (Ответ на требование)
            /// </summary>
            [PublicAPI]
            public static class Fns534Inventory
            {
                /// <summary>
                /// Документ(установленный формат).
                /// Имя (согласно нормативным документам): Представление
                ///</summary>
                public static readonly DocumentType Message = "urn:document:fns534-inventory-message";

                /// <summary>
                /// Служебный документ, в котором передается описание документа представление(установленный формат).
                /// Имя (согласно нормативным документам): Описание
                /// Формирует оператор ЭДО при отправке документооборота.
                ///</summary>
                public static readonly DocumentType Description = "urn:document:fns534-inventory-description";

                /// <summary>
                /// Документы, которые могут идти в составе с основным документом.
                /// Имя (согласно нормативным документам): Приложение
                ///</summary>
                public static readonly DocumentType Attachment = "urn:document:fns534-inventory-attachment";

                /// <summary>
                /// Информационное сообщение о представительстве уполномоченного лица(установленный формат).
                /// Имя (согласно нормативным документам): Доверенность
                ///</summary>
                public static readonly DocumentType Warrant = "urn:document:fns534-inventory-warrant";

                /// <summary>
                /// Подтверждение даты отправки документа(установленный формат).
                /// Имя (согласно нормативным документам): ПодтверждениеДатыОтправки
                /// Формирует оператор ЭДО при отправке документооборота.
                ///</summary>
                public static readonly DocumentType DateConfirmation = "urn:document:fns534-inventory-date-confirmation";

                /// <summary>
                /// Извещение о получении документа его получателем(установленный формат).
                /// Имя (согласно нормативным документам): ИзвещениеОПолучении
                ///</summary>
                public static readonly DocumentType Receipt = "urn:document:fns534-inventory-receipt";

                /// <summary>
                /// Квитанция о приеме налоговой декларации(установленный формат).
                /// Имя (согласно нормативным документам): КвитанцияОПриеме
                ///</summary>
                public static readonly DocumentType AcceptanceResultPositive = "urn:document:fns534-inventory-acceptance-result-positive";

                /// <summary>
                /// Уведомление об отказе в приеме налоговой декларации(установленный формат).
                /// Имя (согласно нормативным документам): УведомлениеОбОтказе
                ///</summary>
                public static readonly DocumentType AcceptanceResultNegative = "urn:document:fns534-inventory-acceptance-result-negative";
            }

            /// <summary>
            /// Заявление
            /// </summary>
            [PublicAPI]
            public static class Fns534Application
            {
                /// <summary>
                /// Заявление российского покупателя о ввозе товаров и уплате косвенных налогов(установленный формат).
                /// Имя (согласно нормативным документам): Заявление
                ///</summary>
                public static readonly DocumentType Application = "urn:document:fns534-application";

                /// <summary>
                /// Служебный документ, в котором передается описание заявления(установленный формат).
                /// Имя (согласно нормативным документам): Описание
                /// Формирует оператор ЭДО при отправке документооборота.
                ///</summary>
                public static readonly DocumentType Description = "urn:document:fns534-application-description";

                /// <summary>
                /// Информационное сообщение о доверенности уполномоченного лица(установленный формат).
                /// Имя (согласно нормативным документам): Доверенность
                ///</summary>
                public static readonly DocumentType Warrant = "urn:document:fns534-application-warrant";

                /// <summary>
                /// Подтверждение даты отправки документа(установленный формат).
                /// Имя (согласно нормативным документам): ПодтверждениеДатыОтправки
                /// Формирует оператор ЭДО при отправке документооборота.
                ///</summary>
                public static readonly DocumentType DateConfirmation = "urn:document:fns534-application-date-confirmation";

                /// <summary>
                /// Уведомление об отказе в приеме заявления(установленный формат).
                /// Имя (согласно нормативным документам): УведомлениеОбОтказе
                ///</summary>
                public static readonly DocumentType AcceptanceResultNegative = "urn:document:fns534-application-acceptance-result-negative";

                /// <summary>
                /// Квитанция о приеме заявления(установленный формат).
                /// Имя (согласно нормативным документам): КвитанцияОПриеме
                ///</summary>
                public static readonly DocumentType AcceptanceResultPositive = "urn:document:fns534-application-acceptance-result-positive";

                /// <summary>
                /// Извещение о получении документа его получателем(установленный формат).
                /// Имя (согласно нормативным документам): ИзвещениеОПолучении
                ///</summary>
                public static readonly DocumentType Receipt = "urn:document:fns534-application-receipt";

                /// <summary>
                /// Сообщение о проставлении отметки налогового органа(установленный формат).
                /// Имя (согласно нормативным документам): СообщениеОПростОтметки
                ///</summary>
                public static readonly DocumentType ProcessingResultPositive = "urn:document:fns534-application-processing-result-positive";

                /// <summary>
                /// Уведомление об отказе в проставлении на заявлении о ввозе товаров и уплате косвенных налогов отметки налогового органа об уплате косвенных налогов(установленный формат).
                /// Имя (согласно нормативным документам): УведомлениеОбОтказеОтметки
                ///</summary>
                public static readonly DocumentType ProcessingResultNegative = "urn:document:fns534-application-processing-result-negative";
            }

            /// <summary>
            /// Регистрация бизнеса
            /// </summary>
            [PublicAPI]
            public static class BusinessRegistration
            {
                /// <summary>
                /// Служебный документ, в котором передается описание документа (установленный формат)
                /// Имя (согласно нормативным документам): Опись представляемого пакета документов
                ///</summary>
                public static readonly DocumentType Inventory = "urn:document:business-registration-inventory";
                /// <summary>
                /// Документ об уплате государственной пошлины
                /// Имя (согласно нормативным документам): Госпошлина
                ///</summary>
                public static readonly DocumentType Duty = "urn:document:business-registration-duty";
                /// <summary>
                /// Заявление о государственной регистрации юридического лица, физического лица в качестве индивидуального предпринимателя
                /// Имя (согласно нормативным документам): Заявление по установленной форме
                ///</summary>
                public static readonly DocumentType Application = "urn:document:business-registration-application";
                /// <summary>
                /// Заявление о переходе на упрощенную систему налогообложения (форма №26.2-1)
                /// Имя (согласно нормативным документам): Заявление о переходе на УСН
                ///</summary>
                public static readonly DocumentType ApplicationUsn = "urn:document:business-registration-application-usn";
                /// <summary>
                /// Учредительный документ
                /// Имя (согласно нормативным документам): Учредительный документ
                ///</summary>
                public static readonly DocumentType FoundingDocument = "urn:document:business-registration-founding-document";
                /// <summary>
                /// Изменения в учредительный документ
                /// Имя (согласно нормативным документам): Изменения в учредительный документ
                ///</summary>
                public static readonly DocumentType FoundingDocumentCorrection = "urn:document:business-registration-founding-document-correction";
                /// <summary>
                /// Документы, необходимые для государственной регистрации
                /// Имя (согласно нормативным документам): Иные документы
                ///</summary>
                public static readonly DocumentType Attachment = "urn:document:business-registration-attachment";

                /// <summary>
                /// Ответные документы
                /// </summary>
                [PublicAPI]
                public static class Replies
                {
                    /// <summary>
                    /// Протокол с информацией о выявленных несоответствиях форматно-логического контроля
                    /// Имя (согласно нормативным документам): Сообщение о невозможности обработки электронных документов
                    ///</summary>
                    public static readonly DocumentType CheckProtocol = "urn:document:business-registration-check-protocol";

                    /// <summary>
                    /// Расписка о принятии заявления
                    /// Имя (согласно нормативным документам): Расписка в получении налоговым(регистрирующим) органом документов в электронном виде
                    ///</summary>
                    public static readonly DocumentType Receipt = "urn:document:business-registration-receipt";

                    /// <summary>
                    /// Решение об отказе в регистрации
                    /// Имя (согласно нормативным документам): Решение об отказе в государственной регистрации
                    ///</summary>
                    public static readonly DocumentType AcceptanceResultNegative = "urn:document:business-registration-acceptance-result-negative";

                    /// <summary>
                    /// Уведомление о постановке на учёт
                    /// Имя (согласно нормативным документам): Уведомление о постановке на учет в качестве индивидуального предпринимателя в налоговом органе
                    ///</summary>
                    public static readonly DocumentType RegistrationNotice = "urn:document:business-registration-registration-notice";

                    /// <summary>
                    /// Свидетельство о постановке на учет
                    /// Имя (согласно нормативным документам): Свидетельство о постановке на учет в налоговом органе
                    ///</summary>
                    public static readonly DocumentType RegistrationCertificate = "urn:document:business-registration-registration-certificate";

                    /// <summary>
                    /// Уведомление о снятии с учета
                    /// Имя (согласно нормативным документам): Уведомление о снятии с учета в налоговом органе
                    ///</summary>
                    public static readonly DocumentType DeregistrationNotice = "urn:document:business-registration-deregistration-notice";

                    /// <summary>
                    /// Выписка или лист записи ЕГРИП
                    /// Имя (согласно нормативным документам): Выписка или лист записи ЕГРИП
                    ///</summary>
                    public static readonly DocumentType EgripExtract = "urn:document:business-registration-egrip-extract";

                    /// <summary>
                    /// Выписка или лист записи ЕГРЮЛ
                    /// Имя (согласно нормативным документам): Выписка или лист записи ЕГРЮЛ
                    ///</summary>
                    public static readonly DocumentType EgrulExtract = "urn:document:business-registration-egrul-extract";

                    /// <summary>
                    /// Уведомление об оставлении документов без рассмотрения
                    /// Имя (согласно нормативным документам): Уведомление об оставлении документов без рассмотрения
                    ///</summary>
                    public static readonly DocumentType DeclineNotice = "urn:document:business-registration-decline-notice";

                    /// <summary>
                    /// Решение о приостановлении регистрации
                    /// Имя (согласно нормативным документам): Решение о приостановлении государственной регистрации
                    ///</summary>
                    public static readonly DocumentType AcceptanceResultSuspension = "urn:document:business-registration-acceptance-result-suspension";
                }
            }

            /// <summary>
            /// Массовая рассылка от ФНС
            /// </summary>
            [PublicAPI]
            public static class Fns534CuBroadcast
            {
                /// <summary>
                /// Неформализованный текст информационной рассылки налогового органа
                /// Имя (согласно нормативным документам): Рассылка
                ///</summary>
                public static readonly DocumentType Broadcast = "urn:document:fns534-cu-broadcast";

                /// <summary>
                /// Неформализованное приложение к рассылке
                /// Имя (согласно нормативным документам): Приложение
                ///</summary>
                public static readonly DocumentType Attachment = "urn:document:fns534-cu-broadcast-attachment";

                /// <summary>
                /// Подтверждение даты отправки рассылки (установленный формат)
                /// Имя (согласно нормативным документам): ПодтверждениеДатыОтправки
                /// Формирует оператор ЭДО при отправке документооборота.
                ///</summary>
                public static readonly DocumentType DateConfirmation = "urn:document:fns534-cu-broadcast-date-confirmation";

                /// <summary>
                /// Служебный документ, в котором передается описание (установленный формат)
                /// Имя (согласно нормативным документам): Описание
                /// Формирует оператор ЭДО при отправке документооборота.
                ///</summary>
                public static readonly DocumentType Description = "urn:document:fns534-cu-broadcast-description";
            }
        }
    }
}