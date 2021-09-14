#nullable enable
using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Drafts.Send;
using Kontur.Extern.Api.Client.Model.Drafts;
using Kontur.Extern.Api.Client.Model.Drafts.LongOperationStatuses;
using Kontur.Extern.Api.Client.Models.ApiTasks;
using Kontur.Extern.Api.Client.Models.Docflows;
using Kontur.Extern.Api.Client.Models.Drafts;
using Kontur.Extern.Api.Client.Models.Drafts.Meta;
using Kontur.Extern.Api.Client.Paths;
using Kontur.Extern.Api.Client.Primitives.LongOperations;
using Kontur.Extern.Api.Client.Http.Serialization;

namespace Kontur.Extern.Api.Client
{
    [PublicAPI]
    public static class DraftPathExtension
    {
        public static Task<Draft> GetAsync(this in DraftPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Drafts.GetDraftAsync(path.AccountId, path.DraftId, timeout);
        }
        
        public static Task<Draft?> TryGetAsync(this in DraftPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Drafts.TryGetDraftAsync(path.AccountId, path.DraftId, timeout);
        }
        
        public static Task DeleteAsync(this in DraftPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Drafts.DeleteDraftAsync(path.AccountId, path.DraftId, timeout);
        }
        
        public static Task<DraftMeta> UpdateMetadataAsync(this in DraftPath path, DraftMetadata metadata, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            return apiClient.Drafts.UpdateDraftMetaAsync(path.AccountId, path.DraftId, metadata.ToRequest(), timeout);
        }
        
        public static async Task<Guid> SetDocumentAsync(
            this DraftPath path,
            IDraftDocument document,
            TimeSpan? uploadTimeout = null,
            TimeSpan? putTimeout = null)
        {
            var apiClient = path.Services.Api;
            var uploader = path.Services.ContentService;
            var crypt = path.Services.Crypt;

            var (signature, documentRequest) = await document.CreateSignedRequestAsync(path.AccountId, uploader, crypt, uploadTimeout).ConfigureAwait(false);
            var createdOrUpdatedDocument = await apiClient.Drafts.UpdateDocumentAsync(path.AccountId, path.DraftId, document.DocumentId, documentRequest, putTimeout).ConfigureAwait(false);
            if (signature is not null)
            {
                await path.Document(createdOrUpdatedDocument.Id).AddSignatureAsync(signature.ToBase64String(), putTimeout).ConfigureAwait(false);
            }
            return createdOrUpdatedDocument.Id;
        }

        public static ILongOperation<DraftCheckingStatus> Check(this DraftPath path, TimeSpan? timeout = null)
        {
            var apiClient = path.Services.Api;
            var accountId = path.AccountId;
            var draftId = path.DraftId;

            return new LongOperation<DraftCheckingStatus>(
                async () =>
                {
                    var result = await apiClient.Drafts.StartCheckDraftAsync(accountId, draftId, timeout).ConfigureAwait(false);
                    return result.Convert(x => DraftCheckingStatus.From(x));
                },
                async taskId =>
                {
                    var result = await apiClient.Drafts.GetCheckDraftTaskStatusAsync(accountId, draftId, taskId, timeout).ConfigureAwait(false);
                    return result.Convert(x => DraftCheckingStatus.From(x));
                },
                path.Services.LongOperationsPollingStrategy
            );
        }
        
        public static ILongOperation<IDocflowWithDocuments, DraftSendingFailure> TrySend(this DraftPath path, bool allowToSendIncorrectPfrReport = false, TimeSpan? timeout = null)
        {   
            var apiClient = path.Services.Api;
            var accountId = path.AccountId;
            var draftId = path.DraftId;
            var jsonSerializer = path.Services.JsonSerializer;

            return new LongOperation<IDocflowWithDocuments, DraftSendingFailure>(
                async () =>
                {
                    var result = await apiClient.Drafts.StartSendDraftAsync(accountId, draftId, allowToSendIncorrectPfrReport, timeout).ConfigureAwait(false);
                    return result.ConvertFailureResult(x => DraftSendingFailure.From(x, draftId, jsonSerializer));
                },
                async taskId =>
                {
                    var result = await apiClient.Drafts.GetSendDraftTaskStatusAsync(accountId, draftId, taskId, timeout).ConfigureAwait(false);
                    return result.ConvertFailureResult(x => DraftSendingFailure.From(x, draftId, jsonSerializer));
                },
                path.Services.LongOperationsPollingStrategy
            );
        }
        
        public static ILongOperation<IDocflowWithDocuments> Send(this DraftPath path, bool allowToSendIncorrectPfrReport = false, TimeSpan? timeout = null)
        {
            var jsonSerializer = path.Services.JsonSerializer;
            var apiClient = path.Services.Api;
            var accountId = path.AccountId;
            var draftId = path.DraftId;

            return new LongOperation<IDocflowWithDocuments>(
                async () =>
                {
                    var result = await apiClient.Drafts.StartSendDraftAsync(accountId, draftId, allowToSendIncorrectPfrReport, timeout).ConfigureAwait(false);
                    return ToOnlyDraftApiResult(result, draftId, jsonSerializer);
                },
                async taskId =>
                {
                    var result = await apiClient.Drafts.GetSendDraftTaskStatusAsync(accountId, draftId, taskId, timeout).ConfigureAwait(false);
                    return ToOnlyDraftApiResult(result, draftId, jsonSerializer);
                },
                path.Services.LongOperationsPollingStrategy
            );

            static ApiTaskResult<IDocflowWithDocuments> ToOnlyDraftApiResult(ApiTaskResult<IDocflowWithDocuments, SendFailure> result, Guid draftId, IJsonSerializer serializer) => 
                result.ConvertToSingleApiResult(x => DraftSendingFailure.From(x, draftId, serializer).ToApiError());
        }
    }
}