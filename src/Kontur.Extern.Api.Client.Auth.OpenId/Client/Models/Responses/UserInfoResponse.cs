using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Auth.OpenId.Client.Models.Responses
{
    [PublicAPI]
    public class UserInfoResponse
    {
        public string Sub { get; set; }

        public string GivenName { get; set; }

        public string FamilyName { get; set; }

        public string MiddleName { get; set; }

        public string Name { get; set; }

        public long UpdatedAt { get; set; }
    }
}