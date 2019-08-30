using JetBrains.Annotations;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Certificates
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