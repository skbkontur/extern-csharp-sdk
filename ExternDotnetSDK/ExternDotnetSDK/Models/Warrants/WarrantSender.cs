using JetBrains.Annotations;
using KeApiOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiOpenSdk.Models.Warrants
{
    [PublicAPI]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class WarrantSender
    {
        [CanBeNull]
        public WarrantIndividual SenderIndividual { get; set; }

        [CanBeNull]
        public WarrantOrganization SenderOrganization { get; set; }
    }
}