using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Accounts;
using Kontur.Extern.Client.Paths;
using Kontur.Extern.Client.Primitives;

namespace Kontur.Extern.Client
{
    [PublicAPI]
    public static class AccountListPathExtension
    {
        public static Task<Account> CreateAsync(this in AccountListPath path, string inn, string kpp, string organizationName)
        {
            var apiClient = path.Services.Api;
            return apiClient.Accounts.CreateAccountAsync(inn, kpp, organizationName);
        }

        public static IEntityList<Account> List(this in AccountListPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return new EntityList<Account>(
                async (skip, take) =>
                {
                    int intSkip;
                    int intTake;
                    checked
                    {
                        intSkip = (int) skip;
                        intTake = (int) take;
                    }

                    var accountList = await apiClient.Accounts.GetAccountsAsync(intSkip, intTake, timeout);

                    return (accountList.Accounts, accountList.TotalCount);
                });
        }
    }
}