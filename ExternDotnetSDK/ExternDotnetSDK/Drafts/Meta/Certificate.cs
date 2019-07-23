using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Drafts.Meta
{
    [DataContract]
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
    public class Certificate
    {
        /// <summary>Публичная часть сертификата</summary>
        [DataMember]
        [Required]
        public string Content { get; set; }
    }
}