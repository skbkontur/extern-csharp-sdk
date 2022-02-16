using System;
using System.Security.Cryptography.X509Certificates;
using Kontur.Extern.Api.Client.Auth.OpenId.Exceptions;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.Auth.OpenId.Client.Models.Requests
{
    /// <summary>
    /// Запрос для аутентификации при помощи сертификата
    /// </summary>
    /// <seealso cref="ClientAuthenticatedRequest" />
    public class CertificateAuthenticationRequest : ClientAuthenticatedRequest
    {
        public CertificateAuthenticationRequest(X509Certificate2 publicKeyCertificate, bool free, string clientId, string clientSecret)
            : base(clientId, clientSecret)
        {
            PublicKeyCertificate = publicKeyCertificate ?? throw new ArgumentNullException(nameof(publicKeyCertificate));
            Free = free;
        }

        public CertificateAuthenticationRequest(X509Certificate2 publicKeyCertificate, bool free, string partialFactorToken, string clientId, string clientSecret)
            : base(clientId, clientSecret)
        {
            if (string.IsNullOrWhiteSpace(partialFactorToken))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(nameof(partialFactorToken));

            PublicKeyCertificate = publicKeyCertificate ?? throw new ArgumentNullException(nameof(publicKeyCertificate));
            Free = free;
            PartialFactorToken = partialFactorToken;
        }

        /// <summary>
        /// Получить или установить публичный ключ
        /// </summary>
        /// <value>
        /// Публичный ключ пользователя
        /// </value>
        public X509Certificate2 PublicKeyCertificate { get; }

        /// <summary>
        /// Получить или установить значение валидации сертификата
        /// </summary>
        /// <value>
        /// Пропускать валидацию сертификата пользователя
        /// </value>
        public bool Free { get; }

        /// <summary>
        /// Получить или установить токен для двуфакторной аутентификации
        /// </summary>
        /// <value>
        /// Токен для 2FA.
        /// </value>
        public string PartialFactorToken { get; }
    }
}
