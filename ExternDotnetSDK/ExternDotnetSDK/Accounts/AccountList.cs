using ExternDotnetSDK.Common;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Accounts
{
    public class AccountList
    {
        [JsonProperty("skip")]
        public long Skip { get; set; }

        [JsonProperty("take")]
        public long Take { get; set; }

        [JsonProperty("total-count")]
        public long TotalCount { get; set; }

        [JsonProperty("accounts")]
        public Account[] Accounts { get; set; }

        [JsonProperty("links")]
        public Link[] Links { get; set; }
    }
}