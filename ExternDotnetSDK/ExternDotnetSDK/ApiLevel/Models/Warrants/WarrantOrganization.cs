using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Warrants
{
    [PublicAPI]
    public class WarrantOrganization
    {
        [CanBeNull]
        public string Name { get; set; }

        [CanBeNull]
        public string Inn { get; set; }

        [CanBeNull]
        public string Kpp { get; set; }

        [CanBeNull]
        public string Ogrn { get; set; }
    }
}