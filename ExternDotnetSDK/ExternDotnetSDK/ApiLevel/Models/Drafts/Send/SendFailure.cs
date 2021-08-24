#nullable enable
using System.Net;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Check;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts.Send
{
    public class SendFailure
    {
        [UsedImplicitly]
        public SendFailure(Urn id, int statusCode, string message, string status, CheckResultData? checkResult)
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
    }
}