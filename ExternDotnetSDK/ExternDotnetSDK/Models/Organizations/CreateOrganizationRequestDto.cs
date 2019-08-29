﻿using System.ComponentModel.DataAnnotations;
using KeApiClientOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiClientOpenSdk.Models.Organizations
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