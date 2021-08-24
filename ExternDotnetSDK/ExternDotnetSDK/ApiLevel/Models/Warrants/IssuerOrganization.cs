using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Warrants
{
    [PublicAPI]
    public class IssuerOrganization
    {
        [CanBeNull]
        public string Name { get; set; }

        [CanBeNull]
        public string Inn { get; set; }

        [CanBeNull]
        public string Kpp { get; set; }
    }
}