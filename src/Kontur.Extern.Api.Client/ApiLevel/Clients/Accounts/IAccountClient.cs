﻿using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Accounts;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Certificates;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Warrants;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Http;
using Kontur.Extern.Api.Client.Models.Accounts;
using Kontur.Extern.Api.Client.Models.Numbers;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.ApiLevel.Clients.Accounts
{
    [PublicAPI]
    [ClientDocumentationSection]
    public interface IAccountClient
    {
        IHttpRequestFactory HttpRequestFactory { get; }

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
        /// Получение учетной записи по ее идентификатору
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="timeout"></param>
        /// <returns>Учетная запись или <code>null</code> если учетная запись с указанным <paramref name="accountId"/> отсутствует.</returns>
        Task<Account?> TryGetAccountAsync(Guid accountId, TimeSpan? timeout = null);

        /// <summary>
        /// Удаление учетной записи
        /// </summary>
        /// <param name="accountId">Идентификатор учетной записи</param>
        /// <param name="timeout"></param>
        /// <returns>Возвращает true, если аккаунт успешно удален; false, если аккаунт не существует.</returns>
        Task<bool> DeleteAccountAsync(Guid accountId, TimeSpan? timeout = null);

        /// <summary>
        /// Создание новой учетной записи
        /// </summary>
        /// <param name="inn">ИНН организации (10 цифр для ЮЛ, 12 цифр для ИП)</param>
        /// <param name="kpp">КПП организации (только для ЮЛ)</param>
        /// <param name="organizationName">Название организации</param>
        /// <param name="timeout"></param>
        /// <returns>Учетная запись</returns>
        Task<Account> CreateAccountAsync(string inn, Kpp? kpp, string organizationName, TimeSpan? timeout = null);

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