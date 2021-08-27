using System.Text.Json.Serialization;
using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests
{
    public class SenderRequest
    {
        //[JsonProperty(Required = Required.Always)]
        public string Inn { get; set; }
        public string Kpp { get; set; }

        //[JsonProperty(Required = Required.Always)]
        public CertificateRequest Certificate { get; set; }

        //[JsonProperty(Required = Required.Always)]
        public bool IsRepresentative { get; set; }

        [JsonPropertyName("ipaddress")]
        //[JsonProperty(Required = Required.Always)]
        public string IpAddress { get; set; }
    }
}