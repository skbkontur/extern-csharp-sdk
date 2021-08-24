using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.Warrants
{
    [PublicAPI]
    public class TrustedIssuerOrganization : WarrantOrganization
    {
        [CanBeNull]
        public string Address { get; set; }

        [CanBeNull]
        public Fio ChiefFio { get; set; }

        [CanBeNull]
        public string ChiefInn { get; set; }
    }
}