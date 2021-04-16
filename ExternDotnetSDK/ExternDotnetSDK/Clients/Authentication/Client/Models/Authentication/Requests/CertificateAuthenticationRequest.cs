using System.Security.Cryptography.X509Certificates;

namespace Kontur.Extern.Client.Clients.Authentication.Client.Models.Authentication.Requests
{
    /// <summary>
    /// Authentication request for certificate
    /// </summary>
    /// <seealso cref="ClientAuthenticatedRequest" />
    public class CertificateAuthenticationRequest : ClientAuthenticatedRequest
    {
        /// <summary>
        /// Gets or sets public key.
        /// </summary>
        /// <value>
        /// User public key for authentication.
        /// </value>
        public X509Certificate2 PublicKey { get; set; }

        /// <summary>
        /// Gets or sets validate certificate.
        /// </summary>
        /// <value>
        /// Skip validation user certificate.
        /// </value>
        public bool Free { get; set; }

        /// <summary>
        /// Gets or sets partial factor token.
        /// </summary>
        /// <value>
        /// Token for process 2FA.
        /// </value>
        public string PartialFactorToken { get; set; }
    }
}
