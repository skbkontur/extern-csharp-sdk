using System.Net;
using KeApiOpenSdk.Models.Common;
using KeApiOpenSdk.Models.Drafts.Check;
using KeApiOpenSdk.Models.Errors;
using KeApiOpenSdk.Models.JsonConverters;
using Newtonsoft.Json;

namespace KeApiOpenSdk.Models.Drafts.Prepare
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