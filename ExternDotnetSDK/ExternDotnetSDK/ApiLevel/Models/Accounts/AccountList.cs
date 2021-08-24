using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.Accounts
{
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