#nullable enable
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Warrants
{
    [PublicAPI]
    public class WarrantIssuer
    {
        public IssuerOrganization? IssuerOrganization { get; set; }
    }
}