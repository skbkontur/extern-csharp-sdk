// ReSharper disable InconsistentNaming
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Drafts.Documents
{
    /// <summary>
    /// Тип формируемого документа
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public enum DocumentFormatType
    {
        /// <summary>
        /// Декларация по упрощенной системе налогообложения по ставке 6% или 15%
        /// </summary>
        USN,

        /// <summary>
        /// Cправка о состоянии расчетов по налогам, сборам, страховым взносам, пеням, штрафам, процентам
        /// </summary>
        ION1,

        /// <summary>
        /// Выписка операций по расчетам с бюджетом
        /// </summary>
        ION2,

        /// <summary>
        /// Перечень налоговых деклараций (расчетов) и бухгалтерской отчетности
        /// </summary>
        ION3,

        /// <summary>
        /// Акт совместной сверки расчетов по  налогам, сборам, страховым взносам, пеням, штрафам, процентам
        /// </summary>
        ION4,

        /// <summary>
        /// Cправка об исполнении налогоплательщиком (плательщиком сбора, плательщиком страховых взносов, налоговым агентом) обязанности по уплате налогов, сборов, страховых взносов, пеней, штрафов, процентов
        /// </summary>
        ION5,

        /// <summary>
        /// Заявление на подключение к системе электронного документооборота ПФР
        /// </summary>
        ZPED,

        /// <summary>
        /// Подписка оператора на организацию по РНС в ФСС
        /// </summary>
        FssSedoProviderSubscriptionForRegistrationNumber,

        /// <summary>
        /// Подписка страхователя по РНС в ФСС
        /// </summary>
        FssSedoAbonentSubscriptionForRegistrationNumber,

        /// <summary>
        /// Подписка страхователя по сотруднику в ФСС
        /// </summary>
        FssSedoAbonentSubscriptionForSnils
    }
}