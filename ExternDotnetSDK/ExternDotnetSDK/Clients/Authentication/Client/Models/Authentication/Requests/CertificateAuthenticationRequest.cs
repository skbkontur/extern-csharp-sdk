using System.Security.Cryptography.X509Certificates;

namespace Kontur.Extern.Client.Clients.Authentication.Client.Models.Authentication.Requests
{
    /// <summary>
    /// Запрос для аутентификации при помощи сертификата
    /// </summary>
    /// <seealso cref="ClientAuthenticatedRequest" />
    public class CertificateAuthenticationRequest : ClientAuthenticatedRequest
    {
        /// <summary>
        /// Получить или установить публичный ключ
        /// </summary>
        /// <value>
        /// Публичный ключ пользователя
        /// </value>
        public X509Certificate2 PublicKey { get; set; }

        /// <summary>
        /// Получить или установить значение валидации сертификата
        /// </summary>
        /// <value>
        /// Пропускать валидацию сертификата пользователя
        /// </value>
        public bool Free { get; set; }

        /// <summary>
        /// Получить или установить токен для двуфакторной аутентификации
        /// </summary>
        /// <value>
        /// Токен для 2FA.
        /// </value>
        public string PartialFactorToken { get; set; } = "extern.api";
    }
}
