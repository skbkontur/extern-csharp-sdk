namespace Kontur.Extern.Client.Clients.Authentication.Client.Models.Authentication.Requests
{
    /// <summary>
    /// Request for token using password
    /// </summary>
    /// <seealso cref="ClientAuthenticatedRequest" />
    public class PasswordTokenRequest : ClientAuthenticatedRequest
    {
        /// <summary>
        /// Gets or sets the name of the user
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        /// <value>
        /// The password
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets partial factor token for 2FA.
        /// </summary>
        public string PartialFactorToken { get; set; }

        /// <summary>
        /// Gets or sets the scope
        /// </summary>
        /// <value>
        /// The scope
        /// </value>
        public string Scope { get; set; } = "extern.api";
    }
}