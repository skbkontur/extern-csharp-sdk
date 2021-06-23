using System.Collections.Generic;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Clients.Authentication.Client.Models.Authentication.Responses
{
    /// <summary>
    /// Response from a certificate authentication endpoint
    /// </summary>
    public class CertificateAuthenticationResponse
    {
        /// <summary>
        /// Gets a encrypted content
        /// </summary>
        [JsonProperty(ClientConstants.CertificateAuthenticationResponse.EncryptedKey)]
        public byte[] EncryptedKey { get; set; }

        /// <summary>
        /// Gets a a list of thumbprints of trusted certificates
        /// </summary>
        [JsonProperty(ClientConstants.CertificateAuthenticationResponse.TrustedThumbprints)]
        public IList<string> TrustedThumbprints { get; set; }
    }
}
