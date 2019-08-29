using JetBrains.Annotations;
using KeApiClientOpenSdk.Models.Common;
using KeApiClientOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiClientOpenSdk.Models.Accounts
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
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