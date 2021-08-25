#nullable enable
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Warrants
{
    [PublicAPI]
    public class WarrantSender
    {
        public WarrantIndividual? SenderIndividual { get; set; }

        public WarrantOrganization? SenderOrganization { get; set; }
    }
}