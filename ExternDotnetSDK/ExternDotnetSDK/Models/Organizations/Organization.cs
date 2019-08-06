using System;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Organizations
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class Organization
    {
        public Guid Id { get; set; }
        public OrganizationGeneral General { get; set; }
    }
}