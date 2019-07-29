using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace ExternDotnetSDK.Documents.Data
{
    [DataContract]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class GenerateReplyDocumentRequestData
    {
        [DataMember]
        [Required]
        public string CertificateBase64 { get; set; }
    }
}