using System;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Warrants
{
    [PublicAPI]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
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