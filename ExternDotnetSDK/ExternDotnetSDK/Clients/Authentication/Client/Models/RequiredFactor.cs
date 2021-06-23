using Newtonsoft.Json;
using static Kontur.Extern.Client.Clients.Authentication.Client.Models.ClientConstants.Multifactor;

namespace Kontur.Extern.Client.Clients.Authentication.Client.Models
{
    public class RequiredFactor
    {
        [JsonProperty(Factors.GrantType)]
        public string GrantType { get; set; }

        [JsonProperty(Factors.Identity)]
        public string Identity { get; set; }
    }
}
