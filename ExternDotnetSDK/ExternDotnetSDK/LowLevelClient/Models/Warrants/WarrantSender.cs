using JetBrains.Annotations;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Warrants
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