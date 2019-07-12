using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using ExternDotnetSDK.Common;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Accounts
{
    public class Account
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("inn")]
        public string Inn { get; set; }

        [JsonProperty("kpp")]
        public string Kpp { get; set; }

        [JsonProperty("organization-name")]
        public string OrganizationName { get; set; }

        [JsonProperty("product-name")]
        public string ProductName { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("links")]
        public Link[] Links { get; set; }
    }
}
