#nullable enable
using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Warrants
{
    [PublicAPI]
    public class Warrant
    {
        public DateTime? DateBegin { get; set; }
        public DateTime? DateEnd { get; set; }
        public string? Number { get; set; }
        public List<int>? Permissions { get; set; } = new();
        public Notary? Notary { get; set; }
        public WarrantSender? Sender { get; set; }
        public WarrantIssuer? Issuer { get; set; }
        public WarrantTrustedIssuer? TrustedIssuer { get; set; }
    }
}