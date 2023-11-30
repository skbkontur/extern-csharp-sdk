using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.Cryptography
{
    public interface ICrypt
    {
        /// <summary>
        /// Подписание данных
        /// </summary>
        /// <param name="content">Содержимое</param>
        /// <param name="certificateContent">Содержимое сертификата</param>
        /// <returns>Подпись</returns>
        byte[] Sign(byte[] content, byte[] certificateContent);

        /// <summary>
        /// Проверка подписи
        /// </summary>
        /// <param name="content">Подписанные данные</param>
        /// <param name="signatures">Содержимое подписи</param>
        /// <returns>Список сертификатов из подписи</returns>
        /// <exception cref="Exception">бросается в случае неправильной подписи</exception>
        List<byte[]> VerifySignature(byte[] content, byte[] signatures);

        /// <summary>
        /// Расшифровка контента
        /// </summary>
        /// <param name="encryptedContent">Зашифрованные данные</param>
        /// <param name="useLocalSystemStorage">Флаг отвечающий за использование хранилища пользователя или хранилище local_machine </param>
        /// <returns>Расшифрованный контент</returns>
        ///<exception cref="Win32Exception"></exception>
        byte[] Decrypt(byte[] encryptedContent, bool useLocalSystemStorage = false);

        /// <summary>
        /// Получение списка сертификатов из хранилища
        /// </summary>
        /// <param name="onlyWithPrivateKey">Фильтр по наличию у сертификатов приватного ключа</param>
        /// <param name="useLocalSystemStorage">Флаг отвечающий за использование хранилища пользователя или хранилище local_machine </param>
        /// <returns>Список найденных сертификатов</returns>
        List<X509Certificate2> GetPersonalCertificates(bool onlyWithPrivateKey, bool useLocalSystemStorage = false);

        /// <summary>
        /// Получение сертификата по отпечатку
        /// </summary>
        /// <param name="thumbprint">Отпечаток искомого сертификата</param>
        /// <param name="useLocalSystemStorage">Флаг отвечающий за использование для поиска хранилища пользователя или хранилище local_machine </param>
        /// <returns>Найденный сертификат</returns>
        /// <exception cref="Exception">бросается в случае если сертификат не найден</exception>
        X509Certificate2 GetCertificateWithPrivateKey(string thumbprint, bool useLocalSystemStorage = false);

        /// <summary>
        /// Подписание данных сырой подписью
        /// </summary>
        /// <param name="certificate">Данные для подписания</param>
        /// <param name="dataToSign">Сертификат с приватным ключом для подписания</param>
        /// <returns>Подпись</returns>
        byte[] RawSign(byte[] dataToSign, X509Certificate2 certificate);
    }
}
