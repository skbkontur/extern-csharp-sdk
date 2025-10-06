using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.ReportsTables;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Models.ReportsTables.Reports;

namespace Kontur.Extern.Api.Client.ApiLevel.Clients.ReportsTables;

[PublicAPI]
[ClientDocumentationSection]
public interface IReportsTablesClient
{
    /// <summary>
    /// Получение списка форм отчетности для организации
    /// </summary>
    /// <param name="accountId">Идентификатор учетной записи</param>
    /// <param name="organizationId">Идентификатор организации</param>
    /// <param name="includeDeleted">Флаг отображения удаленных форм отчетности</param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    public Task<FormsList> GetFormsAsync(Guid accountId, Guid organizationId, bool? includeDeleted = false, TimeSpan? timeout = null);

    /// <summary>
    /// Получение списка платежей для организации
    /// </summary>
    /// <param name="accountId">Идентификатор учетной записи</param>
    /// <param name="organizationIds">Идентификаторы организаций</param>
    /// <param name="deadlineFrom">Начальная дата крайнего срока платежей</param>
    /// <param name="deadlineTo">Конечная дата крайнего срока платежа</param>
    /// <param name="skip">Количество пропускаемых организаций</param>
    /// <param name="take">Количество возвращаемых организаций</param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    public Task<PaymentsList> GetPaymentsAsync(
        Guid accountId,
        Guid[]? organizationIds = null,
        DateTime? deadlineFrom = null,
        DateTime? deadlineTo = null,
        int? skip = null,
        int? take = null,
        TimeSpan? timeout = null);

    /// <summary>
    /// Поиск форм с дедлайнами по нескольким организациям
    /// </summary>
    /// <param name="accountId"></param>
    /// <param name="organizationIds"></param>
    /// <param name="dateFrom"></param>
    /// <param name="dateTo"></param>
    /// <param name="skip"></param>
    /// <param name="take"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    public Task<ReportsTableList> GetReportsTablesAsync(
        Guid accountId,
        Guid[]? organizationIds = null,
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        int? skip = null,
        int? take = null,
        TimeSpan? timeout = null);

    /// <summary>
    /// Детализация по форме отчетности
    /// </summary>
    /// <param name="accountId">Идентификатор учетной записи</param>
    /// <param name="organizationId">Идентификатор организации</param>
    /// <param name="formId">Идентификатор формы отчетности</param>
    /// <param name="deadline">Крайний срок подачи формы отчетности, дедлайн</param>
    /// <param name="periodYear">Год подачи формы отчетности</param>
    /// <param name="periodNumber">Номер периода подачи формы отчетности</param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    public Task<ReportsTableDocflows> GetReportDocflowsAsync(
        Guid accountId,
        Guid organizationId,
        int formId,
        DateTime deadline,
        int periodYear,
        int periodNumber,
        TimeSpan? timeout = null);

}