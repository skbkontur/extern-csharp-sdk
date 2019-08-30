using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Warrants
{
    [PublicAPI]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class Warrant
    {
        [CanBeNull]
        public DateTime? DateBegin { get; set; }

        [CanBeNull]
        public DateTime? DateEnd { get; set; }

        [CanBeNull]
        public string Number { get; set; }

        [CanBeNull]
        public List<int> Permissions { get; set; } = new List<int>();

        [CanBeNull]
        public Notary Notary { get; set; }

        [CanBeNull]
        public WarrantSender Sender { get; set; }

        [CanBeNull]
        public WarrantIssuer Issuer { get; set; }

        [CanBeNull]
        public WarrantTrustedIssuer TrustedIssuer { get; set; }
    }
}