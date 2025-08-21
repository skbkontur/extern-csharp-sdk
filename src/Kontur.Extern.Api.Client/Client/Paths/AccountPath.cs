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
    [ApiPathSection]
    public readonly struct AccountPath
    {
        public AccountPath(Guid accountId, IExternClientServices services)
        {
            AccountId = accountId;
            this.services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }

        private readonly IExternClientServices services;

        #region ObsoleteCode
        [Obsolete($"Use {nameof(IExtern)}.{nameof(IExtern.Services)} instead")]
        public IExternClientServices Services => services;
        #endregion

        public OrganizationListPath Organizations => new(AccountId, services);
        public DocflowListPath Docflows => new(AccountId, services);
        public DraftListPath Drafts => new(AccountId, services);
        public DraftBuilderListPath DraftBuilders => new(AccountId, services);
        public ContentsPath Contents => new(AccountId, services);
        public ReportsTableListPath ReportsTables => new(AccountId, services);
        public EventsPath Events => new(AccountId, services);

        public Task<Account> GetAsync(TimeSpan? timeout = null)
        {
            var apiClient = services.Api;
            return apiClient.Accounts.GetAccountAsync(AccountId, timeout);
        }

        public Task<Account?> TryGetAsync(TimeSpan? timeout = null)
        {
            var apiClient = services.Api;
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
            var apiClient = services.Api;
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
            var apiClient = services.Api;
            return apiClient.Events.ShareEventsAsync(AccountId, shareEventsRequest, timeout);
        }

        public Task<bool> DeleteAsync(TimeSpan? timeout = null)
        {
            var apiClient = services.Api;
            return apiClient.Accounts.DeleteAccountAsync(AccountId, timeout);
        }
    }
}