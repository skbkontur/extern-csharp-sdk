using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Accounts
{
    public class CreateAccountRequestDto
    {
        [JsonProperty("inn"), Required]
        public string Inn { get; set; }

        [JsonProperty("kpp")]
        public string Kpp { get; set; }

        [JsonProperty("organization-name"), Required]
        public string OrganizationName { get; set; }
    }
}
