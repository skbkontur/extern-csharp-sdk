using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.DraftsBuilders.Info
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
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