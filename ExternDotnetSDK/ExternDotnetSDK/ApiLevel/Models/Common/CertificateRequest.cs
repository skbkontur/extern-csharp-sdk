using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Kontur.Extern.Client.ApiLevel.Models.Common
{
    [DataContract]
    public class CertificateRequest
    {
        [DataMember]
        [Required]
        public byte[] Content { get; set; }
    }
}