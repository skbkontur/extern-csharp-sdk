#nullable enable
using System.Net;
using System.Text.Json.Serialization;
using Kontur.Extern.Client.ApiLevel.Models.Responses.Drafts.Check;
using Kontur.Extern.Client.Models.ApiTasks;
using Kontur.Extern.Client.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.Responses.Drafts.Send
{
    public class SendFailure : IApiTaskResult
    {
        [JsonConstructor]
        public SendFailure(Urn id, HttpStatusCode statusCode, string message, string status, CheckResultData? checkResult)
        {
            Id = id;
            StatusCode = (HttpStatusCode) statusCode;
            Message = message;
            Status = status;
            CheckResult = checkResult;
        }
        
        public Urn Id { get; }
        public HttpStatusCode StatusCode { get; }
        public string Message { get; }
        public string Status { get; }
        public CheckResultData? CheckResult { get; }

        public bool IsEmpty => Id == null! && Message is null! && StatusCode == 0 && Status == null!;
    }
}