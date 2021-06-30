using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Kontur.Extern.Client.ApiLevel.Models.Documents.Data
{
    [DataContract]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class GenerateReplyDocumentRequest
    {
        [DataMember]
        [Required]
        public byte[] CertificateBase64 { get; set; }
    }
}