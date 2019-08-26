using ExternDotnetSDK.Models.JsonConverters;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Warrants
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