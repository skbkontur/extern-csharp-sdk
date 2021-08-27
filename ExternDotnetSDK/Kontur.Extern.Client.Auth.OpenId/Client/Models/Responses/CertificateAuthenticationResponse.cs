using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Kontur.Extern.Client.Auth.OpenId.Client.Models.Responses
{
    /// <summary>
    /// Response from a certificate authentication endpoint
    /// </summary>
    public class CertificateAuthenticationResponse
    {
        /// <summary>
        /// Gets a encrypted content
        /// </summary>
        [JsonPropertyName(ContractConstants.CertificateAuthenticationResponse.EncryptedKey)]
        public byte[] EncryptedKey { get; set; }

        /// <summary>
        /// Gets a a list of thumbprints of trusted certificates
        /// </summary>
        [JsonPropertyName(ContractConstants.CertificateAuthenticationResponse.TrustedThumbprints)]
        public IList<string> TrustedThumbprints { get; set; }
    }
}
