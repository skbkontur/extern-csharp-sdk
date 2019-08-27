using JetBrains.Annotations;
using KeApiOpenSdk.Models.Common;
using KeApiOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiOpenSdk.Models.Certificates
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class CertificateList
    {
        public CertificateDto[] Certificates { get; set; }

        [UsedImplicitly]
        public long TotalCount { get; set; }

        [UsedImplicitly]
        public long Skip { get; set; }

        [UsedImplicitly]
        public long Take { get; set; }

        public Link[] Links { get; set; }
    }
}