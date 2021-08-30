#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kontur.Extern.Client.ApiLevel.Models.Api;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Docflows;
using Kontur.Extern.Client.ApiLevel.Models.Documents;
using Kontur.Extern.Client.ApiLevel.Models.Documents.Data;
using Kontur.Extern.Client.ApiLevel.Models.Drafts.Requests;
using Kontur.Extern.Client.Http;
using Vostok.Clusterclient.Core.Model;
// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.ApiLevel.Clients.Docflows
{
    //todo Сделать нормальные тесты для методов.
    public class DocflowsClient : IDocflowsClient
    {
        private readonly IHttpRequestsFactory http;

        public DocflowsClient(IHttpRequestsFactory http) => this.http = http;

        public Task<DocflowPage> GetDocflowsAsync(Guid accountId, DocflowFilter? filter = null, TimeSpan? timeout = null)
        {
            var urlBuilder = new RequestUrlBuilder($"/v1/{accountId}/docflows");
            if (filter != null)
            {
                foreach (var kv in filter.ConvertToQueryParameters().Where(kv => kv.Value != null))
                    urlBuilder.AppendToQuery(kv.Key, kv.Value);
            }

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
            if (filter != null)
            {
                foreach (var kv in filter.ConvertToQueryParameters().Where(kv => kv.Value != null))
                    urlBuilder.AppendToQuery(kv.Key, kv.Value);
            }
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
            if (filter != null)
            {
                foreach (var kv in filter.ConvertToQueryParameters().Where(kv => kv.Value != null))
                    urlBuilder.AppendToQuery(kv.Key, kv.Value);
            }
            return GetRelatedDocflowsAsync(urlBuilder, timeout);
        }

        public Task<Docflow> GetDocflowAsync(Guid accountId, Guid docflowId, TimeSpan? timeout = null) => 
            GetDocflowAsync($"/v1/{accountId}/docflows/{docflowId}", timeout);

        public Task<Docflow?> TryGetDocflowAsync(Guid accountId, Guid docflowId, TimeSpan? timeout = null) => 
            http.TryGetAsync<Docflow>($"/v1/{accountId}/docflows/{docflowId}", timeout);

        public Task<Docflow> GetInventoryDocflowAsync(
            Guid accountId,
            Guid relatedDocflowId,
            Guid relatedDocumentId,
            Guid inventoryId,
            TimeSpan? timeout = null)
        {
            return GetDocflowAsync($"/v1/{accountId}/docflows/{relatedDocflowId}/documents/{relatedDocumentId}/inventories/{inventoryId}", timeout);
        }

        public Task<Docflow?> TryGetInventoryDocflowAsync(Guid accountId, Guid relatedDocflowId, Guid relatedDocumentId, Guid inventoryId, TimeSpan? timeout = null) => 
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

        public Task<CloudDecryptionInitResult> StartCloudDecryptDocumentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            byte[] publicKey,
            TimeSpan? timeout = null)
        {
            return http.PostAsync<CertificateRequest, CloudDecryptionInitResult>(
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/decrypt-content",
                new CertificateRequest {PublicKey = publicKey},
                timeout
            );
        }

        public Task<DecryptDocumentResult> ConfirmDocumentDecryptionAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            string requestId,
            string code,
            bool? unzip = null,
            TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/confirm-content-decryption")
                .AppendToQuery("requestId", requestId)
                .AppendToQuery("code", code)
                .AppendToQuery("unzip", unzip)
                .Build();
            return http.PostAsync<DecryptDocumentResult>(url, timeout);
        }

        public Task<DssDecryptionInitResult> StartDssDecryptDocumentAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            byte[] publicKey,
            bool? unzip = null,
            TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder($"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/decrypt-content")
                .AppendToQuery("unzipIfCan", unzip)
                .Build();
            return http.PostAsync<CertificateRequest, DssDecryptionInitResult>(
                url,
                new CertificateRequest {PublicKey = publicKey},
                timeout
            );
        }

        public Task<ApiTaskResult<DecryptDocumentResult>> GetDssDecryptDocumentTaskAsync(
            Guid accountId,
            Guid docflowId,
            Guid documentId,
            Guid taskId,
            TimeSpan? timeout = null)
        {
            return http.PostAsync<ApiTaskResult<DecryptDocumentResult>>(
                $"/v1/{accountId}/docflows/{docflowId}/documents/{documentId}/tasks/{taskId}",
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

        private Task<DocflowPage> GetRelatedDocflowsAsync(RequestUrlBuilder urlBuilder, TimeSpan? timeout) => http.GetAsync<DocflowPage>(urlBuilder.Build(), timeout);

        private Task<Docflow> GetDocflowAsync(string url, TimeSpan? timeout) => http.GetAsync<Docflow>(url, timeout);
        
        private Task<Docflow?> TryGetDocflowAsync(string url, TimeSpan? timeout) => http.TryGetAsync<Docflow>(url, timeout);
    }
}