using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Accounts
{
    public class CreateAccountRequestDto
    {
        [Required, JsonProperty("inn")]
        public string Inn { get; set; }

        [JsonProperty("kpp")]
        public string Kpp { get; set; }

        [Required, JsonProperty("organization-name")]
        public string OrganizationName { get; set; }
    }
}