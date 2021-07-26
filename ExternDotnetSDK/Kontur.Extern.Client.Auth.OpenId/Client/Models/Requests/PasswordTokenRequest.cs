#nullable enable
using Kontur.Extern.Client.Auth.OpenId.Exceptions;
using Kontur.Extern.Client.Auth.OpenId.Provider.Models;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Auth.OpenId.Client.Models.Requests
{
    /// <summary>
    /// Запрос для получения токена по средствам связки логин пароль
    /// </summary>
    /// <seealso cref="ScopedAuthenticatedRequest" />
    public class PasswordTokenRequest : ScopedAuthenticatedRequest
    {
        public PasswordTokenRequest(Credentials credentials, string scope, string clientId, string clientSecret)
            : this(credentials.UserName, credentials.Password, scope, clientId, clientSecret)
        {
        }
        
        [JsonConstructor]
        public PasswordTokenRequest(string userName, string password, string scope, string clientId, string clientSecret)
            : base(scope, clientId, clientSecret)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(nameof(userName));
            
            if (string.IsNullOrWhiteSpace(password))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(nameof(password));
            
            UserName = userName;
            Password = password;
        }
        
        /// <summary>
        /// Получить или установить логин пользователя
        /// </summary>
        /// <value>
        /// Логин пользователя
        /// </value>
        public string UserName { get; }

        /// <summary>
        /// Получить или установить пароль пользователя
        /// </summary>
        /// <value>
        /// Пароль пользователя.
        /// </value>
        public string Password { get; }

        /// <summary>
        /// Получить или установить токен для двуфакторной аутентификации
        /// </summary>
        /// <value>
        /// Токен для 2FA.
        /// </value>
        public string? PartialFactorToken { get; set; }
    }
}