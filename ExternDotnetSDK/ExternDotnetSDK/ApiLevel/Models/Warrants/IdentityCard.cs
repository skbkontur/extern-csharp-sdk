using System;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Warrants
{
    [PublicAPI]
    public class IdentityCard
    {
        [CanBeNull]
        public string DocumentType { get; set; }

        [CanBeNull]
        public string Series { get; set; }

        [CanBeNull]
        public string Number { get; set; }

        [CanBeNull]
        public string Issuer { get; set; }

        [CanBeNull]
        public DateTime? IssuanceDate { get; set; }

        [CanBeNull]
        public string IssuerCode { get; set; }
    }
}