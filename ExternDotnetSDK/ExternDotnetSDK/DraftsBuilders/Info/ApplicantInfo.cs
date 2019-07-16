using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using ExternDotnetSDK.Common;

namespace ExternDotnetSDK.DraftsBuilders.Info
{
    public class ApplicantInfo
    {
        [Required]
        [DataMember]
        public string Inn { get; set; }

        [Required]
        [DataMember]
        public Fio Fio { get; set; }

        [DataMember]
        public string Email { get; set; }
    }
}