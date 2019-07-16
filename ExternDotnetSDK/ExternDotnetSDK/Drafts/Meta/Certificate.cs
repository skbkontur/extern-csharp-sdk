using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ExternDotnetSDK.Drafts.Meta
{
    [DataContract]
    public class Certificate
    {
        /// <summary>Публичная часть сертификата</summary>
        [DataMember]
        [Required]
        public string Content { get; set; }
    }
}