#nullable enable
using System;
using System.Net;
using System.Text;
using Kontur.Extern.Client.ApiLevel.Models.Responses.Drafts.Send;
using Kontur.Extern.Client.Exceptions;
using Kontur.Extern.Client.Http.Serialization;
using Kontur.Extern.Client.Models.ApiTasks;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Models.Errors;
using Kontur.Extern.Client.Primitives.LongOperations;

namespace Kontur.Extern.Client.Model.Drafts.LongOperationStatuses
{
    public class DraftSendingFailure : ILongOperationFailure, IApiTaskResult
    {
        private readonly Guid draftId;
        private readonly IJsonSerializer jsonSerializer;

        public static DraftSendingFailure From(SendFailure failure, Guid draftId, IJsonSerializer jsonSerializer) => new(
            failure.Id,
            failure.Message,
            failure.StatusCode,
            failure.Status,
            failure.CheckResult is not null ? DraftCheckingStatus.From(failure.CheckResult) : null,
            failure.IsEmpty,
            draftId,
            jsonSerializer
        );

        public DraftSendingFailure(
            Urn id, 
            string message, 
            HttpStatusCode statusCode, 
            string status, 
            DraftCheckingStatus? checkStatus, 
            bool isEmpty,
            Guid draftId, 
            IJsonSerializer jsonSerializer)
        {
            this.draftId = draftId;
            this.jsonSerializer = jsonSerializer;
            Id = id;
            Message = message;
            StatusCode = statusCode;
            Status = status;
            CheckStatus = checkStatus;
            IsEmpty = isEmpty;
        }

        public Urn Id { get; }
        public string Message { get; }
        public HttpStatusCode StatusCode { get; }
        public string Status { get; }
        public DraftCheckingStatus? CheckStatus { get; }

        public bool IsEmpty { get; }

        public ApiException ToException() => Errors.LongOperationFailed(ToApiError());

        public ApiError ToApiError()
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
                messageBuilder.Append(jsonSerializer.SerializeToIndentedString(CheckStatus));
            }
            
            var message = messageBuilder.ToString();
            return new ApiError(Id, StatusCode, message);
        }
    }
}