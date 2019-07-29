using System.ComponentModel.DataAnnotations;
using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Organizations
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class UpdateOrganizationRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}