using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Info
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class UlInfo
    {
        /// <summary>
        /// ОГРН
        /// </summary>
        [Required]
        [DataMember]
        public string Ogrn { get; set; }

        /// <summary>
        /// Название организации
        /// </summary>
        [Required]
        [DataMember]
        public string Name { get; set; }
    }
}