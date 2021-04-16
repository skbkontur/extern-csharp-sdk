namespace Kontur.Extern.Client.Clients.Authentication.Client.Models.Authentication.Requests
{
    /// <summary>
    /// Request for token using certificate
    /// </summary>
    /// <seealso cref="ClientAuthenticatedRequest" />
    public class CertificateTokenRequest : ClientAuthenticatedRequest
    {
        /// <summary>
        /// Gets or sets decrypted content
        /// </summary>
        public byte[] DecryptedKey { get; set; }

        /// <summary>
        /// Gets or sets thumbprint of user certificate
        /// </summary>
        public string Thumbprint { get; set; }

        /// <summary>
        /// Gets or sets the scope
        /// </summary>
        /// <value>
        /// The scope
        /// </value>
        public string Scope { get; set; }
    }
}