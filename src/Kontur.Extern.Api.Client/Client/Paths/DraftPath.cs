using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Drafts.Send;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Http.Serialization;
using Kontur.Extern.Api.Client.Model.Drafts;
using Kontur.Extern.Api.Client.Model.Drafts.LongOperationStatuses;
using Kontur.Extern.Api.Client.Models.ApiTasks;
using Kontur.Extern.Api.Client.Models.Docflows;
using Kontur.Extern.Api.Client.Models.Drafts;
using Kontur.Extern.Api.Client.Models.Drafts.Meta;
using Kontur.Extern.Api.Client.Primitives.LongOperations;

namespace Kontur.Extern.Api.Client.Paths
{
    [PublicAPI]
    [ClientDocumentationSection]
    public readonly struct DraftPath
    {
        public DraftPath(Guid accountId, Guid draftId, IExternClientServices services)
        {
            AccountId = accountId;
            DraftId = draftId;
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public Guid AccountId { get; }
        public Guid DraftId { get; }
        public IExternClientServices Services { get; }

        public DraftDocumentPath Document(Guid documentId) => new(AccountId, DraftId, documentId, Services);

        public Task<Draft> GetAsync(TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.Drafts.GetDraftAsync(AccountId, DraftId, timeout);
        }

        public Task<Draft?> TryGetAsync(TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.Drafts.TryGetDraftAsync(AccountId, DraftId, timeout);
        }

        public Task<bool> DeleteAsync(TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.Drafts.DeleteDraftAsync(AccountId, DraftId, timeout);
        }

        public Task<DraftMeta> UpdateMetadataAsync(DraftMetadata metadata, TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            return apiClient.Drafts.UpdateDraftMetaAsync(AccountId, DraftId, metadata.ToRequest(), timeout);
        }

        public async Task<Guid> SetDocumentAsync(
            IDraftDocument document,
            TimeSpan? uploadTimeout = null,
            TimeSpan? putTimeout = null)
        {
            var apiClient = Services.Api;
            var uploader = Services.ContentService;
            var crypt = Services.Crypt;

            var (signature, documentRequest) = await document.CreateSignedRequestAsync(AccountId, uploader, crypt, uploadTimeout).ConfigureAwait(false);
            var createdOrUpdatedDocument = await apiClient.Drafts.UpdateDocumentAsync(AccountId, DraftId, document.DocumentId, documentRequest, putTimeout).ConfigureAwait(false);
            if (signature is not null)
            {
                await Document(createdOrUpdatedDocument.Id).AddSignatureAsync(signature.ToBase64String(), putTimeout).ConfigureAwait(false);
            }
            return createdOrUpdatedDocument.Id;
        }

        public ILongOperation<DraftCheckingStatus> Check(TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            var accountId = AccountId;
            var draftId = DraftId;

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
                Services.LongOperationsPollingStrategy
            );
        }

        public ILongOperation<IDocflowWithDocuments, DraftSendingFailure> TrySend(bool allowToSendIncorrectPfrReport = false, TimeSpan? timeout = null)
        {
            var apiClient = Services.Api;
            var accountId = AccountId;
            var draftId = DraftId;
            var jsonSerializer = Services.JsonSerializer;

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
                Services.LongOperationsPollingStrategy
            );
        }

        public ILongOperation<IDocflowWithDocuments> Send(bool allowToSendIncorrectPfrReport = false, TimeSpan? timeout = null)
        {
            var jsonSerializer = Services.JsonSerializer;
            var apiClient = Services.Api;
            var accountId = AccountId;
            var draftId = DraftId;

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
                Services.LongOperationsPollingStrategy
            );

            ApiTaskResult<IDocflowWithDocuments> ToOnlyDraftApiResult(ApiTaskResult<IDocflowWithDocuments, SendFailure> result, Guid draftId, IJsonSerializer serializer) =>
                result.ConvertToSingleApiResult(x => DraftSendingFailure.From(x, draftId, serializer).ToApiError());
        }
    }
}