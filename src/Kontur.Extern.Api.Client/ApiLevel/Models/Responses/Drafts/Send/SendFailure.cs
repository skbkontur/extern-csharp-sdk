using System.Net;
using System.Text.Json.Serialization;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Drafts.Check;
using Kontur.Extern.Api.Client.Models.ApiTasks;
using Kontur.Extern.Api.Client.Models.Common;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Drafts.Send
{
    public class SendFailure : IApiTaskResult
    {
        [JsonConstructor]
        public SendFailure(Urn id, HttpStatusCode statusCode, string message, string status, CheckResultData? checkResult)
        {
            Id = id;
            StatusCode = statusCode;
            Message = message;
            Status = status;
            CheckResult = checkResult;
        }
        
        public Urn Id { get; }
        public HttpStatusCode StatusCode { get; }
        public string Message { get; }
        public string Status { get; }
        public CheckResultData? CheckResult { get; }

        [JsonIgnore]
        public bool IsEmpty => Id == null! && Message == null! && StatusCode == 0 && Status == null!;
    }
}