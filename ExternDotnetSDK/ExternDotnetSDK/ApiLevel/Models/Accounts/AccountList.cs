using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Accounts
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