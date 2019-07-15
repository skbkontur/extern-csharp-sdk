using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace ExternDotnetSDK.Documents.Data
{
    [DataContract]
    public class GenerateReplyDocumentRequestData
    {
        [DataMember]
        [Required]
        public string CertificateBase64 { get; set; }
    }
}