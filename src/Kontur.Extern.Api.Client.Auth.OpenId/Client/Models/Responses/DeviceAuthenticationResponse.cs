using JetBrains.Annotations;
using System.Text.Json.Serialization;

namespace Kontur.Extern.Api.Client.Auth.OpenId.Client.Models.Responses
{
    /// <summary>
    /// Response from a device authentification endpoint
    /// </summary>
    [PublicAPI]
    public class DeviceAuthenticationResponse
    {
        /// <summary>
        /// Gets the <b>device_code</b>.
        /// </summary>
        public string DeviceCode { get; set; }

        /// <summary>
        /// Gets the <b>user_code</b>.
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// Gets the <b>verification_uri</b>.
        /// </summary>
        public string VerificationUri { get; set; }

        /// <summary>
        /// Gets the <b>verification_uri_complete</b>.
        /// </summary>
        public string VerificationUriComplete { get; set; }

        /// <summary>
        /// Gets the <b>interval</b>.
        /// </summary>
        [JsonPropertyName("interval")]
        public int IntervalInSeconds { get; set; }

        /// <summary>
        /// Gets the <b>expires_in</b>.
        /// </summary>
        [JsonPropertyName("expires_in")]
        public int ExpiresInSeconds { get; set; }
    }
}
