using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts.Documents;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts.Signatures;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.ApiTasks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Drafts.Check;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Drafts.Send;
using Kontur.Extern.Api.Client.Models.ApiTasks;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Models.Docflows;
using Kontur.Extern.Api.Client.Models.Drafts;
using Kontur.Extern.Api.Client.Models.Drafts.Documents;
using Kontur.Extern.Api.Client.Models.Drafts.Meta;
using Kontur.Extern.Api.Client.Http;
using Kontur.Extern.Api.Client.Models.Drafts.Prepare;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Api.Client.ApiLevel.Clients.Drafts
{
    public class DraftsClient : IDraftsClient
    {
        private readonly IHttpRequestFactory http;

        public DraftsClient(IHttpRequestFactory http) => this.http = http;

        public Task<Draft> CreateDraftAsync(Guid accountId, DraftMetaRequest meta, TimeSpan? timeout = null) =>
            http.PostAsync<DraftMetaRequest, Draft>($"/v1/{accountId}/drafts", meta,  $"{nameof(DraftsClient)}.{nameof(CreateDraftAsync)}", timeout);

        public Task<Draft> GetDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null) =>
            http.GetAsync<Draft>($"/v1/{accountId}/drafts/{draftId}", $"{nameof(DraftsClient)}.{nameof(GetDraftAsync)}",timeout);

        public Task<Draft?> TryGetDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null) =>
            http.TryGetAsync<Draft>($"/v1/{accountId}/drafts/{draftId}", $"{nameof(DraftsClient)}.{nameof(TryGetDraftAsync)}", timeout);

        public Task<bool> DeleteDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null) =>
            http.TryDeleteAsync($"/v1/{accountId}/drafts/{draftId}", $"{nameof(DraftsClient)}.{nameof(DeleteDraftAsync)}", timeout);

        public Task<DraftMeta> GetDraftMetaAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null) =>
            http.GetAsync<DraftMeta>($"/v1/{accountId}/drafts/{draftId}/meta", $"{nameof(DraftsClient)}.{nameof(GetDraftMetaAsync)}", timeout);

        public Task<DraftMeta> UpdateDraftMetaAsync(
            Guid accountId,
            Guid draftId,
            DraftMetaRequest meta,
            TimeSpan? timeout = null)
        {
            return http.PutAsync<DraftMetaRequest, DraftMeta>($"/v1/{accountId}/drafts/{draftId}/meta", meta,  $"{nameof(DraftsClient)}.{nameof(UpdateDraftMetaAsync)}", timeout);
        }

        public Task<DraftDocument> CreateDocumentAsync(
            Guid accountId,
            Guid draftId,
            DocumentRequest documentRequest,
            TimeSpan? timeout = null)
        {
            return http.PostAsync<DocumentRequest, DraftDocument>(
                $"/v1/{accountId}/drafts/{draftId}/documents",
                documentRequest,
                $"{nameof(DraftsClient)}.{nameof(CreateDocumentAsync)}",
                timeout
            );
        }

        public Task<DraftDocument> GetDocumentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            TimeSpan? timeout = null)
        {
            return http.GetAsync<DraftDocument>($"/v1/{accountId}/drafts/{draftId}/documents/{documentId}", $"{nameof(DraftsClient)}.{nameof(GetDocumentAsync)}", timeout);
        }

        public Task<bool> DeleteDocumentAsync(Guid accountId, Guid draftId, Guid documentId, TimeSpan? timeout = null) =>
            http.TryDeleteAsync($"/v1/{accountId}/drafts/{draftId}/documents/{documentId}", $"{nameof(DraftsClient)}.{nameof(DeleteDocumentAsync)}",timeout);

        public Task<DraftDocument> UpdateDocumentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            DocumentRequest documentRequest,
            TimeSpan? timeout = null)
        {
            return http.PutAsync<DocumentRequest, DraftDocument>(
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}",
                documentRequest,
                $"{nameof(DraftsClient)}.{nameof(UpdateDocumentAsync)}",
                timeout
            );
        }

        [Obsolete($"Use async api-task {nameof(StartPrintDocumentAsync)}() method instead")]
        public async Task<byte[]> PrintDocumentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            TimeSpan? timeout = null)
        {
            var base64String = await http.GetAsync<string>(
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/print",
                $"{nameof(DraftsClient)}.{nameof(PrintDocumentAsync)}",
                timeout
            );
            return Convert.FromBase64String(base64String);
        }

        public Task<ApiTaskResult<PrintDocumentResult>> StartPrintDocumentAsync(Guid accountId, Guid draftId, Guid documentId, TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/print")
                .AppendToQuery("deferred", true)
                .Build();

            return http.GetAsync<ApiTaskResult<PrintDocumentResult>>(url, $"{nameof(DraftsClient)}.{nameof(StartPrintDocumentAsync)}", timeout);
        }

        public Task<Signature> CreateSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            SignatureRequest? signatureRequest = null,
            TimeSpan? timeout = null)
        {
            return http.PostAsync<SignatureRequest, Signature>(
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures",
                signatureRequest,
                $"{nameof(DraftsClient)}.{nameof(CreateSignatureAsync)}",
                timeout
            );
        }

        public Task<Signature> GetSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null)
        {
            return http.GetAsync<Signature>($"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures/{signatureId}", $"{nameof(DraftsClient)}.{nameof(GetSignatureAsync)}", timeout);
        }

        public Task<bool> DeleteSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null)
        {
            return http.TryDeleteAsync($"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures/{signatureId}", $"{nameof(DraftsClient)}.{nameof(DeleteSignatureAsync)}", timeout);
        }

        public Task<Signature> UpdateSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId,
            SignatureRequest signatureRequest,
            TimeSpan? timeout = null)
        {
            return http.PutAsync<SignatureRequest, Signature>(
                $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures/{signatureId}",
                signatureRequest,
                $"{nameof(DraftsClient)}.{nameof(UpdateSignatureAsync)}",
                timeout
            );
        }

        public async Task<byte[]> GetSignatureContentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null)
        {
            var base64String = await http.GetAsync<string>(
                    $"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures/{signatureId}/content".ToUrl(),
                    $"{nameof(DraftsClient)}.{nameof(GetSignatureContentAsync)}",
                    timeout)
                .ConfigureAwait(false);
            return Convert.FromBase64String(base64String);
        }

        public Task<CheckResult> CheckDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null) =>
            http.PostAsync<CheckResult>($"/v1/{accountId}/drafts/{draftId}/check", $"{nameof(DraftsClient)}.{nameof(CheckDraftAsync)}", timeout);

        public Task<ApiTaskResult<CheckResult>> StartCheckDraftAsync(
            Guid accountId,
            Guid draftId,
            TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/drafts/{draftId}/check")
                .AppendToQuery("deferred", true)
                .Build();
            return http.PostAsync<ApiTaskResult<CheckResult>>(url, $"{nameof(DraftsClient)}.{nameof(StartCheckDraftAsync)}");
        }

        public Task<ApiTaskResult<CheckResult>> GetCheckDraftTaskStatusAsync(
            Guid accountId,
            Guid draftId,
            Guid taskId,
            TimeSpan? timeout = null)
        {
            return http.GetAsync<ApiTaskResult<CheckResult>>(
                $"/v1/{accountId}/drafts/{draftId}/tasks/{taskId}",
                $"{nameof(DraftsClient)}.{nameof(GetCheckDraftTaskStatusAsync)}",
                timeout
            );
        }

        public Task<IDocflowWithDocuments> SendDraftAsync(Guid accountId, Guid draftId, bool? force = null, TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/drafts/{draftId}/send")
                .AppendToQuery("force", force)
                .Build();
            return http.PostAsync<IDocflowWithDocuments>(url, $"{nameof(DraftsClient)}.{nameof(SendDraftAsync)}", timeout);
        }

        public async Task<ApiTaskResult<IDocflowWithDocuments, SendFailure>> StartSendDraftAsync(
            Guid accountId,
            Guid draftId,
            bool? force = null,
            TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/drafts/{draftId}/send")
                .AppendToQuery("deferred", true)
                .AppendToQuery("force", force)
                .Build();

            var response = await http
                .Post(url)
                .CallingMethod($"{nameof(DraftsClient)}.{nameof(StartSendDraftAsync)}")
                .SendAsync(timeout, DoNotFailOnBadRequestsWithPayloads).ConfigureAwait(false);

            return await response.GetMessageAsync<ApiTaskResult<IDocflowWithDocuments, SendFailure>>().ConfigureAwait(false);
        }

        public async Task<ApiTaskResult<IDocflowWithDocuments, SendFailure>> GetSendDraftTaskStatusAsync(Guid accountId, Guid draftId, Guid taskId, TimeSpan? timeout = null)
        {
            var url = $"/v1/{accountId}/drafts/{draftId}/tasks/{taskId}";
            var response = await http
                .Get(url)
                .CallingMethod($"{nameof(DraftsClient)}.{nameof(GetSendDraftTaskStatusAsync)}")
                .SendAsync(timeout, DoNotFailOnBadRequestsWithPayloads).ConfigureAwait(false);

            return await response.GetMessageAsync<ApiTaskResult<IDocflowWithDocuments, SendFailure>>().ConfigureAwait(false);
        }

        public Task BuildDocumentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            DocumentFormatType type,
            int? version,
            string contract,
            TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/build")
                .AppendToQuery("type", type)
                .AppendToQuery("version", version)
                .Build();
            
            return http
                .Post(url)
                .WithJson(contract)
                .CallingMethod($"{nameof(DraftsClient)}.{nameof(BuildDocumentAsync)}_WithDocumentId")
                .SendAsync(timeout);
        }

        public async Task<DraftDocument> BuildDocumentAsync(
            Guid accountId,
            Guid draftId,
            DocumentFormatType type,
            int? version,
            string contract,
            TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/drafts/{draftId}/build-document")
                .AppendToQuery("type", type)
                .AppendToQuery("version", version)
                .Build();
            var response = await http.Post(url)
                .WithJson(contract)
                .CallingMethod($"{nameof(DraftsClient)}.{nameof(BuildDocumentAsync)}")
                .SendAsync(timeout).ConfigureAwait(false);
            return await response.GetMessageAsync<DraftDocument>().ConfigureAwait(false);
        }

        public Task<ApiTaskPage> GetDraftTasks(
            Guid accountId,
            Guid draftId,
            int? skip = null,
            int? take = null,
            bool? includeReleased = null,
            TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/drafts/{draftId}/tasks")
                .AppendToQuery("skip", skip)
                .AppendToQuery("take", take)
                .AppendToQuery("includeReleased", includeReleased)
                .Build();
            return http.GetAsync<ApiTaskPage>(url, $"{nameof(DraftsClient)}.{nameof(GetDraftTasks)}", timeout);
        }

        public Task<PrepareResult> PrepareDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null)
            => http.PostAsync<PrepareResult>($"/v1/{accountId}/drafts/{draftId}/prepare", $"{nameof(DraftsClient)}.{nameof(PrepareDraftAsync)}", timeout);

        private static bool DoNotFailOnBadRequestsWithPayloads(IHttpResponse httpResponse) =>
            httpResponse.Status.IsBadRequest && httpResponse.HasPayload && httpResponse.ContentType.IsJson;
    }
}