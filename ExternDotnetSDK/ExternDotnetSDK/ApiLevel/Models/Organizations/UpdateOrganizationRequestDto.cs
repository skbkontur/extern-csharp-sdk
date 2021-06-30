using System.ComponentModel.DataAnnotations;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Organizations
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class UpdateOrganizationRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}