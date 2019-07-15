using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ExternDotnetSDK.Drafts.Requests
{
    [DataContract]
    public class CertificateRequest
    {
        /// <summary>
        ///     Публичная часть сертификата
        /// </summary>
        [DataMember]
        [Required]
        public string Content { get; set; }
    }
}