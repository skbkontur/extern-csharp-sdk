#nullable enable
using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Api;
using Kontur.Extern.Client.ApiLevel.Models.Docflows;
using Kontur.Extern.Client.ApiLevel.Models.Drafts;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Meta;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Send;
using Kontur.Extern.Client.Http.Serialization;
using Kontur.Extern.Client.Model.Documents.Contents;
using Kontur.Extern.Client.Model.Drafts;
using Kontur.Extern.Client.Model.Drafts.LongOperationStatuses;
using Kontur.Extern.Client.Paths;
using Kontur.Extern.Client.Primitives.LongOperations;
using Kontur.Extern.Client.Uploading;
using OneOf;
using DraftDocument = Kontur.Extern.Client.Model.Drafts.DraftDocument;

namespace Kontur.Extern.Client
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
            DraftDocument document,
            TimeSpan? uploadTimeout = null,
            TimeSpan? putTimeout = null)
        {
            var apiClient = path.Services.Api;
            var uploader = path.Services.ContentService;

            var contentId = await UploadContent(path.AccountId, uploader, document.DocumentContent).ConfigureAwait(false);
            var documentRequest = await document.CreateSignedRequestAsync(contentId, path.Services.Crypt).ConfigureAwait(false);
            var createdDocument = await apiClient.Drafts.UpdateDocumentAsync(path.AccountId, path.DraftId, document.DocumentId, documentRequest, putTimeout).ConfigureAwait(false);
            return createdDocument.Id;

            Task<Guid> UploadContent(Guid accountId, IContentService contentUploader, IDocumentContent content) =>
                content.UploadAsync(contentUploader, accountId, uploadTimeout);
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
        
        public static ILongOperation<OneOf<Docflow, DraftSendingFailure>> TrySend(this DraftPath path, bool allowToSendIncorrectPfrReport = false, TimeSpan? timeout = null)
        {   
            var apiClient = path.Services.Api;
            var accountId = path.AccountId;
            var draftId = path.DraftId;

            return new LongOperation<OneOf<Docflow, DraftSendingFailure>>(
                async () =>
                {
                    var result = await apiClient.Drafts.StartSendDraftAsync(accountId, draftId, allowToSendIncorrectPfrReport, timeout).ConfigureAwait(false);
                    return result.ConvertSecondResult(x => DraftSendingFailure.From(x));
                },
                async taskId =>
                {
                    var result = await apiClient.Drafts.GetSendDraftTaskStatusAsync(accountId, draftId, taskId, timeout).ConfigureAwait(false);
                    return result.ConvertSecondResult(x => DraftSendingFailure.From(x));
                },
                path.Services.LongOperationsPollingStrategy
            );
        }
        
        public static ILongOperation<Docflow> Send(this DraftPath path, bool allowToSendIncorrectPfrReport = false, TimeSpan? timeout = null)
        {
            var jsonSerializer = path.Services.JsonSerializer;
            var apiClient = path.Services.Api;
            var accountId = path.AccountId;
            var draftId = path.DraftId;

            return new LongOperation<Docflow>(
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

            static ApiTaskResult<Docflow> ToOnlyDraftApiResult(ApiTaskResult<Docflow, SendFailure> result, Guid draftId, IJsonSerializer serializer) => 
                result.ConsiderSecondAsError(x => DraftSendingFailure.From(x).ToApiError(draftId, serializer));
        }
    }
}