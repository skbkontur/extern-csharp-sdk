using ExternDotnetSDK.Common;
using JetBrains.Annotations;

namespace ExternDotnetSDK.Accounts
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