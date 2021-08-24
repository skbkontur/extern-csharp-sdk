using Kontur.Extern.Client.ApiLevel.Models.Common;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests
{
    public class SenderRequest
    {
        [JsonProperty(Required = Required.Always)]
        public string Inn { get; set; }
        public string Kpp { get; set; }

        [JsonProperty(Required = Required.Always)]
        public CertificateRequest Certificate { get; set; }

        [JsonProperty(Required = Required.Always)]
        public bool IsRepresentative { get; set; }

        [JsonProperty("ipaddress", Required = Required.Always)]
        public string IpAddress { get; set; }
    }
}