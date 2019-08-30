using System.ComponentModel.DataAnnotations;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Organizations
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class UpdateOrganizationRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}