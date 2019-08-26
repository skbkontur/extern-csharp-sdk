using System;
using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.JsonConverters;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Warrants
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