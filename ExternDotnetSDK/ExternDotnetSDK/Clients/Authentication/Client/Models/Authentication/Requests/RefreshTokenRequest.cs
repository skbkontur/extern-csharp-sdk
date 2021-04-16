namespace Kontur.Extern.Client.Clients.Authentication.Client.Models.Authentication.Requests
{
    /// <summary>
    /// Request for token using refresh_token
    /// </summary>
    /// <seealso cref="ClientAuthenticatedRequest" />
    public class RefreshTokenRequest : ClientAuthenticatedRequest
    {
        /// <summary>
        /// Gets or sets the refresh token.
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets the scope.
        /// </summary>
        public string Scope { get; set; }
    }
}