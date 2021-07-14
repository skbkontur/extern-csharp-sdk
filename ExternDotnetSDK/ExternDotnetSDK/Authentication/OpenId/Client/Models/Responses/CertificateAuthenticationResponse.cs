using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Authentication.OpenId.Client.Models.Responses
{
    /// <summary>
    /// Response from a certificate authentication endpoint
    /// </summary>
    public class CertificateAuthenticationResponse
    {
        /// <summary>
        /// Gets a encrypted content
        /// </summary>
        [JsonProperty(ContractConstants.CertificateAuthenticationResponse.EncryptedKey)]
        public byte[] EncryptedKey { get; set; }

        /// <summary>
        /// Gets a a list of thumbprints of trusted certificates
        /// </summary>
        [JsonProperty(ContractConstants.CertificateAuthenticationResponse.TrustedThumbprints)]
        public IList<string> TrustedThumbprints { get; set; }
    }
}
