using System;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDK.Clients.Common;
using ExternDotnetSDK.Logging;
using ExternDotnetSDK.Models.Organizations;
using Refit;

namespace ExternDotnetSDK.Clients.Organizations
{
    public class OrganizationsClient : InnerCommonClient, IOrganizationsClient
    {
        public OrganizationsClient(ILogError logError, HttpClient client)
            : base(logError, client) =>
            ClientRefit = RestService.For<IOrganizationClientRefit>(client);

        public IOrganizationClientRefit ClientRefit { get; }

        public async Task<OrganizationBatch> GetAllOrganizationsAsync(
            Guid accountId,
            string inn = null,
            string kpp = null,
            int skip = 0,
            int take = 1000) => await TryExecuteTask(ClientRefit.GetAllOrganizationsAsync(accountId, inn, kpp, skip, take));

        public async Task<Organization> GetOrganizationAsync(Guid accountId, Guid orgId) =>
            await TryExecuteTask(ClientRefit.GetOrganizationAsync(accountId, orgId));

        public async Task<Organization> UpdateOrganizationAsync(Guid accountId, Guid orgId, string newName) =>
            await TryExecuteTask(
                ClientRefit.UpdateOrganizationAsync(accountId, orgId, new UpdateOrganizationRequestDto {Name = newName}));

        public async Task<Organization> CreateOrganizationAsync(Guid accountId, string inn, string kpp, string name) =>
            await TryExecuteTask(
                ClientRefit.CreateOrganizationAsync(
                    accountId,
                    new CreateOrganizationRequestDto
                    {
                        Inn = inn,
                        Kpp = kpp,
                        Name = name
                    }));

        public async Task DeleteOrganizationAsync(Guid accountId, Guid orgId) =>
            await TryExecuteTask(ClientRefit.DeleteOrganizationAsync(accountId, orgId));
    }
}