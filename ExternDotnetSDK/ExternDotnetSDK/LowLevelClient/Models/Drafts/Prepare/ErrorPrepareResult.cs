using System.Net;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.Drafts.Check;
using Kontur.Extern.Client.Models.Errors;
using Kontur.Extern.Client.Models.JsonConverters;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Models.Drafts.Prepare
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