using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.Certificates
{
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