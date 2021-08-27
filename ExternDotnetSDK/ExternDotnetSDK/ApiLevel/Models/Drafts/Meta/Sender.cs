using System.Text.Json.Serialization;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Meta
{
    public class Sender
    {
        //[JsonProperty(Required = Required.Always)]
        public string Inn { get; set; }

        public string Kpp { get; set; }

        public string Name { get; set; }

        //[JsonProperty(Required = Required.Always)]
        public Certificate Certificate { get; set; }

        //[JsonProperty(Required = Required.Always)]
        public bool IsRepresentative { get; set; }

        [JsonPropertyName("ipaddress")]
        //[Required]
        public string IpAddress { get; set; }
    }
}