using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Auth.Abstractions
{
    [PublicAPI]
    public class UserInfo
    {
        public string Sub { get; set; }

        public string GivenName { get; set; }

        public string FamilyName { get; set; }

        public string MiddleName { get; set; }

        public string Name { get; set; }
    }
}
