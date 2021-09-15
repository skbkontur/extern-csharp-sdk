using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Models.Accounts;
using Kontur.Extern.Api.Client.Models.Numbers;
using Kontur.Extern.Api.Client.Paths;
using Kontur.Extern.Api.Client.Primitives;

namespace Kontur.Extern.Api.Client
{
    [PublicAPI]
    public static class AccountListPathExtension
    {
        public static Task<Account> CreateIndividualEntrepreneurAccountAsync(this in AccountListPath path, Inn inn, string organizationName)
        {
            if (string.IsNullOrWhiteSpace(organizationName))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(organizationName);
            var apiClient = path.Services.Api;
            return apiClient.Accounts.CreateAccountAsync(inn.ToString(), null, organizationName);
        }
        
        public static Task<Account> CreateLegalEntityAccountAsync(this in AccountListPath path, LegalEntityInn inn, Kpp kpp, string organizationName)
        {
            if (string.IsNullOrWhiteSpace(organizationName))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(organizationName);
            var apiClient = path.Services.Api;
            return apiClient.Accounts.CreateAccountAsync(inn.ToString(), kpp.ToString(), organizationName);
        }

        public static IEntityList<Account> List(this in AccountListPath path)
        {
            var apiClient = path.Services.Api;
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