using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Info
{
    public class IpInfo
    {
        /// <summary>
        /// ОГРНИП
        /// </summary>
        [Required]
        [DataMember]
        public string OgrnIp { get; set; }
    }
}