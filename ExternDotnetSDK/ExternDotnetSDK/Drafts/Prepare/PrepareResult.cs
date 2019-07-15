using ExternDotnetSDK.Common;
using ExternDotnetSDK.Drafts.Check;

namespace ExternDotnetSDK.Drafts.Prepare
{
    public class PrepareResult : IPrepareResult
    {
        public CheckResultData CheckResult { get; set; }
        public Link[] Links { get; set; }
        public PrepareStatus Status { get; set; }
    }
}
