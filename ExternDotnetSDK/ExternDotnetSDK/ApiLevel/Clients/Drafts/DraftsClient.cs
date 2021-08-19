#nullable enable
using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Api;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Docflows;
using Kontur.Extern.Client.ApiLevel.Models.Drafts;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Check;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Meta;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Prepare;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Send;
using Kontur.Extern.Client.Http;
using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.ApiLevel.Clients.Drafts
{
    public class DraftsClient : IDraftsClient
    {
        private readonly IHttpRequestsFactory http;

        public DraftsClient(IHttpRequestsFactory http) => this.http = http;

        public Task<Draft> CreateDraftAsync(Guid accountId, DraftMetaRequest meta, TimeSpan? timeout = null) => 
            http.PostAsync<DraftMetaRequest, Draft>($"/v1/{accountId}/drafts", meta, timeout);

        public Task<Draft> GetDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null) => 
            http.GetAsync<Draft>($"/v1/{accountId}/drafts/{draftId}", timeout);

        public Task<Draft?> TryGetDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null) => 
            http.TryGetAsync<Draft>($"/v1/{accountId}/drafts/{draftId}", timeout);

        public Task DeleteDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null) => 
            http.DeleteAsync($"/v1/{accountId}/drafts/{draftId}", timeout);

        public Task<DraftMeta> GetDraftMetaAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null) => 
            http.GetAsync<DraftMeta>($"/v1/{accountId}/drafts/{draftId}/meta", timeout);

        public Task<DraftMeta> UpdateDraftMetaAsync(
            Guid accountId,
            Guid draftId,
            DraftMetaRequest meta,
            TimeSpan? timeout = null)
        {
            return http.PutAsync<DraftMetaRequest, DraftMeta>($"/v1/{accountId}/drafts/{draftId}/meta", meta, timeout);
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
                timeout
            );
        }

        public Task<DraftDocument> GetDocumentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            TimeSpan? timeout = null)
        {
            return http.GetAsync<DraftDocument>($"/v1/{accountId}/drafts/{draftId}/documents/{documentId}", timeout);
        }

        public Task DeleteDocumentAsync(Guid accountId, Guid draftId, Guid documentId, TimeSpan? timeout = null) => 
            http.DeleteAsync($"/v1/{accountId}/drafts/{draftId}/documents/{documentId}", timeout);

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
                timeout
            );
        }

        public Task<byte[]> PrintDocumentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            TimeSpan? timeout = null)
        {
            return http.GetBytesAsync($"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/print", timeout);
        }

        public Task<ApiTaskResult<PrintDocumentResult>> StartPrintDocumentAsync(Guid accountId, Guid draftId, Guid documentId, TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/print")
                .AppendToQuery("deferred", true)
                .Build();
            return http.PostAsync<ApiTaskResult<PrintDocumentResult>>(url, timeout);
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
            return http.GetAsync<Signature>($"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures/{signatureId}", timeout);
        }

        public Task DeleteSignatureAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null)
        {
            return http.DeleteAsync($"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures/{signatureId}", timeout);
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
                timeout
            );
        }

        public async Task<string> GetSignatureContentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null)
        {
            var response = await http.Get($"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/signatures/{signatureId}/content".ToUrl()).SendAsync(timeout).ConfigureAwait(false);
            return await response.GetMessageAsync<string>().ConfigureAwait(false);
        }

        public Task<CheckResult> CheckDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null) => 
            http.PostAsync<CheckResult>($"/v1/{accountId}/drafts/{draftId}/check", timeout);

        public Task<ApiTaskResult<CheckResult>> StartCheckDraftAsync(
            Guid accountId,
            Guid draftId,
            TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/drafts/{draftId}/check")
                .AppendToQuery("deferred", true)
                .Build();
            return http.PostAsync<ApiTaskResult<CheckResult>>(url);
        }
        
        public Task<ApiTaskResult<CheckResult>> GetCheckDraftTaskStatusAsync(
            Guid accountId,
            Guid draftId,
            Guid taskId,
            TimeSpan? timeout = null)
        {
            return http.GetAsync<ApiTaskResult<CheckResult>>(
                $"/v1/{accountId}/drafts/{draftId}/tasks/{taskId}",
                timeout
            );
        }

        public Task<PrepareResult> PrepareDraftAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null) => 
            http.PostAsync<PrepareResult>($"/v1/{accountId}/drafts/{draftId}/prepare", timeout);

        public Task<ApiTaskResult<PrepareResult>> StartPrepareDraftAsync(
            Guid accountId,
            Guid draftId,
            TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/drafts/{draftId}/prepare")
                .AppendToQuery("deferred", true)
                .Build();
            return http.PostAsync<ApiTaskResult<PrepareResult>>(url);
        }

        public Task<ApiTaskResult<PrepareResult>> GetPrepareDraftTaskStatusAsync(Guid accountId, Guid draftId, Guid taskId, TimeSpan? timeout = null)
        {
            return http.GetAsync<ApiTaskResult<PrepareResult>>(
                $"/v1/{accountId}/drafts/{draftId}/tasks/{taskId}",
                timeout
            );
        }

        public Task<Docflow> SendDraftAsync(Guid accountId, Guid draftId, bool? force = null, TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/drafts/{draftId}/send")
                .AppendToQuery("force", force)
                .Build();
            return http.PostAsync<Docflow>(url, timeout);
        }

        public Task<ApiTaskResult<Docflow>> _StartSendDraftAsync(
            Guid accountId,
            Guid draftId,
            bool? force = null,
            TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/drafts/{draftId}/send")
                .AppendToQuery("deferred", true)
                .AppendToQuery("force", force)
                .Build();
            return http.PostAsync<ApiTaskResult<Docflow>>(url, timeout);
        }
        
        public async Task<ApiTaskResult<Docflow, SendFailure>> StartSendDraftAsync(
            Guid accountId,
            Guid draftId,
            bool? force = null,
            TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/drafts/{draftId}/send")
                .AppendToQuery("deferred", true)
                .AppendToQuery("force", force)
                .Build();

            var response = await http.Post(url).SendAsync(timeout, DoNotFailOnBadRequestsWithPayloads).ConfigureAwait(false);

            if (response.Status.IsSuccessful)
                return await response.GetMessageAsync<ApiTaskResult<Docflow>>().ConfigureAwait(false);
            
            return await response.GetMessageAsync<ApiTaskResult<SendFailure>>().ConfigureAwait(false);
        }

        public async Task<ApiTaskResult<Docflow, SendFailure>> GetSendDraftTaskStatusAsync(Guid accountId, Guid draftId, Guid taskId, TimeSpan? timeout = null)
        {
            var url = $"/v1/{accountId}/drafts/{draftId}/tasks/{taskId}";
            var response = await http.Get(url).SendAsync(timeout, DoNotFailOnBadRequestsWithPayloads).ConfigureAwait(false);
            
            if (response.Status.IsSuccessful)
                return await response.GetMessageAsync<ApiTaskResult<Docflow>>().ConfigureAwait(false);
            
            return await response.GetMessageAsync<ApiTaskResult<SendFailure>>().ConfigureAwait(false);
        }

        public Task BuildDocumentAsync(
            Guid accountId,
            Guid draftId,
            Guid documentId,
            FormatType type,
            int? version,
            string contract,
            TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/drafts/{draftId}/documents/{documentId}/build")
                .AppendToQuery("type", type)
                .AppendToQuery("version", version)
                .Build();
            return http.Post(url).WithJson(contract).SendAsync(timeout);
        }

        public async Task<DraftDocument> BuildDocumentAsync(
            Guid accountId,
            Guid draftId,
            FormatType type,
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
            return http.GetAsync<ApiTaskPage>(url, timeout);
        }

        public Task<SignInitResult> DssSignAsync(Guid accountId, Guid draftId, TimeSpan? timeout = null) => 
            http.PostAsync<SignInitResult>($"/v1/{accountId}/drafts/{draftId}/cloud-sign", timeout);

        public Task<ApiTaskResult<CryptOperationStatusResult>> GetDssSignTask(
            Guid accountId,
            Guid draftId,
            Guid taskId,
            TimeSpan? timeout = null)
        {
            return http.GetAsync<ApiTaskResult<CryptOperationStatusResult>>($"/v1/{accountId}/drafts/{draftId}/tasks/{taskId}", timeout);
        }

        private static bool DoNotFailOnBadRequestsWithPayloads(IHttpResponse httpResponse) => 
            httpResponse.Status.IsBadRequest && httpResponse.HasPayload && httpResponse.ContentType.IsJson;
    }
}