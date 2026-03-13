using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Docflows;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Docflows.Documents;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Docflows;
using Kontur.Extern.Api.Client.Models.ApiTasks;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Models.Docflows;
using Kontur.Extern.Api.Client.Models.Docflows.Documents;
using Kontur.Extern.Api.Client.Http;
using Kontur.Extern.Api.Client.Models.Docflows.DocumentsRequests;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Vostok.Clusterclient.Core.Model;

// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.ApiLevel.Clients.Docflows
{
    //todo Сделать нормальные тесты для методов.
    public class DocflowsClient : IDocflowsClient
    {
        private readonly IHttpRequestFactory http;

        public DocflowsClient(IHttpRequestFactory http) => this.http = http;

        public Task<DocflowPage> GetDocflowsAsync(Guid accountId, DocflowFilter? filter = null, TimeSpan? timeout = null)
        {
            var urlBuilder = new RequestUrlBuilder($"/v1/{accountId}/docflows");
            filter?.AppendToQuery(urlBuilder);
            return GetRelatedDocflowsAsync(urlBuilder, $"{nameof(DocflowsClient)}.{nameof(GetDocflowsAsync)}", timeout);
        }

        public Task<DocflowPage> GetRelatedDocflows(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            DocflowFilter? filter = null,
            TimeSpan? timeout = null)
        {
            var urlBuilder = new RequestUrlBuilder($"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/related");
            filter?.AppendToQuery(urlBuilder);
            return GetRelatedDocflowsAsync(urlBuilder, $"{nameof(DocflowsClient)}.{nameof(GetRelatedDocflows)}", timeout);
        }

        public Task<DocflowPage> GetInventoryDocflowsAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            DocflowFilter? filter = null,
            TimeSpan? timeout = null)
        {
            var urlBuilder = new RequestUrlBuilder($"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories");
            filter?.AppendToQuery(urlBuilder);
            return GetRelatedDocflowsAsync(urlBuilder, $"{nameof(DocflowsClient)}.{nameof(GetInventoryDocflowsAsync)}", timeout);
        }

        public Task<IDocflowWithDocuments> GetDocflowAsync(Guid accountId, Guid docflowId, TimeSpan? timeout = null) =>
            GetDocflowAsync($"/v1/{accountId}/docflows/{docflowId}", $"{nameof(DocflowsClient)}.{nameof(GetDocflowAsync)}", timeout);

        public Task<IDocflowWithDocuments?> TryGetDocflowAsync(Guid accountId, Guid docflowId, TimeSpan? timeout = null) =>
            http.TryGetAsync<IDocflowWithDocuments>($"/v1/{accountId}/docflows/{docflowId}", timeout, $"{nameof(DocflowsClient)}.{nameof(TryGetDocflowAsync)}");

        public Task<IDocflowWithDocuments> GetInventoryDocflowAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            TimeSpan? timeout = null)
        {
            return GetDocflowAsync($"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}", $"{nameof(DocflowsClient)}.{nameof(GetInventoryDocflowAsync)}", timeout);
        }

        public Task<IDocflowWithDocuments?> TryGetInventoryDocflowAsync(Guid accountId, Guid relatedDocflowId, Guid relatedDocumentId, Guid inventoryId, TimeSpan? timeout = null) =>
            TryGetDocflowAsync($"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}", $"{nameof(DocflowsClient)}.{nameof(TryGetInventoryDocflowAsync)}", timeout);

        public Task<List<Document>> GetDocumentsAsync(Guid accountId, Guid docflowId, TimeSpan? timeout = null) =>
            http.GetAsync<List<Document>>(
                $"/v1/{accountId}/docflows/{docflowId}/documents",
                TimeoutSpecification.SpecificOrLongOperationTimeout(timeout),
                $"{nameof(DocflowsClient)}.{nameof(GetDocumentsAsync)}");

        public Task<Document> GetDocumentAsync(Guid accountId, Guid docflowId, Guid documentId, TimeSpan? timeout = null) =>
            http.GetAsync<Document>($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}", timeout, $"{nameof(DocflowsClient)}.{nameof(GetDocumentAsync)}");

        public Task<Document?> TryGetDocumentAsync(Guid accountId, Guid docflowId, Guid documentId, TimeSpan? timeout = null) =>
            http.TryGetAsync<Document>($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}",timeout, $"{nameof(DocflowsClient)}.{nameof(TryGetDocumentAsync)}");

        public Task<Document> PatchDocumentAsync(Guid accountId, Guid docflowId, Guid documentId, JsonPatchDocument<Document> patch, TimeSpan? timeout = null)
        {
            return http.PatchAsync<List<Operation<Document>>, Document>($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}", patch.Operations,timeout, $"{nameof(DocflowsClient)}.{nameof(PatchDocumentAsync)}_WithDocumentId");
        }

        public Task<IDocflowWithDocuments> PatchDocflowAsync(Guid accountId, Guid docflowId, JsonPatchDocument<IDocflowWithDocuments> patch, TimeSpan? timeout = null)
        {
            return http.PatchAsync<List<Operation<IDocflowWithDocuments>>, IDocflowWithDocuments>($"/v1/{accountId}/docflows/{docflowId}", patch.Operations,timeout, $"{nameof(DocflowsClient)}.{nameof(PatchDocflowAsync)}");
        }

        public Task<DocflowDocumentDescription> GetDocumentDescriptionAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            TimeSpan? timeout = null)
        {
            return http.GetAsync<DocflowDocumentDescription>(
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/description",
                timeout,
                $"{nameof(DocflowsClient)}.{nameof(GetDocumentDescriptionAsync)}");
        }

        public Task<List<Signature>> GetDocumentSignaturesAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            TimeSpan? timeout = null)
        {
            return http.GetAsync<List<Signature>>(
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/signatures",
                timeout,
                $"{nameof(DocflowsClient)}.{nameof(GetDocumentSignaturesAsync)}");
        }

        public Task<Signature> GetSignatureAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null)
        {
            return http.GetAsync<Signature>(
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/signatures/{signatureId}",
                timeout,
                $"{nameof(DocflowsClient)}.{nameof(GetSignatureAsync)}");
        }

        public async Task<byte[]> GetSignatureContentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null)
        {
            var base64String = await http.GetAsync<string>(
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/signatures/{signatureId}/content",
                timeout,
                $"{nameof(DocflowsClient)}.{nameof(GetSignatureContentAsync)}");
            return Convert.FromBase64String(base64String);
        }

        public async Task<byte[]> GetInventorySignatureContentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null)
        {
            var base64String = await http.GetAsync<string>(
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/signatures/{signatureId}/content",
                timeout,
                $"{nameof(DocflowsClient)}.{nameof(GetInventorySignatureContentAsync)}");
            return Convert.FromBase64String(base64String);
        }

        public Task<PrintDocumentResult> PrintDocumentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid contentId,
            TimeSpan? timeout = null)
        {
            return http.PostAsync<PrintDocumentRequest, PrintDocumentResult>(
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/print",
                new PrintDocumentRequest {ContentId = contentId},
                timeout,
                $"{nameof(DocflowsClient)}.{nameof(PrintDocumentAsync)}");
        }

        public Task<PrintDocumentResult> PrintInventoryDocumentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid contentId,
            TimeSpan? timeout = null)
        {
            return http.PostAsync<PrintDocumentRequest, PrintDocumentResult>(
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/print",
                new PrintDocumentRequest {ContentId = contentId},
                timeout,
                $"{nameof(DocflowsClient)}.{nameof(PrintInventoryDocumentAsync)}");
        }

        public Task<ApiTaskResult<PrintDocumentResult>> StartPrintDocumentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid contentId,
            TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/print")
                .AppendToQuery("deferred", true)
                .Build();
            return http.PostAsync<PrintDocumentRequest, ApiTaskResult<PrintDocumentResult>>(
                url,
                new PrintDocumentRequest {ContentId = contentId},
                timeout,
                $"{nameof(DocflowsClient)}.{nameof(StartPrintDocumentAsync)}");
        }

        public Task<ApiTaskResult<PrintDocumentResult>> StartPrintInventoryDocumentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid contentId,
            TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/print")
                .AppendToQuery("deferred", true)
                .Build();
            return http.PostAsync<PrintDocumentRequest, ApiTaskResult<PrintDocumentResult>>(
                url,
                new PrintDocumentRequest {ContentId = contentId},
                timeout,
                $"{nameof(DocflowsClient)}.{nameof(StartPrintInventoryDocumentAsync)}");
        }

        public Task<ApiTaskResult<PrintDocumentResult>> GetPrintDocumentTaskAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid taskId,
            TimeSpan? timeout = null)
        {
            return http.GetAsync<ApiTaskResult<PrintDocumentResult>>(
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/tasks/{taskId}",
                timeout,
                $"{nameof(DocflowsClient)}.{nameof(GetPrintDocumentTaskAsync)}");
        }

        public Task<ApiTaskResult<PrintDocumentResult>> GetPrintInventoryDocumentTaskAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid taskId,
            TimeSpan? timeout = null)
        {
            return http.GetAsync<ApiTaskResult<PrintDocumentResult>>(
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/tasks/{taskId}",
                timeout,
                $"{nameof(DocflowsClient)}.{nameof(GetPrintInventoryDocumentTaskAsync)}");
        }

        public Task<RecognizeResult> RecognizeDocumentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid contentId,
            TimeSpan? timeout = null)
        {
            return http.PostAsync<RecognizeRequest, RecognizeResult>(
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/recognize",
                new RecognizeRequest
                {
                    ContentId = contentId
                },
                timeout,
                $"{nameof(DocflowsClient)}.{nameof(RecognizeDocumentAsync)}");
        }

        public Task<DocumentsRequest> GetDocumentsRequestAsync(Guid accountId, Guid docflowId, Guid requestId, TimeSpan? timeout = null) =>
            http.GetAsync<DocumentsRequest>(
                $"/v1/{accountId}/docflows/{docflowId}/documents-requests/{requestId}",
                timeout,
                $"{nameof(DocflowsClient)}.{nameof(GetDocumentsRequestAsync)}");

        public Task<DocumentsRequest> GenerateDocumentsRequestAsync(Guid accountId, Guid docflowId, byte[] certificate, TimeSpan? timeout = null, Guid? machineReadableWarrantId = null)
        {
            return http.PostAsync<GenerateDocumentsRequestRequest, DocumentsRequest>(
                $"/v1/{accountId}/docflows/{docflowId}/generate-documents-request",
                new GenerateDocumentsRequestRequest
                {
                    CertificateBase64 = certificate,
                    MachineReadableWarrantId = machineReadableWarrantId
                },
                timeout,
                $"{nameof(DocflowsClient)}.{nameof(GenerateDocumentsRequestAsync)}");
        }

        public Task<IDocflowWithDocuments> SendDocumentsRequestAsync(Guid accountId, Guid docflowId, Guid requestId, TimeSpan? timeout = null)
        {
            return http.PostAsync<IDocflowWithDocuments>(
                $"/v1/{accountId}/docflows/{docflowId}/documents-requests/{requestId}/send",
                timeout,
                $"{nameof(DocflowsClient)}.{nameof(SendDocumentsRequestAsync)}");
        }

        public Task<DocumentsRequest> UpdateDocumentsRequestSignatureAsync(Guid accountId, Guid docflowId, Guid requestId, byte[] signature, TimeSpan? timeout = null)
        {
            return http.PutAsync<byte[], DocumentsRequest>(
                $"v1/{accountId}/docflows/{docflowId}/documents-requests/{requestId}/signature",
                signature,
                timeout,
                $"{nameof(DocflowsClient)}.{nameof(UpdateDocumentsRequestSignatureAsync)}");
        }

        public Task<SaveDecryptedContentResult> SaveDocumentDecryptedContentAsync(Guid accountId, Guid docflowId, Guid documentId, SaveDecryptedContentRequest request, TimeSpan? timeout = null)
        {
            return http.PutAsync<SaveDecryptedContentRequest, SaveDecryptedContentResult>(
                $"v1/{accountId}/docflows/{docflowId}/documents/{documentId}/decrypted-content",
                request,
                timeout,
                $"{nameof(DocflowsClient)}.{nameof(SaveDocumentDecryptedContentAsync)}");
        }

        private Task<DocflowPage> GetRelatedDocflowsAsync(RequestUrlBuilder urlBuilder, string callingMethod, TimeSpan? timeout) => http.GetAsync<DocflowPage>(urlBuilder.Build(), timeout, callingMethod);

        private Task<IDocflowWithDocuments> GetDocflowAsync(string url, string callingMethod, TimeSpan? timeout)
        {
            return http.GetAsync<IDocflowWithDocuments>(url, timeout, callingMethod);
        }

        private Task<IDocflowWithDocuments?> TryGetDocflowAsync(string url, string callingMethod, TimeSpan? timeout) => http.TryGetAsync<IDocflowWithDocuments>(url, timeout, callingMethod);
    }
}