using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Accounts;
using Kontur.Extern.Client.ApiLevel.Models.Certificates;
using Kontur.Extern.Client.ApiLevel.Models.Warrants;

namespace Kontur.Extern.Client.ApiLevel.Clients.Accounts
{
    public interface IAccountClient
    {
        /// <summary>
        /// Получение списка учетных записей пользователя
        /// </summary>
        /// <param name="skip">Количество пропускаемых записей</param>
        /// <param name="take">Количество возвращаемых записей</param>
        /// <param name="timeout"></param>
        /// <returns>Список учетных записей</returns>
        Task<AccountList> GetAccountsAsync(int? skip = null, int? take = null, TimeSpan? timeout = null);

        /// <summary>
        /// Получение учетной записи по ее идентификатору
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="timeout"></param>
        /// <returns>Учетная запись</returns>
        Task<Account> GetAccountAsync(Guid accountId, TimeSpan? timeout = null);

        /// <summary>
        /// Удаление учетной записи
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="timeout"></param>
        Task DeleteAccountAsync(Guid accountId, TimeSpan? timeout = null);

        /// <summary>
        /// Создание новой учетной записи
        /// </summary>
        /// <param name="inn">ИНН организации</param>
        /// <param name="kpp">КПП организации</param>
        /// <param name="organizationName">Название организации</param>
        /// <param name="timeout"></param>
        /// <returns>Учетная запись</returns>
        Task<Account> CreateAccountAsync(string inn, string kpp, string organizationName, TimeSpan? timeout = null);

        /// <summary>
        /// Получение списка сертификатов
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="skip">Количество пропускаемых записей</param>
        /// <param name="take">Количество возвращаемых записей</param>
        /// <param name="forAllUsers">Получить сертификаты всех пользователей, которые имеют доступ к указанной учетной записи (только для администратора)</param>
        /// <param name="timeout"></param>
        /// <returns>Список сертификатов пользователя</returns>
        Task<CertificateList> GetAccountCertificatesAsync(
            Guid accountId,
            int? skip = null,
            int? take = null,
            bool? forAllUsers = null,
            TimeSpan? timeout = null);

        /// <summary>
        /// Получение списка доверенностей
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="skip">Количество пропускаемых записей</param>
        /// <param name="take">Количество возвращаемых записей</param>
        /// <param name="forAllUsers">Получить доверенности всех пользователей (только для администратора)</param>
        /// <param name="timeout"></param>
        /// <returns>Список доверенностей пользователя</returns>
        Task<WarrantList> GetAccountWarrantsAsync(
            Guid accountId,
            int? skip = null,
            int? take = null,
            bool? forAllUsers = null,
            TimeSpan? timeout = null);
    }
}