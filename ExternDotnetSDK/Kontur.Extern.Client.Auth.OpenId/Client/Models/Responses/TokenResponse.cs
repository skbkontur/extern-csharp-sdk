using Newtonsoft.Json;

namespace Kontur.Extern.Client.Auth.OpenId.Client.Models.Responses
{
    /// <summary>
    /// Response from a token endpoint
    /// </summary>
    public class TokenResponse
    {
        /// <summary>
        /// Gets the <b>access_token</b>.
        /// </summary>
        [JsonProperty(ContractConstants.TokenResponse.AccessToken)]
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets the <b>id_token</b>.
        /// </summary>
        [JsonProperty(ContractConstants.TokenResponse.IdentityToken)]
        public string IdToken { get; set; }

        /// <summary>
        /// Gets the <b>refresh_token</b>.
        /// </summary>
        [JsonProperty(ContractConstants.TokenResponse.RefreshToken)]
        public string RefreshToken { get; set; }

        /// <summary>
        /// Gets the <b>token_type</b>
        /// </summary>
        [JsonProperty(ContractConstants.TokenResponse.TokenType)]
        public string TokenType { get; set; }

        /// <summary>
        /// Gets the <b>expires_in</b>.
        /// </summary>
        [JsonProperty(ContractConstants.TokenResponse.ExpiresIn)]
        public int ExpiresInSeconds { get; set; }
    }
}