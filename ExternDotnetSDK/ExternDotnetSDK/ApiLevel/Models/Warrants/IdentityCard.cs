#nullable enable
using System;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Warrants
{
    [PublicAPI]
    public class IdentityCard
    {
        public string DocumentType { get; set; }
        public string Series { get; set; }
        public string? Number { get; set; }
        public string? Issuer { get; set; }
        public DateTime? IssuanceDate { get; set; }
        public string? IssuerCode { get; set; }
    }
}