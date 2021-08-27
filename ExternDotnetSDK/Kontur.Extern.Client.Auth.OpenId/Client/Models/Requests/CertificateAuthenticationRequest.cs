using System;
using System.Security.Cryptography.X509Certificates;
using Kontur.Extern.Client.Auth.OpenId.Exceptions;
// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.Auth.OpenId.Client.Models.Requests
{
    /// <summary>
    /// Запрос для аутентификации при помощи сертификата
    /// </summary>
    /// <seealso cref="ClientAuthenticatedRequest" />
    public class CertificateAuthenticationRequest : ClientAuthenticatedRequest
    {
        public CertificateAuthenticationRequest(X509Certificate2 publicKey, bool free, string partialFactorToken, string clientId, string clientSecret)
            : base(clientId, clientSecret)
        {
            if (string.IsNullOrWhiteSpace(partialFactorToken))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(nameof(partialFactorToken));
            
            PublicKey = publicKey ?? throw new ArgumentNullException(nameof(publicKey));
            Free = free;
            PartialFactorToken = partialFactorToken;
        }

        /// <summary>
        /// Получить или установить публичный ключ
        /// </summary>
        /// <value>
        /// Публичный ключ пользователя
        /// </value>
        public X509Certificate2 PublicKey { get; }

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
