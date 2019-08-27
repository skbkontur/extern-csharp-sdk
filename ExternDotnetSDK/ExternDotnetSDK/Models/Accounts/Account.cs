using System;
using KeApiOpenSdk.Models.Common;
using KeApiOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiOpenSdk.Models.Accounts
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class Account
    {
        public Guid Id { get; set; }
        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string OrganizationName { get; set; }
        public string ProductName { get; set; }
        public string Role { get; set; }
        public Link[] Links { get; set; }
    }
}