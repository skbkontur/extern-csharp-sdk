using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Warrants
{
    [PublicAPI]
    public class WarrantSender
    {
        [CanBeNull]
        public WarrantIndividual SenderIndividual { get; set; }

        [CanBeNull]
        public WarrantOrganization SenderOrganization { get; set; }
    }
}