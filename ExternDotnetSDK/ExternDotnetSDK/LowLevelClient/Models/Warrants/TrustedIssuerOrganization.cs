using JetBrains.Annotations;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Warrants
{
    [PublicAPI]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
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