using Newtonsoft.Json;

namespace Kontur.Extern.Client.Clients.Authentication.Client.Models.Authentication.Responses
{
    /// <summary>
    /// Response from a token endpoint
    /// </summary>
    public class TokenResponse
    {
        /// <summary>
        /// Gets the <b>access_token</b>.
        /// </summary>
        [JsonProperty(ClientConstants.TokenResponse.AccessToken)]
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets the <b>id_token</b>.
        /// </summary>
        [JsonProperty(ClientConstants.TokenResponse.IdentityToken)]
        public string IdToken { get; set; }

        /// <summary>
        /// Gets the <b>refresh_token</b>.
        /// </summary>
        [JsonProperty(ClientConstants.TokenResponse.RefreshToken)]
        public string RefreshToken { get; set; }

        /// <summary>
        /// Gets the <b>token_type</b>
        /// </summary>
        [JsonProperty(ClientConstants.TokenResponse.TokenType)]
        public string TokenType { get; set; }

        /// <summary>
        /// Gets the <b>expires_in</b>.
        /// </summary>
        [JsonProperty(ClientConstants.TokenResponse.ExpiresIn)]
        public int ExpiresIn { get; set; }
    }
}