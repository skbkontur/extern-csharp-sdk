using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.ReportsTables;
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
    public Task<FormsList> GetFormsAsync(Guid accountId, Guid organizationId, bool? includeDeleted = false, TimeSpan? timeout = null);

    /// <summary>
    /// Поиск форм с дедлайнами по нескольким организациям
    /// </summary>
    /// <param name="accountId"></param>
    /// <param name="organizationIds"></param>
    /// <param name="dateFrom"></param>
    /// <param name="dateTo"></param>
    /// <param name="includeDeleted"></param>
    /// <param name="skip"></param>
    /// <param name="take"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    public Task<ReportsTableList> GetReportsTablesAsync(
        Guid accountId,
        Guid[]? organizationIds = null,
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        bool? includeDeleted = false,
        int? skip = null,
        int? take = null,
        TimeSpan? timeout = null);
}