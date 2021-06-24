using System;
using JetBrains.Annotations;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Certificates
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
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
        public byte[] Content { get; set; }

        public DateTime ExpiredAt { get; set; }

        public CertificateType CertificateType { get; set; }
    }
}