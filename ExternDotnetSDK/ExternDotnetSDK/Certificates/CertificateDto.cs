using System;
using ExternDotnetSDK.Common;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Certificates
{
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
    public class CertificateDto
    {
        [UsedImplicitly]
        public string Fio { get; set; }

        [UsedImplicitly]
        public string Inn { get; set; }

        [UsedImplicitly]
        public string Kpp { get; set; }

        [UsedImplicitly]
        public bool IsValid { get; set; }

        [UsedImplicitly]
        public bool IsCloud { get; set; }

        [UsedImplicitly]
        public bool IsQualified { get; set; }

        [UsedImplicitly]
        public string Content { get; set; }

        public DateTime ExpiredAt { get; set; }

        public CertificateType CertificateType { get; set; }
    }
}