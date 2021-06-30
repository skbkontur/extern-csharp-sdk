using System.Net;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Check;
using Kontur.Extern.Client.ApiLevel.Models.Errors;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Prepare
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