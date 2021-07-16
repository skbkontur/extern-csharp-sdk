using Kontur.Extern.Client.Exceptions;

namespace Kontur.Extern.Client.Authentication.OpenId.Client.Models.Requests
{
    /// <summary>
    /// Запрос для аутентификации при помощи refresh token
    /// </summary>
    /// <seealso cref="ScopedAuthenticatedRequest" />
    public class RefreshTokenRequest : ScopedAuthenticatedRequest
    {
        public RefreshTokenRequest(string refreshToken, string clientId, string clientSecret, string scope)
            : base(scope, clientId, clientSecret)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
                throw Errors.StringShouldNotBeEmptyOrWhiteSpace(nameof(refreshToken));
            
            RefreshToken = refreshToken;
        }
        
        /// <summary>
        /// Получить или установить refresh token
        /// </summary>
        public string RefreshToken { get; }
    }
}