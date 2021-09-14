using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Auth.OpenId.Client.Models.Responses
{
    [PublicAPI]
    public class RequiredFactor
    {
        public string GrantType { get; set; }
        public string Identity { get; set; }
    }
}
