using System;
using JetBrains.Annotations;
using Kontur.Extern.Client.Auth.OpenId.Exceptions;
// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.Auth.OpenId.Client.Models.Requests
{
    /// <summary>
    /// Запрос для получения токенов авторизации при помощи сертификата
    /// </summary>
    /// <seealso cref="ScopedAuthenticatedRequest" />
    public class CertificateTokenRequest : ScopedAuthenticatedRequest
    {
        public CertificateTokenRequest([NotNull] byte[] decryptedKey, string thumbprint, string scope, string clientId, string clientSecret)
            : base(scope, clientId, clientSecret)
        {
            if (decryptedKey == null)
                throw new ArgumentNullException(nameof(decryptedKey));
            
            if (decryptedKey.Length == 0)
                throw Errors.ArrayCannotBeEmpty(nameof(decryptedKey));

            if (string.IsNullOrWhiteSpace(thumbprint))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(nameof(thumbprint));

            DecryptedKey = decryptedKey;
            Thumbprint = thumbprint;
        }
        
        /// <summary>
        /// Получить или установить расшифрованный контент
        /// </summary>
        public byte[] DecryptedKey { get; }

        /// <summary>
        /// Получить или установить отпечаток сертефиката пользователя
        /// </summary>
        public string Thumbprint { get; }
    }
}