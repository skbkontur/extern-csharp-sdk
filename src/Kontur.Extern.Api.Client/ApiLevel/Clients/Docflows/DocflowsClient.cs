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
            return GetRelatedDocflowsAsync(urlBuilder, timeout);
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
            return GetRelatedDocflowsAsync(urlBuilder, timeout);
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
            return GetRelatedDocflowsAsync(urlBuilder, timeout);
        }

        public Task<IDocflowWithDocuments> GetDocflowAsync(Guid accountId, Guid docflowId, TimeSpan? timeout = null) =>
            GetDocflowAsync($"/v1/{accountId}/docflows/{docflowId}", timeout);

        public Task<IDocflowWithDocuments?> TryGetDocflowAsync(Guid accountId, Guid docflowId, TimeSpan? timeout = null) =>
            http.TryGetAsync<IDocflowWithDocuments>($"/v1/{accountId}/docflows/{docflowId}", timeout);

        public Task<IDocflowWithDocuments> GetInventoryDocflowAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            TimeSpan? timeout = null)
        {
            return GetDocflowAsync($"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}", timeout);
        }

        public Task<IDocflowWithDocuments?> TryGetInventoryDocflowAsync(Guid accountId, Guid relatedDocflowId, Guid relatedDocumentId, Guid inventoryId, TimeSpan? timeout = null) =>
            TryGetDocflowAsync($"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}", timeout);

        public Task<List<Document>> GetDocumentsAsync(Guid accountId, Guid docflowId, TimeSpan? timeout = null) =>
            http.GetAsync<List<Document>>(
                $"/v1/{accountId}/docflows/{docflowId}/documents",
                TimeoutSpecification.SpecificOrLongOperationTimeout(timeout)
            );

        public Task<Document> GetDocumentAsync(Guid accountId, Guid docflowId, Guid documentId, TimeSpan? timeout = null) =>
            http.GetAsync<Document>($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}", timeout);

        public Task<Document?> TryGetDocumentAsync(Guid accountId, Guid docflowId, Guid documentId, TimeSpan? timeout = null) =>
            http.TryGetAsync<Document>($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}", timeout);

        public Task<DocflowDocumentDescription> GetDocumentDescriptionAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            TimeSpan? timeout = null)
        {
            return http.GetAsync<DocflowDocumentDescription>(
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/description",
                timeout
            );
        }

        public Task<List<Signature>> GetDocumentSignaturesAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            TimeSpan? timeout = null)
        {
            return http.GetAsync<List<Signature>>(
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/signatures",
                timeout
            );
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
                timeout
            );
        }

        public Task<byte[]> GetSignatureContentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null)
        {
            return http.GetBytesAsync(
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/signatures/{signatureId}/content",
                timeout
            );
        }

        public Task<string> GetSignatureContentAsBase64StringAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null)
        {
            return http.GetAsync<string>(
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/signatures/{signatureId}/content",
                timeout
            );
        }

        public Task<byte[]> GetInventorySignatureContentAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            Guid documentId,
            Guid signatureId,
            TimeSpan? timeout = null)
        {
            return http.GetBytesAsync(
                $"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}/documents/{documentId}/signatures/{signatureId}/content",
                timeout
            );
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
                timeout
            );
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
                timeout
            );
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
                timeout
            );
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
                timeout
            );
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
                timeout
            );
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
                timeout
            );
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
                timeout
            );
        }

        public Task<DocumentsRequest> GetDocumentsRequestAsync(Guid accountId, Guid docflowId, Guid requestId, TimeSpan? timeout = null) =>
            http.GetAsync<DocumentsRequest>(
                $"/v1/{accountId}/docflows/{docflowId}/documents-requests/{requestId}",
                timeout
            );

        public Task<DocumentsRequest> GenerateDocumentsRequestAsync(Guid accountId, Guid docflowId, byte[] certificate, TimeSpan? timeout = null)
        {
            return http.PostAsync<GenerateDocumentsRequestRequest, DocumentsRequest>(
                $"/v1/{accountId}/docflows/{docflowId}/generate-documents-request",
                new GenerateDocumentsRequestRequest
                {
                    CertificateBase64 = certificate
                },
                timeout
            );
        }

        public Task<IDocflowWithDocuments> SendDocumentsRequestAsync(Guid accountId, Guid docflowId, Guid requestId, TimeSpan? timeout = null)
        {
            return http.PostAsync<IDocflowWithDocuments>(
                $"/v1/{accountId}/docflows/{docflowId}/documents-requests/{requestId}/send",
                timeout);
        }

        public Task<DocumentsRequest> UpdateDocumentsRequestSignatureAsync(Guid accountId, Guid docflowId, Guid requestId, byte[] signature, TimeSpan? timeout = null)
        {
            return http.PutAsync<byte[], DocumentsRequest>(
                $"v1/{accountId}/docflows/{docflowId}/documents-requests/{requestId}/signature",
                signature,
                timeout);
        }

        private Task<DocflowPage> GetRelatedDocflowsAsync(RequestUrlBuilder urlBuilder, TimeSpan? timeout) => http.GetAsync<DocflowPage>(urlBuilder.Build(), timeout);

        private Task<IDocflowWithDocuments> GetDocflowAsync(string url, TimeSpan? timeout)
        {
            return http.GetAsync<IDocflowWithDocuments>(url, timeout);
        }

        private Task<IDocflowWithDocuments?> TryGetDocflowAsync(string url, TimeSpan? timeout) => http.TryGetAsync<IDocflowWithDocuments>(url, timeout);
    }
}