using System.Net;
using ExternDotnetSDK.Models.Common;
using ExternDotnetSDK.Models.Drafts.Check;
using ExternDotnetSDK.Models.Drafts.Prepare;
using ExternDotnetSDK.Models.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Models.Errors
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class ErrorPrepareResult : Error, IPrepareResult
    {
        public ErrorPrepareResult(string message, Urn id)
        {
            StatusCode = HttpStatusCode.BadRequest;
            Message = message;
            Id = id;
        }

        public CheckResultData CheckResult { get; set; }
        public Link[] Links { get; set; }
        public PrepareStatus Status { get; set; }
    }
}