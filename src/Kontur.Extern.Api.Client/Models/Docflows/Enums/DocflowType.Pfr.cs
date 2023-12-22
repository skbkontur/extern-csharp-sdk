using System.Diagnostics.CodeAnalysis;

namespace Kontur.Extern.Api.Client.Models.Docflows.Enums
{
    [SuppressMessage("ReSharper", "CommentTypo")]
    public partial struct DocflowType
    {
        /// <summary>
        /// Тип документооборота: Сведения ПФР, отчетность по форме СЗВ-ТД
        /// Основной документооборот по обмену электронными документами Абонентов с органами ПФР
        /// </summary>
        public static readonly DocflowType PfrReport = "urn:docflow:pfr-report";

        /// <summary>
        /// Тип документооборота: Письмо в ПФР
        /// Документооборот неформализованной переписки Абонента и органа ПФР
        /// </summary>
        public static readonly DocflowType PfrLetter = "urn:docflow:pfr-letter";

        /// <summary>
        /// Тип документооборота: Письмо из ПФР
        /// Документооборот неформализованной переписки Абонента и органа ПФР
        /// </summary>
        public static readonly DocflowType PfrCuLetter = "urn:docflow:pfr-cu-letter";

        /// <summary>
        /// Тип документооборота: Уточнение платежей
        /// Документооборот по проверке правильности и полноты уплаты Абонентом страховых взносов в орган ПФР
        /// </summary>
        public static readonly DocflowType PfrIos = "urn:docflow:pfr-ios";

        /// <summary>
        /// Тип документооборота: Служебный документ ПФР
        /// Заявление на подключение к системе электронного документооборота ПФР
        /// </summary>
        public static readonly DocflowType PfrAncillary = "urn:docflow:pfr-ancillary";
    }
}