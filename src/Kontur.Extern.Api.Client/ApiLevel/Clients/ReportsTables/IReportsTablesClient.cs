using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.ReportsTables;
using Kontur.Extern.Api.Client.Models.ReportsTables;

namespace Kontur.Extern.Api.Client.ApiLevel.Clients.ReportsTables;

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
    public Task<FormsResult> GetFormsAsync(Guid accountId, Guid organizationId, bool? includeDeleted = false, TimeSpan? timeout = null);

    /// <summary>
    /// Поиск форм с дедлайнами по нескольким организациям
    /// </summary>
    /// <param name="accountId"></param>
    /// <param name="searchReportsRequest"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    public Task<ReportsTableResult> GetReportsTablesAsync(
        Guid accountId,
        Guid[]? organizationIds = null,
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        bool? includeDeleted = false,
        int? skip = null,
        int? take = null,
        TimeSpan? timeout = null);
}