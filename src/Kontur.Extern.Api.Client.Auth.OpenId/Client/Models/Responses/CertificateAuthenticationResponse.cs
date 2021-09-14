using System.Collections.Generic;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Auth.OpenId.Client.Models.Responses
{
    /// <summary>
    /// Response from a certificate authentication endpoint
    /// </summary>
    [PublicAPI]
    public class CertificateAuthenticationResponse
    {
        /// <summary>
        /// Gets a encrypted content
        /// </summary>
        public byte[] EncryptedKey { get; set; }

        /// <summary>
        /// Gets a a list of thumbprints of trusted certificates
        /// </summary>
        public IList<string> TrustedThumbprints { get; set; }
    }
}
