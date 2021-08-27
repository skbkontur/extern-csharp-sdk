using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Common
{
    public class CertificateRequest
    {
        //[JsonProperty(Required = Required.Always)]
        public byte[] Content { get; set; }
    }
}