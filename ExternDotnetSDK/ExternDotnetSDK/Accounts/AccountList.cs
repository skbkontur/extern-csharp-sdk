using ExternDotnetSDK.Common;
using ExternDotnetSDK.JsonConverters;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Accounts
{
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
    public class AccountList
    {
        [UsedImplicitly]
        public long Skip { get; set; }

        [UsedImplicitly]
        public long Take { get; set; }

        [UsedImplicitly]
        public long TotalCount { get; set; }

        public Account[] Accounts { get; set; }
        public Link[] Links { get; set; }
    }
}