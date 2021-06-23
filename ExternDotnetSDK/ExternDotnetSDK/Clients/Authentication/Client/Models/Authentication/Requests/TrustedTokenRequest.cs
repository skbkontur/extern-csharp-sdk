namespace Kontur.Extern.Client.Clients.Authentication.Client.Models.Authentication.Requests
{
    /// <summary>
    /// Request for token using trusted grant
    /// </summary>
    /// <seealso cref="ClientAuthenticatedRequest" />
    public class TrustedTokenRequest : ClientAuthenticatedRequest
    {
        /// <summary>
        /// Gets or sets user jwt token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the scope
        /// </summary>
        /// <value>
        /// The scope
        /// </value>
        public string Scope { get; set; }
    }
}