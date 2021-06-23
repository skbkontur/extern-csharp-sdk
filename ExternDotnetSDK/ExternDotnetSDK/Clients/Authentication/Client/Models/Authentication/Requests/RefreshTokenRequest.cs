namespace Kontur.Extern.Client.Clients.Authentication.Client.Models.Authentication.Requests
{
    /// <summary>
    /// Запрос для аутентификации при помощи refresh token
    /// </summary>
    /// <seealso cref="ClientAuthenticatedRequest" />
    public class RefreshTokenRequest : ClientAuthenticatedRequest
    {
        /// <summary>
        /// Получить или установить refresh token
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Получить или установить scope
        /// </summary>
        /// <value>
        /// сфера использования токена
        /// </value>
        public string Scope { get; set; }
    }
}