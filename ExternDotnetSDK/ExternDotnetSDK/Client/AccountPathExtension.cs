#nullable enable
using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Accounts;
using Kontur.Extern.Client.ApiLevel.Models.Certificates;
using Kontur.Extern.Client.ApiLevel.Models.Warrants;
using Kontur.Extern.Client.Paths;
using Kontur.Extern.Client.Primitives;
// ReSharper disable CommentTypo

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

        /// <summary>
        /// Получение списка сертификатов выбранного аккаунта
        /// </summary>
        /// <param name="path"></param>
        /// <param name="forAllUsers">Получить сертификаты всех пользователей, которые имеют доступ к указанной учетной записи (только для администратора)</param>
        /// <returns></returns>
        public static IEntityList<CertificateDto> Certificates(this in AccountPath path, bool? forAllUsers = null)
        {
            var apiClient = path.Services.Api;
            var accountId = path.AccountId;
            
            return new EntityList<CertificateDto>(
                async (skip, take, timeout) =>
                {
                    int intSkip;
                    int intTake;
                    checked
                    {
                        intSkip = (int) skip;
                        intTake = (int) take;
                    }

                    var certificateList = await apiClient.Accounts.GetAccountCertificatesAsync(accountId, intSkip, intTake, forAllUsers, timeout);

                    return (certificateList.Certificates, certificateList.TotalCount);
                });    
        }
        
        public static IEntityList<Warrant> Warrants(this in AccountPath path) => throw new NotImplementedException();

        public static Task DeleteAsync(this in AccountPath path)
        {
            var apiClient = path.Services.Api;
            return apiClient.Accounts.DeleteAccountAsync(path.AccountId);
        }
    }
}