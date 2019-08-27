using JetBrains.Annotations;
using KeApiOpenSdk.Models.Common;
using KeApiOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiOpenSdk.Models.Warrants
{
    [PublicAPI]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class Notary
    {
        [CanBeNull]
        public Fio Fio { get; set; }

        [CanBeNull]
        public string Inn { get; set; }

        [CanBeNull]
        public string Address { get; set; }

        [CanBeNull]
        public WarrantOrganization NotaryOrganization { get; set; }
    }
}