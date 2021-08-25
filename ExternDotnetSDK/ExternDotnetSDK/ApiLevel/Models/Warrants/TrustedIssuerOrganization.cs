#nullable enable
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.Warrants
{
    [PublicAPI]
    public class TrustedIssuerOrganization : WarrantOrganization
    {
        public string? Address { get; set; }

        public Fio? ChiefFio { get; set; }

        public string? ChiefInn { get; set; }
    }
}