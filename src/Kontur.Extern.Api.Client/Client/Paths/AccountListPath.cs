using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Models.Accounts;
using Kontur.Extern.Api.Client.Models.Numbers;
using Kontur.Extern.Api.Client.Primitives;

namespace Kontur.Extern.Api.Client.Paths
{
    [PublicAPI]
    [ClientDocumentationSection]
    public readonly struct AccountListPath
    {
        public AccountListPath(IExternClientServices services) => Services = services ?? throw new ArgumentNullException(nameof(services));

        public IExternClientServices Services { get; }

        public AccountPath WithId(Guid accountId) => new(accountId, Services);
        public HandbooksPath Handbooks => new(Services);

        public Task<Account> CreateIndividualEntrepreneurAccountAsync(Inn inn, string organizationName, TimeSpan? timeout = null)
        {
            if (string.IsNullOrWhiteSpace(organizationName))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(organizationName);
            var apiClient = Services.Api;
            return apiClient.Accounts.CreateAccountAsync(inn.ToString(), null, organizationName, timeout);
        }

        public Task<Account> CreateLegalEntityAccountAsync(LegalEntityInn inn, Kpp kpp, string organizationName, TimeSpan? timeout = null)
        {
            if (string.IsNullOrWhiteSpace(organizationName))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(organizationName);
            var apiClient = Services.Api;
            return apiClient.Accounts.CreateAccountAsync(inn.ToString(), kpp, organizationName, timeout);
        }

        public IEntityList<Account> List()
        {
            var apiClient = Services.Api;
            return new EntityList<Account>(
                async (skip, take, timeout) =>
                {
                    int intSkip;
                    checked
                    {
                        intSkip = (int) skip;
                    }

                    var accountList = await apiClient.Accounts.GetAccountsAsync(intSkip, take, timeout);

                    return (accountList.Accounts, accountList.TotalCount);
                });
        }
    }
}