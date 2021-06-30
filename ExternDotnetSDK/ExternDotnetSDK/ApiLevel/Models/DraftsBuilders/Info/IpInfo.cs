using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Info
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