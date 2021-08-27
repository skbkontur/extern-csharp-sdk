using System.Text.Json.Serialization;
using static Kontur.Extern.Client.Auth.OpenId.Client.Models.ContractConstants.Multifactor;

namespace Kontur.Extern.Client.Auth.OpenId.Client.Models.Responses
{
    public class RequiredFactor
    {
        [JsonPropertyName(Factors.GrantType)]
        public string GrantType { get; set; }

        [JsonPropertyName(Factors.Identity)]
        public string Identity { get; set; }
    }
}
