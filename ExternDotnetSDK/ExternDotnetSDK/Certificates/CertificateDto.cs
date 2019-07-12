using System;
using JetBrains.Annotations;

namespace ExternDotnetSDK.Certificates
{
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
    }
}