namespace Kontur.Extern.Client.Clients.Authentication.Client.Models.Authentication.Requests
{
    /// <summary>
    /// Запрос для получения токенов авторизации при помощи сертификата
    /// </summary>
    /// <seealso cref="ClientAuthenticatedRequest" />
    public class CertificateTokenRequest : ClientAuthenticatedRequest
    {
        /// <summary>
        /// Получить или установить расшифрованный контент
        /// </summary>
        public byte[] DecryptedKey { get; set; }

        /// <summary>
        /// Получить или установить отпечаток сертефиката пользователя
        /// </summary>
        public string Thumbprint { get; set; }

        /// <summary>
        /// Получить или установить scope
        /// </summary>
        /// <value>
        /// сфера использования токена
        /// </value>
        public string Scope { get; set; }
    }
}