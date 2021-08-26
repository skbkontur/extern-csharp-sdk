#nullable enable
using System;
using System.Net;
using System.Text;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Send;
using Kontur.Extern.Client.ApiLevel.Models.Errors;
using Kontur.Extern.Client.Http.Serialization;

namespace Kontur.Extern.Client.Model.Drafts.LongOperationStatuses
{
    public class DraftSendingFailure
    {
        public static DraftSendingFailure From(SendFailure failure) => new(
            failure.Id,
            failure.Message,
            failure.StatusCode,
            failure.Status,
            failure.CheckResult is not null ? DraftCheckingStatus.From(failure.CheckResult) : null
        );
        
        public DraftSendingFailure(Urn id, string message, HttpStatusCode statusCode, string status, DraftCheckingStatus? checkStatus)
        {
            Id = id;
            Message = message;
            StatusCode = statusCode;
            Status = status;
            CheckStatus = checkStatus;
        }

        public Urn Id { get; }
        public string Message { get; }
        public HttpStatusCode StatusCode { get; }
        public string Status { get; }
        public DraftCheckingStatus? CheckStatus { get; }

        public ApiError ToApiError(Guid draftId, IJsonSerializer serializer)
        {
            var messageBuilder = new StringBuilder();
            messageBuilder.Append($"Failed to send draft {draftId}.");
            if (!string.IsNullOrWhiteSpace(Status))
            {
                messageBuilder.Append(" ").Append(nameof(Status)).Append(": ").Append(Status);
            }

            messageBuilder.AppendLine();
                
            if (CheckStatus is not null && !CheckStatus.IsSuccessful)
            {
                messageBuilder.Append(serializer.SerializeToIndentedString(CheckStatus));
            }
            
            var message = messageBuilder.ToString();
            return new ApiError(Id, StatusCode, message);
        }
    }
}