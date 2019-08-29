using System;
using JetBrains.Annotations;
using KeApiClientOpenSdk.Models.Common;
using KeApiClientOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiClientOpenSdk.Models.Warrants
{
    [PublicAPI]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class WarrantIndividual
    {
        [CanBeNull]
        public Fio Fio { get; set; }

        [CanBeNull]
        public string Inn { get; set; }

        [CanBeNull]
        public IdentityCard Document { get; set; }

        [CanBeNull]
        public DateTime? BirthDate { get; set; }

        [CanBeNull]
        public string Ogrnip { get; set; }

        [CanBeNull]
        public string Citizenship { get; set; }

        [CanBeNull]
        public string Address { get; set; }
    }
}