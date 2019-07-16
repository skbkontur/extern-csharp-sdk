using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ExternDotnetSDK.DraftsBuilders.Info
{
    public class UlInfo
    {
        [Required]
        [DataMember]
        public string Ogrn { get; set; }

        [Required]
        [DataMember]
        public string Name { get; set; }
    }
}