using ExternDotnetSDK.Common;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Certificates
{
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
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