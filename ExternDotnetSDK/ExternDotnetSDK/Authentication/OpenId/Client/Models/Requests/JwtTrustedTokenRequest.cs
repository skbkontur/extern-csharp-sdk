using Kontur.Extern.Client.Exceptions;

namespace Kontur.Extern.Client.Authentication.OpenId.Client.Models.Requests
{
    /// <summary>
    /// Request for token using trusted grant
    /// </summary>
    /// <seealso cref="ScopedAuthenticatedRequest" />
    public class JwtTrustedTokenRequest : ScopedAuthenticatedRequest
    {
        public JwtTrustedTokenRequest(string jwtToken, string scope, string clientId, string clientSecret)
            : base(scope, clientId, clientSecret)
        {
            if (string.IsNullOrWhiteSpace(jwtToken))
                throw Errors.StringShouldNotBeEmptyOrWhiteSpace(nameof(jwtToken));
            
            Token = jwtToken;
        }
        
        /// <summary>
        /// Gets user jwt token
        /// </summary>
        public string Token { get; }
    }
}