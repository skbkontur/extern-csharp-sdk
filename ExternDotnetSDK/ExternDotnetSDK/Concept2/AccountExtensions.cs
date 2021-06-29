using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Client.Models.Accounts;
using Kontur.Extern.Client.Models.Certificates;
using Kontur.Extern.Client.Models.Warrants;

namespace Kontur.Extern.Client.Concept2
{
    internal static class AccountListContextExtension
    {
        public static Task<Account> CreateAsync(this in AccountListPath path, string inn, string kpp, string organizationName)
        {
            var apiClient = path.Services.ApiClient;
            return apiClient.Accounts.CreateAccountAsync(inn, kpp, organizationName);
        }

        public static Task<List<Account>> FindAsync(this in AccountListPath path, string inn, string kpp) => throw new NotImplementedException();

        public static IEntityList<Account> List(this in AccountListPath path) => throw new NotImplementedException();
    }

    internal static class AccountContextExtension
    {
        public static Task<Account> GetAsync(this in AccountPath path)
        {
            var apiClient = path.Services.ApiClient;
            return apiClient.Accounts.GetAccountAsync(path.AccountId);
        }
        
        public static IEntityList<CertificateDto> Certificates(this in AccountPath path) => throw new NotImplementedException();
        public static IEntityList<Warrant> Warrants(this in AccountPath path) => throw new NotImplementedException();

        public static Task DeleteAsync(this in AccountPath path)
        {
            var apiClient = path.Services.ApiClient;
            return apiClient.Accounts.DeleteAccountAsync(path.AccountId);
        }
    }
}