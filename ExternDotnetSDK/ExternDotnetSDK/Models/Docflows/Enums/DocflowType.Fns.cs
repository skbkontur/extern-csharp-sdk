using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Models.Docflows.Enums
{
    /// <summary>
    /// Типы документооборотов.
    /// </summary>
    [SuppressMessage("ReSharper", "CommentTypo")]
    public partial struct DocflowType
    {
        /// <summary>
        /// ФНС
        /// </summary>
        [PublicAPI]
        public static class Fns
        {
            /// <summary>
            /// Декларация и 2-НДФЛ
            /// </summary>
            public static class Fns534
            {
                /// <summary>
                /// Тип документооборота:
                /// Документооборот по представлению налоговых деклараций (расчетов), бухгалтерской отчетности и сведений о доходах по форме 2-НДФЛ
                /// </summary>
                public static readonly DocflowType Report = "urn:docflow:fns534-report";
        
                /// <summary>
                /// Тип документооборота: ИОН.
                /// Документооборот по информационному обслуживанию абонентов со стороны налоговых органов
                /// </summary>
                public static readonly DocflowType Ion = "urn:docflow:fns534-ion";
        
                /// <summary>
                /// Тип документооборота: Письмо в ФНС
                /// Документооборот по осуществлению письменных обращений абонентов в налоговые органы
                /// </summary>
                public static readonly DocflowType Letter = "urn:docflow:fns534-letter";
        
                /// <summary>
                /// Тип документооборота: Письмо из ФНС
                /// Документооборот по осуществлению индивидуального информирования абонентов со стороны налоговых органов
                /// </summary>
                public static readonly DocflowType CuLetter = "urn:docflow:fns534-cu-letter";
        
                /// <summary>
                /// Тип документооборота: Массовая рассылка от ФНС
                /// Документооборот по осуществлению информационной рассылки со стороны налоговых органов
                /// </summary>
                public static readonly DocflowType CuBroadcast = "urn:docflow:fns534-cu-broadcast";
                
                /// <summary>
                /// Тип документооборота: Представление
                /// Документооборот для представления отдельных документов
                /// </summary>
                public static readonly DocflowType Submission = "urn:docflow:fns534-submission";
                
                /// <summary>
                /// Тип документооборота: Опись - Ответ на требование
                /// Документооборот для представления ответа на требование
                /// </summary>
                public static readonly DocflowType Inventory = "urn:docflow:fns534-inventory";
                
                /// <summary>
                /// Тип документооборота: Документ (Требование)
                /// Документооборот, используемый налоговыми органами при реализации своих полномочий в отношениях, регулируемых законодательством о налогах и сборах
                /// </summary>
                public static readonly DocflowType Demand = "urn:docflow:fns534-demand";
        
                /// <summary>
                /// Тип документооборота: Заявление
                /// Документооборот по представлению в налоговый орган заявления о ввозе товаров и уплате косвенных налогов
                /// </summary>
                public static readonly DocflowType Application = "urn:docflow:fns534-application";
            }
            
            /// <summary>
            /// Тип документооборота: Регистрация бизнеса
            /// Документооборот для представления в налоговый орган документов для регистрации юридического лица, крестьянского (фермерского) хозяйства, физического лица в качестве индивидуального предпринимателя
            /// </summary>
            public static readonly DocflowType BusinessRegistration = "urn:docflow:business-registration";
        }
    }
}