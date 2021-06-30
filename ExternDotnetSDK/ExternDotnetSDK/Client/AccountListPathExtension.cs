using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Accounts;
using Kontur.Extern.Client.Paths;
using Kontur.Extern.Client.Primitives;

namespace Kontur.Extern.Client
{
    internal static class AccountListPathExtension
    {
        public static Task<Account> CreateAsync(this in AccountListPath path, string inn, string kpp, string organizationName)
        {
            var apiClient = path.Services.Api;
            return apiClient.Accounts.CreateAccountAsync(inn, kpp, organizationName);
        }

        public static Task<List<Account>> FindAsync(this in AccountListPath path, string inn, string kpp) => throw new NotImplementedException();

        public static IEntityList<Account> List(this in AccountListPath path) => throw new NotImplementedException();
    }
}