using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Events;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Models.Accounts;
using Kontur.Extern.Api.Client.Models.Certificates;
using Kontur.Extern.Api.Client.Models.Warrants;
using Kontur.Extern.Api.Client.Primitives;

namespace Kontur.Extern.Api.Client.Paths
{
    [PublicAPI]
    [ClientDocumentationSection]
    public readonly struct AccountPath
    {
        public AccountPath(Guid accountId, IExternClientServices services)
        {
            AccountId = accountId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }

        public IExternClientServices Services { get; }

        public OrganizationListPath Organizations => new(AccountId, Services);
        public DocflowListPath Docflows => new(AccountId, Services);
        public DraftListPath Drafts => new(AccountId, Services);
        public DraftBuilderListPath DraftBuilders => new(AccountId, Services);
        public ContentsPath Contents => new(AccountId, Services);
        public ReportsTableListPath ReportsTables => new(AccountId, Services);
        public EventsPath Events => new(AccountId, Services);

        public Task<Account> GetAsync(TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.Accounts.GetAccountAsync(AccountId, timeout);
        }

        public Task<Account?> TryGetAsync(TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.Accounts.TryGetAccountAsync(AccountId, timeout);
        }

        /// <summary>
        /// Получение списка сертификатов выбранного аккаунта
        /// </summary>
        /// <param name="path"></param>
        /// <param name="forAllUsers">Получить сертификаты всех пользователей, которые имеют доступ к указанной учетной записи (только для администратора)</param>
        /// <returns></returns>
        public IEntityList<Certificate> Certificates(bool? forAllUsers = null)
        {
            var apiClient = Services.Api;
            var accountId = AccountId;

            return new EntityList<Certificate>(
                async (skip, take, timeout) =>
                {
                    int intSkip;
                    checked
                    {
                        intSkip = (int) skip;
                    }

                    var certificateList = await apiClient.Accounts.GetAccountCertificatesAsync(accountId, intSkip, take, forAllUsers, timeout);

                    return (certificateList.Certificates, certificateList.TotalCount);
                });
        }

        public IEntityList<Warrant> Warrants() => throw new NotImplementedException();

        public Task ShareAccountEventsAsync(ShareEventsRequest shareEventsRequest, TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.Events.ShareEventsAsync(AccountId, shareEventsRequest, timeout);
        }

        public Task<bool> DeleteAsync(TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.Accounts.DeleteAccountAsync(AccountId, timeout);
        }
    }
}