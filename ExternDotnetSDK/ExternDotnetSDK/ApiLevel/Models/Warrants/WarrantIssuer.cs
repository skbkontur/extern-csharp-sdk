using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Warrants
{
    [PublicAPI]
    public class WarrantIssuer
    {
        [CanBeNull]
        public IssuerOrganization IssuerOrganization { get; set; }
    }
}