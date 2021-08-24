using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.Warrants
{
    [PublicAPI]
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