using System.ComponentModel.DataAnnotations;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Organizations
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class CreateOrganizationRequestDto
    {
        [Required]
        public string Inn { get; set; }

        public string Kpp { get; set; }

        [Required]
        public string Name { get; set; }
    }
}