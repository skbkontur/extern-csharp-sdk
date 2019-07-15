using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ExternDotnetSDK.DraftsBuilders.Info
{
    public class IpInfo
    {
        [Required]
        [DataMember]
        public string OgrnIp { get; set; }
    }
}
