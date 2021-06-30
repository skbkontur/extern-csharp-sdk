using System;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Organizations
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class Organization
    {
        public Guid Id { get; set; }
        public OrganizationGeneral General { get; set; }
    }
}