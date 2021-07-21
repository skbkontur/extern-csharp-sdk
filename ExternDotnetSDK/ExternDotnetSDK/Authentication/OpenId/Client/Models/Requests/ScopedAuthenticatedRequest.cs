using Kontur.Extern.Client.Exceptions;

namespace Kontur.Extern.Client.Authentication.OpenId.Client.Models.Requests
{
    /// <summary>
    /// Базовый запрос с указанием области действия токена 
    /// </summary>
    /// <seealso cref="ClientAuthenticatedRequest" />
    public abstract class ScopedAuthenticatedRequest : ClientAuthenticatedRequest
    {
        protected ScopedAuthenticatedRequest(string scope, string clientId, string clientSecret)
            : base(clientId, clientSecret)
        {
            if (string.IsNullOrWhiteSpace(scope))
                throw Errors.StringShouldNotBeNullOrWhiteSpace(nameof(scope));

            Scope = scope;
        }

        /// <summary>
        /// Определяет область действия токена + его содержимое. Указывается набор скоупов через пробел, порядок не имеет значения.
        /// </summary>
        public string Scope { get; }
    }
}