#nullable enable
using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Accounts;
using Kontur.Extern.Client.ApiLevel.Models.Certificates;
using Kontur.Extern.Client.ApiLevel.Models.Warrants;
using Kontur.Extern.Client.Paths;
using Kontur.Extern.Client.Primitives;

namespace Kontur.Extern.Client
{
    [PublicAPI]
    public static class AccountPathExtension
    {
        public static Task<Account> GetAsync(this in AccountPath path)
        {
            var apiClient = path.Services.Api;
            return apiClient.Accounts.GetAccountAsync(path.AccountId);
        }
        
        public static Task<Account?> TryGetAsync(this in AccountPath path)
        {
            var apiClient = path.Services.Api;
            return apiClient.Accounts.TryGetAccountAsync(path.AccountId);
        }
        
        public static IEntityList<CertificateDto> Certificates(this in AccountPath path) => throw new NotImplementedException();
        public static IEntityList<Warrant> Warrants(this in AccountPath path) => throw new NotImplementedException();

        public static Task DeleteAsync(this in AccountPath path)
        {
            var apiClient = path.Services.Api;
            return apiClient.Accounts.DeleteAccountAsync(path.AccountId);
        }
    }
}