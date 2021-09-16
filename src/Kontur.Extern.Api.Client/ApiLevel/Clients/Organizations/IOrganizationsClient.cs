using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Organizations;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.ApiLevel.Clients.Organizations
{
    [SuppressMessage("ReSharper", "CommentTypo")]
    public interface IOrganizationsClient
    {
        Task<OrganizationBatch> GetAllOrganizationsAsync(
            Guid accountId,
            string? inn = null,
            string? kpp = null,
            int skip = 0,
            int take = 1000,
            TimeSpan? timeout = null);

        Task<Organization> GetOrganizationAsync(Guid accountId, Guid orgId, TimeSpan? timeout = null);
        Task<Organization?> TryGetOrganizationAsync(Guid accountId, Guid orgId, TimeSpan? timeout = null);
        Task<Organization> UpdateOrganizationAsync(Guid accountId, Guid orgId, string newName, TimeSpan? timeout = null);
        Task<Organization> CreateOrganizationAsync(Guid accountId, string inn, Kpp? kpp, string name, TimeSpan? timeout = null);
        
        /// <summary>
        /// Удаление организации
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="orgId">Идентификтор органзации</param>
        /// <param name="timeout"></param>
        /// <returns>Возвращает true, если организация успешно удалена; false, если организации не существует.</returns>
        Task<bool> DeleteOrganizationAsync(Guid accountId, Guid orgId, TimeSpan? timeout = null);
    }
}