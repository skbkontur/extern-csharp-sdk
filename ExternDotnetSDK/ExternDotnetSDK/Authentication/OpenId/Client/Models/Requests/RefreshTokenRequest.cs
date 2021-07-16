using System;
using Kontur.Extern.Client.Exceptions;

namespace Kontur.Extern.Client.Authentication.OpenId.Client.Models.Requests
{
    /// <summary>
    /// Запрос для аутентификации при помощи refresh token
    /// </summary>
    /// <seealso cref="ClientAuthenticatedRequest" />
    public class RefreshTokenRequest : ClientAuthenticatedRequest
    {
        public RefreshTokenRequest()
        {
        }

        public RefreshTokenRequest(string refreshToken, string clientId)
        {
            if (string.IsNullOrWhiteSpace(clientId))
                throw Errors.StringShouldNotBeEmptyOrWhiteSpace(nameof(clientId));

            if (string.IsNullOrWhiteSpace(refreshToken))
                throw Errors.StringShouldNotBeEmptyOrWhiteSpace(nameof(refreshToken));
            
            RefreshToken = refreshToken;
            ClientId = clientId;
        }
        
        /// <summary>
        /// Получить или установить refresh token
        /// </summary>
        public string RefreshToken { get; set; }
        public string ClientId { get; set; }

        /// <summary>
        /// Получить или установить scope
        /// </summary>
        /// <value>
        /// сфера использования токена
        /// </value>
        public string Scope { get; set; }
    }
}