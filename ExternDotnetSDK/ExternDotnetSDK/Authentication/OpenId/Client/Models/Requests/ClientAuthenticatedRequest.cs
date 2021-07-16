using Kontur.Extern.Client.Exceptions;

namespace Kontur.Extern.Client.Authentication.OpenId.Client.Models.Requests
{
    /// <summary>
    /// Базовый запрос для аутентификации
    /// </summary>
    public abstract class ClientAuthenticatedRequest
    {
        protected ClientAuthenticatedRequest(string clientId, string clientSecret)
        {
            if (string.IsNullOrWhiteSpace(clientId))
                throw Errors.StringShouldNotBeEmptyOrWhiteSpace(nameof(clientId));

            if (string.IsNullOrWhiteSpace(clientSecret))
                throw Errors.StringShouldNotBeEmptyOrWhiteSpace(nameof(clientSecret));
            
            ClientId = clientId;
            ClientSecret = clientSecret;
        }
        
        /// <summary>
        /// Получить или установить идентификатор клиента
        /// </summary>
        public string ClientId { get; }

        /// <summary>
        /// Получить или установить секрет клиента
        /// </summary>
        /// <value>
        /// ApiKey
        /// </value>
        public string ClientSecret { get; }
    }
}