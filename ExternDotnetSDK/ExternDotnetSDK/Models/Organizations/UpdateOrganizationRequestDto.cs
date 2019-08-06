using System.ComponentModel.DataAnnotations;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Organizations
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class UpdateOrganizationRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}