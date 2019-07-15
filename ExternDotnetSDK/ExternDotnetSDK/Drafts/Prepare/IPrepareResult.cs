using ExternDotnetSDK.Common;
using ExternDotnetSDK.Drafts.Check;

namespace ExternDotnetSDK.Drafts.Prepare
{
    public interface IPrepareResult
    {
        CheckResultData CheckResult { get; set; }
        Link[] Links { get; set; }
        PrepareStatus Status { get; set; }
    }
}