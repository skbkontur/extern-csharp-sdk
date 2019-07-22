using System.ComponentModel.DataAnnotations;
using ExternDotnetSDK.Common;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Organizations
{
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
    public class CreateOrganizationRequestDto
    {
        [Required]
        public string Inn { get; set; }

        public string Kpp { get; set; }

        [Required]
        public string Name { get; set; }
    }
}