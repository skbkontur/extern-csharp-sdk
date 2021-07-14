using Newtonsoft.Json;
using static Kontur.Extern.Client.Authentication.OpenId.Client.Models.ContractConstants.Multifactor;

namespace Kontur.Extern.Client.Authentication.OpenId.Client.Models.Responses
{
    public class RequiredFactor
    {
        [JsonProperty(Factors.GrantType)]
        public string GrantType { get; set; }

        [JsonProperty(Factors.Identity)]
        public string Identity { get; set; }
    }
}
