#nullable enable
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.Warrants
{
    [PublicAPI]
    public class Notary
    {
        public Fio? Fio { get; set; }
        public string? Inn { get; set; }
        public string? Address { get; set; }
        public WarrantOrganization? NotaryOrganization { get; set; }
    }
}