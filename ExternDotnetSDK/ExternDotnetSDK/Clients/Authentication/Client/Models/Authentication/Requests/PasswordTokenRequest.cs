namespace Kontur.Extern.Client.Clients.Authentication.Client.Models.Authentication.Requests
{
    /// <summary>
    /// Запрос для получения токена по средствам связки логин пароль
    /// </summary>
    /// <seealso cref="ClientAuthenticatedRequest" />
    public class PasswordTokenRequest : ClientAuthenticatedRequest
    {
        /// <summary>
        /// Получить или установить логин пользователя
        /// </summary>
        /// <value>
        /// Логин пользователя
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Получить или установить пароль пользователя
        /// </summary>
        /// <value>
        /// Пароль пользователя.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Получить или установить токен для двуфакторной аутентификации
        /// </summary>
        /// <value>
        /// Токен для 2FA.
        /// </value>
        public string PartialFactorToken { get; set; }

        /// <summary>
        /// Получить или установить scope
        /// </summary>
        /// <value>
        /// сфера использования токена
        /// </value>
        public string Scope { get; set; } = "extern.api";
    }
}