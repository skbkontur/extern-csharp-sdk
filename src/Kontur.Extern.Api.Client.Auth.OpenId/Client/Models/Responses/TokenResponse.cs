using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Auth.OpenId.Exceptions;

namespace Kontur.Extern.Api.Client.Auth.OpenId.Client.Models.Responses
{
    /// <summary>
    /// Response from a token endpoint
    /// </summary>
    [PublicAPI]
    public class TokenResponse
    {
        /// <summary>
        /// Gets the <b>access_token</b>.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets the <b>id_token</b>.
        /// </summary>
        public string IdToken { get; set; }

        /// <summary>
        /// Gets the <b>refresh_token</b>.
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Gets the <b>token_type</b>
        /// </summary>
        public string TokenType { get; set; }

        /// <summary>
        /// Gets the <b>expires_in</b>.
        /// </summary>
        [JsonPropertyName("expires_in")]
        public int ExpiresInSeconds { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(AccessToken))
                throw Errors.TokenResponseHasEmptyAccessToken();
            
            if (ExpiresInSeconds <= 0)
                throw Errors.TokenResponseInvalidExpirationSeconds(ExpiresInSeconds);
        }
    }
}